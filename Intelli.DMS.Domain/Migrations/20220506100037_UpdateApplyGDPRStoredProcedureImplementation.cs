using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class UpdateApplyGDPRStoredProcedureImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				ALTER PROCEDURE [dbo].[sp_ApplyGDPR]

				AS
				declare @Count int
				declare @requestId nvarchar(50)
				
				create table #tempBatch (GDPRStatus VARCHAR(50),batchId int,requestId nvarchar(50),
							clientId int, clientName VARCHAR(50), appliedTime datetime)
							

				BEGIN

					SET NOCOUNT ON;
							Declare @BatchId int = 0;
							Declare @TempClientId int = 0;
							Declare @ClientId int = 0;
							Declare @ClientName VARCHAR(50)
							Declare @GdprStatus VARCHAR(50)
							Declare @AppliedTime datetime=getdate()
							
					  BEGIN TRY		
							
						set	@Count= (select count(*) from ClientsDataToBeDeleted)
						if (@Count>0)
						begin
						DECLARE batch_cursor CURSOR FOR
						SELECT RequestId
						FROM ClientsDataToBeDeleted
                     	end

						else
						begin
						set	@Count= (select count(*) from BatchesDataTobeDeleted)
						DECLARE batch_cursor CURSOR FOR
						SELECT RequestId
						FROM BatchesDataTobeDeleted
						end

						OPEN batch_cursor
						FETCH NEXT FROM batch_cursor
						INTO @requestId

						WHILE @@FETCH_STATUS = 0
						BEGIN

						IF not EXISTS(SELECT * from Batches where RequestId = @requestId)
						 BEGIN
							INSERT INTO #tempBatch VALUES ('NotApplied',0,@requestId,0,null,@AppliedTime) 
						 END
					    ELSE
						 BEGIN

							set @BatchId = (select Id from Batches where RequestId = @requestId)
							set @ClientId = (select CustomerId from Batches where RequestId = @requestId)
							set @ClientName = (select FirstName from Clients where Id = @ClientId)

							Update BatchMeta set FieldValue = '****' where BatchId = @BatchId

							update BatchItemFields set RegisteredFieldValue = '****', RegisteredFieldValue_old = '****' where batchItemId in  (select Id from BatchItems where BatchId = @BatchId)
			
							update Batches set AppliedGDPR = 1 where Id = @BatchId
			
				            IF not EXISTS (Select * from ServiceLastExcecution where ServiceName = 'GDPRService')
							Begin
								insert into ServiceLastExcecution(ServiceName , Time) values ('GDPRService' ,GETDATE())
							End
							Else
							Begin 
								update ServiceLastExcecution set Time = GETDATE() where ServiceName = 'GDPRService'
							end

			             	INSERT INTO #tempBatch VALUES ('Completed',@BatchId,@requestId,@ClientId,@ClientName,@AppliedTime) 

							FETCH NEXT FROM batch_cursor 
							INTO @requestId
					
						END
						END

						CLOSE batch_cursor;
						DEALLOCATE batch_cursor;
							
					Select * from #tempBatch

					END TRY
					BEGIN CATCH	
					return 1
					END CATCH

					return 0
					END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
