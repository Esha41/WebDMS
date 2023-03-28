using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddBatchItemRefferenceColumnInBatchItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchItemRefference",
                table: "BatchItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(@"
	update [BatchItems] set [BatchItemRefference] = (
		select top 1 cast(bi.BatchId as varchar) + '_' + cast(bi.id as varchar) from BatchItems bi where bi.BatchId = [batchItems].BatchId
		order by bi.id desc
	)
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchItemRefference",
                table: "BatchItems");
        }
    }
}
