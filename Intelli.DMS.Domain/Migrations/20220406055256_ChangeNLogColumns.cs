using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeNLogColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoggedOn",
                table: "NLog",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedOn",
                table: "NLog");
        }
    }
}
