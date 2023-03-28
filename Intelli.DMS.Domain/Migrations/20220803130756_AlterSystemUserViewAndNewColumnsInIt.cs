using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AlterSystemUserViewAndNewColumnsInIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
ALTER View [dbo].[SystemUsersView] As
SELECT Distinct su.[Id]
,su.[FullName]
,su.[Email]
,su.[OutlookEmail]
,su.[IsActiveDirectoryUser]
,su.[IsActive]
,su.[CreatedAt]
,su.[UpdatedAt]
,(
	select string_agg( ISNULL(c.CompanyName, ' '), ',') from UserCompanies uc inner join Companies c on uc.CompanyId = c.Id where uc.SystemUserId = su.Id	
) [CompanyName],(
	select top 1 CompanyId from UserCompanies uc inner join Companies c on uc.CompanyId = c.Id where uc.SystemUserId = su.Id	
) [CompanyId]
FROM [SystemUsers] su
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
