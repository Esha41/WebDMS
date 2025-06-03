using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class seedcompanyidtobatchestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update Batches set CompanyId = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
