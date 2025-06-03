using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveCompanyIdInBatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Companies_CompanyId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_CompanyId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Batches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Batches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_CompanyId",
                table: "Batches",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Companies_CompanyId",
                table: "Batches",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
