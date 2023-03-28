using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumenTVersionEntityRelationshipsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentVersionId",
                table: "BatchMeta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentVersionId",
                table: "BatchItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentVersionId",
                table: "BatchItemPages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentVersionId",
                table: "BatchItemFields",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentVersionId",
                table: "BatchMeta");

            migrationBuilder.DropColumn(
                name: "DocumentVersionId",
                table: "BatchItems");

            migrationBuilder.DropColumn(
                name: "DocumentVersionId",
                table: "BatchItemPages");

            migrationBuilder.DropColumn(
                name: "DocumentVersionId",
                table: "BatchItemFields");
        }
    }
}
