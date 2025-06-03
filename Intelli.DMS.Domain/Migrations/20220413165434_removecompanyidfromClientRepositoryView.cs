using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class removecompanyidfromClientRepositoryView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                               DROP VIEW [dbo].[ClientRepositoryView];
                    ");
            migrationBuilder.Sql(@"
                                Create View [dbo].[ClientRepositoryView] As
                                SELECT Repo.[Id] 
                                        ,Repo.[AppliedGDPR]
                                        ,cus.[Id] as ClientId
                                		,cus.[FirstName]+' '+cus.[LastName] as ClientName
                                		,Repo.[Id] as RepositoryName
                                		,Repo.[CreatedDate]
                                        ,Repo.[IsActive]
                                        ,Repo.[CreatedAt]
                                        ,Repo.[UpdatedAt]
                                FROM [Batches] Repo
                                LEFT OUTER JOIN  [dbo].[Clients] cus on Repo.[CustomerId] = cus.[Id]   
                                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
