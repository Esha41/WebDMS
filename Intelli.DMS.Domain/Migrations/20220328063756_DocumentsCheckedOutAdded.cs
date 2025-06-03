using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class DocumentsCheckedOutAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentsCheckedOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchItemId = table.Column<int>(type: "int", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsCheckedOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsCheckedOut_BatchItems_BatchItemId",
                        column: x => x.BatchItemId,
                        principalTable: "BatchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsCheckedOut_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsCheckedOutLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    BatchItemId = table.Column<int>(type: "int", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    CheckedOutAt = table.Column<long>(type: "bigint", nullable: false),
                    CheckedInAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsCheckedOutLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsCheckedOutLogs_BatchItems_BatchItemId",
                        column: x => x.BatchItemId,
                        principalTable: "BatchItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsCheckedOutLogs_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsCheckedOut_BatchItemId",
                table: "DocumentsCheckedOut",
                column: "BatchItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsCheckedOut_SystemUserId",
                table: "DocumentsCheckedOut",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsCheckedOutLogs_BatchItemId",
                table: "DocumentsCheckedOutLogs",
                column: "BatchItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsCheckedOutLogs_SystemUserId",
                table: "DocumentsCheckedOutLogs",
                column: "SystemUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentsCheckedOut");

            migrationBuilder.DropTable(
                name: "DocumentsCheckedOutLogs");
        }
    }
}
