using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RenameVariablesCompanyCustomFiledEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes");

            migrationBuilder.DropColumn(
                name: "FieldCode",
                table: "CompanyCustomFieldes");

            migrationBuilder.RenameColumn(
                name: "FieldTypeId",
                table: "CompanyCustomFieldes",
                newName: "DocumentClassFieldTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCustomFieldes_FieldTypeId",
                table: "CompanyCustomFieldes",
                newName: "IX_CompanyCustomFieldes_DocumentClassFieldTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_DocumentClassFieldTypeId",
                table: "CompanyCustomFieldes",
                column: "DocumentClassFieldTypeId",
                principalTable: "DocumentClassFieldTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_DocumentClassFieldTypeId",
                table: "CompanyCustomFieldes");

            migrationBuilder.RenameColumn(
                name: "DocumentClassFieldTypeId",
                table: "CompanyCustomFieldes",
                newName: "FieldTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCustomFieldes_DocumentClassFieldTypeId",
                table: "CompanyCustomFieldes",
                newName: "IX_CompanyCustomFieldes_FieldTypeId");

            migrationBuilder.AddColumn<string>(
                name: "FieldCode",
                table: "CompanyCustomFieldes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes",
                column: "FieldTypeId",
                principalTable: "DocumentClassFieldTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
