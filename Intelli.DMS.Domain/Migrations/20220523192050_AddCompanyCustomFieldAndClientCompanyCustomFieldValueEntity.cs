using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddCompanyCustomFieldAndClientCompanyCustomFieldValueEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientCompanyCustomFieldValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    DictionaryValueId = table.Column<int>(type: "int", nullable: true),
                    RegisteredFieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    DocumentVersionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCompanyCustomFieldValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCompanyCustomFieldValues_BopDictionaries_DictionaryValueId",
                        column: x => x.DictionaryValueId,
                        principalTable: "BopDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientCompanyCustomFieldValues_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCompanyCustomFieldValues_DocumentClassFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "DocumentClassFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCompanyCustomFieldValues_DocumentVersion_DocumentVersionId",
                        column: x => x.DocumentVersionId,
                        principalTable: "DocumentVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCustomFieldes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FieldTypeId = table.Column<int>(type: "int", nullable: false),
                    FieldCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uilabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UISort = table.Column<int>(type: "int", nullable: true),
                    DictionaryTypeId = table.Column<int>(type: "int", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomFieldes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCustomFieldes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCustomFieldes_DictionaryTypes_DictionaryTypeId",
                        column: x => x.DictionaryTypeId,
                        principalTable: "DictionaryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCustomFieldes_DocumentClassFieldTypes_FieldTypeId",
                        column: x => x.FieldTypeId,
                        principalTable: "DocumentClassFieldTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanyCustomFieldValues_ClientId",
                table: "ClientCompanyCustomFieldValues",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanyCustomFieldValues_DictionaryValueId",
                table: "ClientCompanyCustomFieldValues",
                column: "DictionaryValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanyCustomFieldValues_DocumentVersionId",
                table: "ClientCompanyCustomFieldValues",
                column: "DocumentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCompanyCustomFieldValues_FieldId",
                table: "ClientCompanyCustomFieldValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomFieldes_CompanyId",
                table: "CompanyCustomFieldes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomFieldes_DictionaryTypeId",
                table: "CompanyCustomFieldes",
                column: "DictionaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomFieldes_FieldTypeId",
                table: "CompanyCustomFieldes",
                column: "FieldTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCompanyCustomFieldValues");

            migrationBuilder.DropTable(
                name: "CompanyCustomFieldes");
        }
    }
}
