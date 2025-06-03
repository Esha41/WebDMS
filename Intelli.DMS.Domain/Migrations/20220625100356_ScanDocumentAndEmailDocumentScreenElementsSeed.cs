using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ScanDocumentAndEmailDocumentScreenElementsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('ScanDocument',22,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('RemoveScannedDocument',22,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('RemoveAllScannedDocument',22,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('SaveScannedDocument',22,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))


	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('Filter',23,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('Export',23,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('DocumentUpload',23,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
	INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('Delete',23,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

                                   ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
