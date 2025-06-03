using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveDocumentClassFieldUiLabelInBatchItemField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentClassFieldUiLabel",
                table: "BatchItemFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentClassFieldUiLabel",
                table: "BatchItemFields",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
