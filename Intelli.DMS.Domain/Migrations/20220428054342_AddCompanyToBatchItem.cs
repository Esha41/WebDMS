using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddCompanyToBatchItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update [BatchItems] set [CompanyId]=1 ");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "BatchItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplyGDPR",
                columns: table => new
                {
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                });
            


            migrationBuilder.AddForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems");

            migrationBuilder.DropTable(
                name: "ApplyGDPR");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "BatchItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchItems_Companies_CompanyId",
                table: "BatchItems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
