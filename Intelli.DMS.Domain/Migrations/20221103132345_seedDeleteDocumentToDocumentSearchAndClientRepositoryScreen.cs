using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class seedDeleteDocumentToDocumentSearchAndClientRepositoryScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO[dbo].[ScreenElements]([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES('DeleteDocument', 13, 1, DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()), DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

                INSERT INTO[dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES('DeleteDocument', 17, 1, DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()), DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}