using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RenameEnumvalueColumAndRemoveColumnsInDocumentClassEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupMandatoryDocument",
                table: "DocumentClasses");

            migrationBuilder.DropColumn(
                name: "RecognitionMappedName",
                table: "DocumentClasses");

            migrationBuilder.RenameColumn(
                name: "EnumValue",
                table: "DocumentClasses",
                newName: "DocumentClassCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentClassCode",
                table: "DocumentClasses",
                newName: "EnumValue");

            migrationBuilder.AddColumn<short>(
                name: "GroupMandatoryDocument",
                table: "DocumentClasses",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecognitionMappedName",
                table: "DocumentClasses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
