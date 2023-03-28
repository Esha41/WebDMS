using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeSeededScreens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                update screens set ScreenName = 'CheckedOutDocuments' where Id = 12;
                update screens set ScreenName = '' where Id = 13
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
