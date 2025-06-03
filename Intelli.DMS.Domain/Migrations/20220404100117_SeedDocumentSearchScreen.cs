using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class SeedDocumentSearchScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Screens ON 
                GO
                insert into Screens (Id, ScreenName, IsActive, CreatedAt, UpdatedAt) values (13, 'DocumentSearch', 1, 0, 0)
                GO
                SET IDENTITY_INSERT Screens OFF
                GO

                if not exists (select * from roleScreens where systemRoleId = 1 and screenId = 13)
                begin
                insert into roleScreens (systemRoleId, screenId, privilege, isActive, createdAt, updatedAt)
                values (1, 13, 2, 1, 0, 0)
                end
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
