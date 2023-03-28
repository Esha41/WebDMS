using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddCreatedByIdInClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ClienTag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienTag_CreatedById",
                table: "ClienTag",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienTag_SystemUsers_CreatedById",
                table: "ClienTag",
                column: "CreatedById",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienTag_SystemUsers_CreatedById",
                table: "ClienTag");

            migrationBuilder.DropIndex(
                name: "IX_ClienTag_CreatedById",
                table: "ClienTag");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ClienTag");
        }
    }
}
