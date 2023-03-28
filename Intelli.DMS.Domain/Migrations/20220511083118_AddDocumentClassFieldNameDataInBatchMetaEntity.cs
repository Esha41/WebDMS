using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumentClassFieldNameDataInBatchMetaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
  Update [DMS].[dbo].[BatchMeta]
  Set [DocumentClassFieldName]=(Select UILabel from DocumentClassFields where Id=[DocumentClassFieldId])

                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
