using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddStatusInBatchItemStatusEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[BatchItemStatuses]
           ([BatchItemStatus]
           ,[EnumValue]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('Uploaded'
           ,'Uploaded'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

INSERT INTO [dbo].[BatchItemStatuses]
           ([BatchItemStatus]
           ,[EnumValue]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('In Process'
           ,'In Process'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

INSERT INTO [dbo].[BatchItemStatuses]
           ([BatchItemStatus]
           ,[EnumValue]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('Approved'
           ,'Approved'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
                                 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
