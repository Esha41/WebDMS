using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class DropColumnsInDocumentClassField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectiveActionMappedName",
                table: "DocumentClassFields");

            migrationBuilder.DropColumn(
                name: "EnumValue",
                table: "DocumentClassFields");

            migrationBuilder.DropColumn(
                name: "MappedName",
                table: "DocumentClassFields");

            migrationBuilder.DropColumn(
                name: "PublishEnabled",
                table: "DocumentClassFields");

            migrationBuilder.DropColumn(
                name: "UISort",
                table: "DocumentClassFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectiveActionMappedName",
                table: "DocumentClassFields",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnumValue",
                table: "DocumentClassFields",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "(N'-')");

            migrationBuilder.AddColumn<string>(
                name: "MappedName",
                table: "DocumentClassFields",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PublishEnabled",
                table: "DocumentClassFields",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<int>(
                name: "UISort",
                table: "DocumentClassFields",
                type: "int",
                nullable: true);
        }
    }
}
