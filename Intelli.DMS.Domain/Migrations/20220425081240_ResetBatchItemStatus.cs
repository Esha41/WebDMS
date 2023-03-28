using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ResetBatchItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE [dbo].[BatchItemStatuses]
   SET BatchItemStatus='Uploaded'
      ,EnumValue='Uploaded'
 WHERE id =1

 UPDATE [dbo].[BatchItemStatuses]
   SET BatchItemStatus='In Process'
      ,EnumValue='In Process'
 WHERE id =2

 UPDATE [dbo].[BatchItemStatuses]
   SET BatchItemStatus='Approved'
      ,EnumValue='Approved'
 WHERE id =3
                                 ");

            migrationBuilder.Sql(@"
UPDATE [dbo].[BatchStatuses]
   SET [BatchStatus] = 'Pending Client'
      ,[EnumValue] = 'Pending Client'
      ,[Description] = 'Pending Client'
 WHERE id=1

 UPDATE [dbo].[BatchStatuses]
   SET [BatchStatus] = 'Confilled Client'
      ,[EnumValue] = 'Confilled Client'
      ,[Description] = 'Confilled Client'
 WHERE id=2

 UPDATE [dbo].[BatchStatuses]
   SET [BatchStatus] = 'Completed Client'
      ,[EnumValue] = 'Completed Client'
      ,[Description] = 'Completed Client'
 WHERE id=3

                                 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
