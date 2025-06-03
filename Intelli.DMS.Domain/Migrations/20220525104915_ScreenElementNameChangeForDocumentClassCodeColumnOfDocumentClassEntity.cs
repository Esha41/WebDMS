using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ScreenElementNameChangeForDocumentClassCodeColumnOfDocumentClassEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
 Update [DMS].[dbo].[ScreenElements]
  Set [ScreenElementName]='DocumentClassCodeInput' 
  where [ScreenElementName]='EnumValueInput' and [ScreenId]=14
                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
