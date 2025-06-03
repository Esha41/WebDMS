using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveColumnsFromComanyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallBackURL",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsSignedCompany",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SLAImportance",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SendLink",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SendRejectionReasonAsCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SupportsCalls",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "VideoCallBackUrl",
                table: "Companies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallBackURL",
                table: "Companies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSignedCompany",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SLAImportance",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SendLink",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendRejectionReasonAsCode",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "SupportsCalls",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VideoCallBackUrl",
                table: "Companies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
