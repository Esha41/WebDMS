using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class UpdateAllPasswordInCompanyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
  update [DMS].[dbo].[Companies]
  Set [Password]='9KgTDfW90XDQ5codx2olOg=='
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
