using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class FillupOutLookEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update SystemUsers  set OutlookEmail = Email ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
