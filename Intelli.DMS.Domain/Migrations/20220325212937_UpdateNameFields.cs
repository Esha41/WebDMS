using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class UpdateNameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchMetaHistory_SystemUsers",
                table: "BatchMetaHistory");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "BatchItems");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchMetaHistory_SystemUsers_SystemUserId",
                table: "BatchMetaHistory",
                column: "SystemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchMetaHistory_SystemUsers_SystemUserId",
                table: "BatchMetaHistory");

            migrationBuilder.AddColumn<int>(
                name: "AspNetUserId",
                table: "BatchItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchMetaHistory_SystemUsers",
                table: "BatchMetaHistory",
                column: "SystemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
