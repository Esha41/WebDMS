using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveColumnsInCompanyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentController",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CustomerRetries",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpActive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpDirectory",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpHostName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpPassword",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpPort",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponseActive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponseDirectory",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponseHostName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponsePassword",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponsePort",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponseUserName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpResponseUserSecureProtocol",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpUserName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FtpUserSecureProtocol",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HawkAppID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HawkSecret",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HawkUser",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MaxCallTIme",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ResponseWithFtp",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RetriesWhenFailPublished",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SLABatchQuantity",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SLAMinutes",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SMSProvider",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SimilarityThreshold",
                table: "Companies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentController",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Companies",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerRetries",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FtpActive",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpDirectory",
                table: "Companies",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpHostName",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpPassword",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FtpPort",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FtpResponseActive",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpResponseDirectory",
                table: "Companies",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpResponseHostName",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpResponsePassword",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FtpResponsePort",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpResponseUserName",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FtpResponseUserSecureProtocol",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpUserName",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FtpUserSecureProtocol",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HawkAppID",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HawkSecret",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HawkUser",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxCallTIme",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResponseWithFtp",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetriesWhenFailPublished",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SLABatchQuantity",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SLAMinutes",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMSProvider",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimilarityThreshold",
                table: "Companies",
                type: "int",
                nullable: true);
        }
    }
}
