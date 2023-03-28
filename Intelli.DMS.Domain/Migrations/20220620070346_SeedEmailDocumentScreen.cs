using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class SeedEmailDocumentScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Screens ON 
                GO
                insert into Screens (Id, ScreenName, IsActive, CreatedAt, UpdatedAt) values (23, 'EmailedDocuments', 1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()), DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
                GO
                SET IDENTITY_INSERT Screens OFF
                GO
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
