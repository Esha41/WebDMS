using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ClientTagEntityNameSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClienTag_Clients_ClientId",
                table: "ClienTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienTag_SystemUsers_CreatedById",
                table: "ClienTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienTag",
                table: "ClienTag");

            migrationBuilder.RenameTable(
                name: "ClienTag",
                newName: "ClientTag");

            migrationBuilder.RenameIndex(
                name: "IX_ClienTag_CreatedById",
                table: "ClientTag",
                newName: "IX_ClientTag_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ClienTag_ClientId",
                table: "ClientTag",
                newName: "IX_ClientTag_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientTag",
                table: "ClientTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTag_Clients_ClientId",
                table: "ClientTag",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTag_SystemUsers_CreatedById",
                table: "ClientTag",
                column: "CreatedById",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientTag_Clients_ClientId",
                table: "ClientTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientTag_SystemUsers_CreatedById",
                table: "ClientTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientTag",
                table: "ClientTag");

            migrationBuilder.RenameTable(
                name: "ClientTag",
                newName: "ClienTag");

            migrationBuilder.RenameIndex(
                name: "IX_ClientTag_CreatedById",
                table: "ClienTag",
                newName: "IX_ClienTag_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ClientTag_ClientId",
                table: "ClienTag",
                newName: "IX_ClienTag_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienTag",
                table: "ClienTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienTag_Clients_ClientId",
                table: "ClienTag",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClienTag_SystemUsers_CreatedById",
                table: "ClienTag",
                column: "CreatedById",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
