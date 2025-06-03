using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeCienCompanyCustomFieldValuesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_DocumentClassFields_FieldId",
                table: "ClientCompanyCustomFieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_DocumentVersion_DocumentVersionId",
                table: "ClientCompanyCustomFieldValues");

            migrationBuilder.DropIndex(
                name: "IX_ClientCompanyCustomFieldValues_DocumentVersionId",
                table: "ClientCompanyCustomFieldValues");

            migrationBuilder.DropColumn(
                name: "DocumentVersionId",
                table: "ClientCompanyCustomFieldValues");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_CompanyCustomFieldes_FieldId",
                table: "ClientCompanyCustomFieldValues",
                column: "FieldId",
                principalTable: "CompanyCustomFieldes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_CompanyCustomFieldes_FieldId",
                table: "ClientCompanyCustomFieldValues");

            migrationBuilder.AddColumn<int>(
                name: "DocumentVersionId",
                table: "ClientCompanyCustomFieldValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanyCustomFieldValues_DocumentVersionId",
                table: "ClientCompanyCustomFieldValues",
                column: "DocumentVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_DocumentClassFields_FieldId",
                table: "ClientCompanyCustomFieldValues",
                column: "FieldId",
                principalTable: "DocumentClassFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCompanyCustomFieldValues_DocumentVersion_DocumentVersionId",
                table: "ClientCompanyCustomFieldValues",
                column: "DocumentVersionId",
                principalTable: "DocumentVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
