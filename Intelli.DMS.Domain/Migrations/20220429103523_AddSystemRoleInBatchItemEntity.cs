using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddSystemRoleInBatchItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemRoleId",
                table: "BatchItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_SystemRoleId",
                table: "BatchItems",
                column: "SystemRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItems_SystemRoles_SystemRoleId",
                table: "BatchItems",
                column: "SystemRoleId",
                principalTable: "SystemRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchItems_SystemRoles_SystemRoleId",
                table: "BatchItems");

            migrationBuilder.DropIndex(
                name: "IX_BatchItems_SystemRoleId",
                table: "BatchItems");

            migrationBuilder.DropColumn(
                name: "SystemRoleId",
                table: "BatchItems");
        }
    }
}
