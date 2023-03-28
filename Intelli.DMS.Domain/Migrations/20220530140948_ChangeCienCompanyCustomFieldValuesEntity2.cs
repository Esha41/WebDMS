using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeCienCompanyCustomFieldValuesEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes",
                column: "FieldTypeId",
                principalTable: "DocumentClassFieldTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                table: "CompanyCustomFieldes",
                column: "FieldTypeId",
                principalTable: "DocumentClassFieldTypes",
                principalColumn: "Id");
        }
    }
}
