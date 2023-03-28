using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class logging_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "NLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NLog",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "NLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "NLog");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NLog");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "NLog");
        }
    }
}
