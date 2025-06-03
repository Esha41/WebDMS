using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class addGDPRViewsAnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create PROCEDURE [dbo].[sp_ApplyGDPR]

					@requestId varchar(250)
				AS
				DECLARE @ResultValue int
				BEGIN

					SET NOCOUNT ON;
							Declare @BatchId int = 0;

							BEGIN TRY
						 IF not EXISTS(SELECT * from Batches where RequestId = @requestId )
						 BEGIN
							Set @ResultValue = 0;
							END
							ELSE
						 BEGIN
							set @BatchId = (select Id from Batches where RequestId = @requestId)
			
			
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
							
							select * from ServiceLastExcecution
							Set @ResultValue = 1;
							END
						END TRY
						BEGIN CATCH
							Set @ResultValue = 0;
							END CATCH

					Return @ResultValue;
							END");

			migrationBuilder.Sql(@"  create View [dbo].[ClientsDataToBeDeleted] As
								  SELECT B.* , C.id as cId
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.Clients AS C 
												   ON B.CustomerId = C.id
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 4 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.publisheddate, Getdate()) > C.gdprdaystobekept )
											   ) 
		 
											 )");

			migrationBuilder.Sql(@" Create View [dbo].[BatchesDataTobeDeleted] As
								  SELECT B.*  
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.companies AS C 
												   ON B.companyid = C.id 
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 4 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.publisheddate, Getdate()) > C.gdprdaystobekept )
											   ) )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
