using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddIEntityFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "TempBatchPages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TempBatchPages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "TempBatchPages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "StationVariableTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StationVariableTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "StationVariableTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "StationVariables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StationVariables",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "StationVariables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Stations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Stations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "Stations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "Rules_xref",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Rules_xref",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "Rules_xref",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "OCREnginesDocumentClasses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OCREnginesDocumentClasses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "OCREnginesDocumentClasses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "OCREngines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OCREngines",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "OCREngines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "LivenessTokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LivenessTokens",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "LivenessTokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "DocumentRejectionReasons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DocumentRejectionReasons",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "DocumentRejectionReasons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "DocumentRejectionReasonCompany",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DocumentRejectionReasonCompany",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "DocumentRejectionReasonCompany",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "DashboardMenu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DashboardMenu",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "DashboardMenu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "CompanySigningDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CompanySigningDocuments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "CompanySigningDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "CompanyFlows",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CompanyFlows",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "CompanyFlows",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "CatalogNameProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CatalogNameProducts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "CatalogNameProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BUs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BUs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BUs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BopConfigs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BopConfigs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BopConfigs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BatchStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BatchStatuses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BatchStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BatchSources",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BatchSources",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BatchSources",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BatchSourceDocumentsSpecifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BatchSourceDocumentsSpecifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BatchSourceDocumentsSpecifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BatchMetaHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BatchMetaHistory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BatchMetaHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "BatchItemStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BatchItemStatuses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "BatchItemStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "AdvancedSignatureCallHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AdvancedSignatureCallHistory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "AdvancedSignatureCallHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                table: "AdvancedLogging",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AdvancedLogging",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedAt",
                table: "AdvancedLogging",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TempBatchPages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TempBatchPages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TempBatchPages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StationVariableTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StationVariableTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StationVariableTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StationVariables");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StationVariables");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StationVariables");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rules_xref");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Rules_xref");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Rules_xref");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OCREnginesDocumentClasses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OCREnginesDocumentClasses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OCREnginesDocumentClasses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OCREngines");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OCREngines");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OCREngines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LivenessTokens");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LivenessTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "LivenessTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DocumentRejectionReasons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DocumentRejectionReasons");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DocumentRejectionReasons");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DocumentRejectionReasonCompany");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DocumentRejectionReasonCompany");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DocumentRejectionReasonCompany");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DashboardMenu");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DashboardMenu");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DashboardMenu");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CompanySigningDocuments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CompanySigningDocuments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CompanySigningDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CompanyFlows");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CompanyFlows");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CompanyFlows");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CatalogNameProducts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CatalogNameProducts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CatalogNameProducts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BUs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BUs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BUs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BopConfigs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BopConfigs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BopConfigs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BatchStatuses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BatchStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BatchStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BatchSources");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BatchSources");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BatchSources");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BatchSourceDocumentsSpecifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BatchSourceDocumentsSpecifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BatchSourceDocumentsSpecifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BatchMetaHistory");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BatchMetaHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BatchMetaHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BatchItemStatuses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BatchItemStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BatchItemStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AdvancedSignatureCallHistory");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AdvancedSignatureCallHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AdvancedSignatureCallHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AdvancedLogging");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AdvancedLogging");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AdvancedLogging");
        }
    }
}
