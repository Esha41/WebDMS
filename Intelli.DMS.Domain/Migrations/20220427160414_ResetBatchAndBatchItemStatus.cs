using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ResetBatchAndBatchItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                 SET IDENTITY_INSERT [dbo].[BatchItemStatuses]  ON
                 
                 INSERT INTO [dbo].[BatchItemStatuses]
                            ([ID]
                 		   ,[BatchItemStatus]
                            ,[EnumValue]
                            ,[CreatedAt]
                            ,[IsActive]
                            ,[UpdatedAt])
                      VALUES
                            (4
                 		   ,'Rejected'
                            ,'Rejected'
                            ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
                            ,1
                            ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
                 

                                ");

            migrationBuilder.Sql(@"

                 UPDATE [dbo].[BatchStatuses]
                   SET [BatchStatus] = 'Created'
                      ,[EnumValue] = 'Created'
                      ,[Description] = 'Created'
                 WHERE id=1
           
                 UPDATE [dbo].[BatchStatuses]
                   SET [BatchStatus] = 'Checked'
                      ,[EnumValue] = 'Checked'
                      ,[Description] = 'Checked'
                 WHERE id=2
           
                 UPDATE [dbo].[BatchStatuses]
                   SET [BatchStatus] = 'Pending'
                      ,[EnumValue] = 'Pending'
                      ,[Description] = 'Pending'
                 WHERE id=3

             ");

            migrationBuilder.Sql(@"

                                ALTER  View [dbo].[ClientRepositoryView] As
                                    SELECT Repo.[Id] 
                                        ,Repo.[AppliedGDPR]
                                        ,cus.[Id] as ClientId
                                		,cus.[FirstName]+' '+cus.[LastName] as ClientName
                                	    ,c.[Id] as CompanyId
                                	    ,c.[CompanyName]
                                		,Repo.[Id] as RepositoryName
                                		, isNull((
										 select top 1 batstatus.BatchStatus from BatchStatuses batstatus
										 where batstatus.Id = Repo.BatchStatusId
										 ),'-') [ClientStatus]
                                		,Repo.[CreatedDate]
                                        ,cus.[IsActive]
                                        ,Repo.[CreatedAt]
                                        ,Repo.[UpdatedAt]
                                FROM [Batches] Repo
                                LEFT OUTER JOIN  [dbo].[Clients] cus on Repo.[CustomerId] = cus.[Id]   
                                LEFT OUTER JOIN  [dbo].[Companies] c on Repo.[CompanyId] = c.[Id]

             ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
