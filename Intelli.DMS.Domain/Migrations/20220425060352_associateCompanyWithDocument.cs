using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class associateCompanyWithDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "BatchItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BatchItems_CompanyId",
                table: "BatchItems",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems");

            migrationBuilder.DropIndex(
                name: "IX_BatchItems_CompanyId",
                table: "BatchItems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "BatchItems");
        }
    }
}
