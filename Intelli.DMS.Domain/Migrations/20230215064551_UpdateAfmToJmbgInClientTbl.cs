using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class UpdateAfmToJmbgInClientTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AFM",
                table: "Clients",
                newName: "JMBG");
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JMBG",
                table: "Clients",
                newName: "AFM");
        }
    }
}
