using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumentClassFieldUiLabelDataInBatchItemField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
 Update [DMS].[dbo].[BatchItemFields]
  Set [DocumentClassFieldUiLabel]=(Select UILabel from DocumentClassFields where id=[DocumentClassFieldId])
                                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
