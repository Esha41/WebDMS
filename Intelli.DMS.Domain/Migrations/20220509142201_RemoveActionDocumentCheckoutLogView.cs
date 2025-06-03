using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class RemoveActionDocumentCheckoutLogView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "DocumentsCheckedOutLogs");

            migrationBuilder.Sql(@"
              ALTER View [dbo].[DocumentCheckOutLogsView] As                
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
                                  FROM [DMS].[dbo].[DocumentsCheckedOutLogs] DocCheckLogs
                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "DocumentsCheckedOutLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
