using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class SeedDummyAlerts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Alerts]
                select [SystemUserId] = su.Id
                , [Msg] = 'Welcome to Document Management System'
                , [IsRead] = 0
                , [CreatedAt] = 0
                , [IsActive] = 1
                , [UpdatedAt] = 0
                from SystemUsers su
                where isActive = 1
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
