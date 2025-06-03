using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveAFMUniqueIndexInClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Customer_AFM", table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
