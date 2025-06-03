using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumentCheckoutLogView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              CREATE View [dbo].[DocumentCheckOutLogsView] As                
                        SELECT TOP (1000) DocCheckLogs.[Id]
									      ,DocCheckLogs.[IsActive]
									      ,DocCheckLogs.[CreatedAt]
									      ,DocCheckLogs.[UpdatedAt]
									      ,DocCheckLogs.[BatchItemId]
									      , isNull((
										  select top 1 SysUser.FullName from SystemUsers SysUser
										  where SysUser.Id = DocCheckLogs.SystemUserId
										  ),'_') [UserName]
									      ,DocCheckLogs.[CheckedOutAt]
									      ,DocCheckLogs.[CheckedInAt]
									      ,DocCheckLogs.[Action]
                                  FROM [DMS].[dbo].[DocumentsCheckedOutLogs] DocCheckLogs
                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
