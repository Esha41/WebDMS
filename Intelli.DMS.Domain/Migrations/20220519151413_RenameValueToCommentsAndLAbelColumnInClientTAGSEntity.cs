using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RenameValueToCommentsAndLAbelColumnInClientTAGSEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "ClientTag");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ClientTag",
                newName: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "ClientTag",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "ClientTag",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
