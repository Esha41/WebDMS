using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumenTVersionEntityRelationshipsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BatchMeta_DocumentVersionId",
                table: "BatchMeta",
                column: "DocumentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_DocumentVersionId",
                table: "BatchItems",
                column: "DocumentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemPages_DocumentVersionId",
                table: "BatchItemPages",
                column: "DocumentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchItemFields_DocumentVersionId",
                table: "BatchItemFields",
                column: "DocumentVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItemFields_DocumentVersion_DocumentVersionId",
                table: "BatchItemFields",
                column: "DocumentVersionId",
                principalTable: "DocumentVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItemPages_DocumentVersion_DocumentVersionId",
                table: "BatchItemPages",
                column: "DocumentVersionId",
                principalTable: "DocumentVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItems_DocumentVersion_DocumentVersionId",
                table: "BatchItems",
                column: "DocumentVersionId",
                principalTable: "DocumentVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchMeta_DocumentVersion_DocumentVersionId",
                table: "BatchMeta",
                column: "DocumentVersionId",
                principalTable: "DocumentVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchItemFields_DocumentVersion_DocumentVersionId",
                table: "BatchItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchItemPages_DocumentVersion_DocumentVersionId",
                table: "BatchItemPages");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchItems_DocumentVersion_DocumentVersionId",
                table: "BatchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchMeta_DocumentVersion_DocumentVersionId",
                table: "BatchMeta");

            migrationBuilder.DropIndex(
                name: "IX_BatchMeta_DocumentVersionId",
                table: "BatchMeta");

            migrationBuilder.DropIndex(
                name: "IX_BatchItems_DocumentVersionId",
                table: "BatchItems");

            migrationBuilder.DropIndex(
                name: "IX_BatchItemPages_DocumentVersionId",
                table: "BatchItemPages");

            migrationBuilder.DropIndex(
                name: "IX_BatchItemFields_DocumentVersionId",
                table: "BatchItemFields");
        }
    }
}
