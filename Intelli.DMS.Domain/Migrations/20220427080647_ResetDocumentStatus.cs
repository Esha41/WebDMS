using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ResetDocumentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

  Update [BatchItemStatuses] set [BatchItemStatus]='Checked',[EnumValue]='Checked' where id=2

                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
