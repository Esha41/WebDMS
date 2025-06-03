using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddStatusInBatchStatusEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[BatchStatuses]
           ([BatchStatus]
           ,[EnumValue]
           ,[Description]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('Pending Client'
           ,'Pending Client'
           ,'Pending Client'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

		   INSERT INTO [dbo].[BatchStatuses]
           ([BatchStatus]
           ,[EnumValue]
           ,[Description]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('Confilled Client'
           ,'Confilled Client'
           ,'Confilled Client'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

		   INSERT INTO [dbo].[BatchStatuses]
           ([BatchStatus]
           ,[EnumValue]
           ,[Description]
           ,[CreatedAt]
           ,[IsActive]
           ,[UpdatedAt])
     VALUES
           ('Completed Client'
           ,'Completed Client'
           ,'Completed Client'
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE())
           ,1
           ,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
                                 ")
;        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
