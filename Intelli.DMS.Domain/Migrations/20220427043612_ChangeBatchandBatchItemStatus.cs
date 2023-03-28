using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeBatchandBatchItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

  Update [BatchItemStatuses] set [BatchItemStatus]='Created',[EnumValue]='Created' where id=1

  Update [BatchStatuses] set [BatchStatus]='CREATED',[EnumValue]='CREATED',[Description]='CREATED' where id=1
  Update [BatchStatuses] set [BatchStatus]='CHECKED OK',[EnumValue]='CHECKED OK',[Description]='CHECKED OK' where id=2
  Update [BatchStatuses] set [BatchStatus]='CHECKED NOT OK',[EnumValue]='CHECKED NOT OK',[Description]='CHECKED NOT OK' where id=3

                                 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
