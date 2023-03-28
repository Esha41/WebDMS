using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddClientCommentsandAddCompanyCodePrivilage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
 Delete FROM [DMS].[dbo].[ScreenElements] where [ScreenId]=21 And [ScreenElementName]='Label'
 Update  [DMS].[dbo].[ScreenElements] Set [ScreenElementName]='ClientComments'  where [ScreenId]=21 And [ScreenElementName]='Value'

   INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('CompanyCodeInput', 5,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
