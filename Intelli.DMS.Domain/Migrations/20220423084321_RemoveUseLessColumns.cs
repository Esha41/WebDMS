using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveUseLessColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersion_Batches_BatchId",
                table: "DocumentVersion");

            migrationBuilder.DropIndex(
                name: "IX_DocumentVersion_BatchId",
                table: "DocumentVersion");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "DocumentVersion");

            migrationBuilder.DropColumn(
                name: "BatchItemReference",
                table: "DocumentVersion");

            migrationBuilder.DropColumn(
                name: "BatchItemReference",
                table: "BatchItemPages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "DocumentVersion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BatchItemReference",
                table: "DocumentVersion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchItemReference",
                table: "BatchItemPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersion_BatchId",
                table: "DocumentVersion",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersion_Batches_BatchId",
                table: "DocumentVersion",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
