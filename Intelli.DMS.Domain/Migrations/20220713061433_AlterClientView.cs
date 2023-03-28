using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AlterClientView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                              ALTER View [dbo].[ClientView] As
                                
                                SELECT cl.[Id]
                                      ,cl.[FirstName]
                                      ,cl.[LastName]
                                      ,cl.[AFM]
                                      ,cl.[IsActive]
                                      ,cl.[CreatedAt]
                                      ,cl.[UpdatedAt]
                                      ,cl.[IsNotValidForTransaction]
                                      ,cl.[Reason]
                                      ,cl.[GdprdaysToBeKept]
                                      ,cl.[ExternalId]
                                      ,cl.[CompanyId]
                                	  , isNull((
                                select top 1 stat.BatchStatus from BatchStatuses stat
                                where bat.BatchStatusId = stat.Id
                                ),'Created') [ClientStatus]
                                  FROM [DMS].[dbo].[Clients] cl
                                left outer JOIN  [dbo].[Batches] bat on bat.[CustomerId] = cl.[Id]  

                             
 
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
