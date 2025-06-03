using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class changeAfmLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE Clients
                                   ALTER COLUMN Afm nvarchar(20);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
