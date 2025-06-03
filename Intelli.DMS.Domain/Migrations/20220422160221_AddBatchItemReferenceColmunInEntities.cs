using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddBatchItemReferenceColmunInEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "DocumentVersion",
                newName: "BatchItemReference");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "DocumentApprovalHistory",
                newName: "BatchItemReference");

            migrationBuilder.RenameColumn(
                name: "BatchItemRefference",
                table: "BatchItems",
                newName: "BatchItemReference");

            migrationBuilder.AddColumn<string>(
                name: "BatchItemReference",
                table: "BatchMeta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchItemReference",
                table: "BatchItemPages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchItemReference",
                table: "BatchMeta");

            migrationBuilder.DropColumn(
                name: "BatchItemReference",
                table: "BatchItemPages");

            migrationBuilder.RenameColumn(
                name: "BatchItemReference",
                table: "DocumentVersion",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "BatchItemReference",
                table: "DocumentApprovalHistory",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "BatchItemReference",
                table: "BatchItems",
                newName: "BatchItemRefference");
        }
    }
}
