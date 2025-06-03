using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class UpdateApplyGDPRStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "ApplyGDPR");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "ApplyGDPR",
                newName: "requestId");

            migrationBuilder.AddColumn<string>(
                name: "GDPRStatus",
                table: "ApplyGDPR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "appliedTime",
                table: "ApplyGDPR",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "batchId",
                table: "ApplyGDPR",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "ApplyGDPR",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "clientName",
                table: "ApplyGDPR",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDPRStatus",
                table: "ApplyGDPR");

            migrationBuilder.DropColumn(
                name: "appliedTime",
                table: "ApplyGDPR");

            migrationBuilder.DropColumn(
                name: "batchId",
                table: "ApplyGDPR");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "ApplyGDPR");

            migrationBuilder.DropColumn(
                name: "clientName",
                table: "ApplyGDPR");

            migrationBuilder.RenameColumn(
                name: "requestId",
                table: "ApplyGDPR",
                newName: "ServiceName");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "ApplyGDPR",
                type: "datetime2",
                nullable: true);
        }
    }
}
