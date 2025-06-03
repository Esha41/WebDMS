using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveExtraStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                    Delete FROM [DMS].[dbo].[BatchItemStatuses] where Id=4
                    Delete FROM [DMS].[dbo].[BatchStatuses] where Id=4

                                  ");
            migrationBuilder.Sql(@"

                    Update [BatchItemStatuses] Set [CreatedAt]=DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
                               ,[IsActive]=1
                               ,[UpdatedAt]=DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
                    		   where id=1
                    
                    Update [BatchStatuses] Set [CreatedAt]=DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
                               ,[IsActive]=1
                               ,[UpdatedAt]=DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
                    		   where id=1

                                ");
            migrationBuilder.Sql(@"

                    delete from [ScreenElements] where  [ScreenId]=11 And [ScreenElementName]='ApprovalTab'
                    delete from [ScreenElements] where  [ScreenId]=14 And [ScreenElementName]='DocuemntClassGeneralTab'
                    delete from [ScreenElements] where  [ScreenId]=14 And [ScreenElementName]='DocumentClassFieldsTab'
                    delete from [ScreenElements] where  [ScreenId]=21 And [ScreenElementName]='TagsTab'

                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
