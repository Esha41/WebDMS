using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumentClassFieldUiLabelInBatchItemField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentClassFieldName",
                table: "BatchMeta");

            migrationBuilder.AddColumn<string>(
                name: "DocumentClassFieldUiLabel",
                table: "BatchItemFields",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentClassFieldUiLabel",
                table: "BatchItemFields");

            migrationBuilder.AddColumn<string>(
                name: "DocumentClassFieldName",
                table: "BatchMeta",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
