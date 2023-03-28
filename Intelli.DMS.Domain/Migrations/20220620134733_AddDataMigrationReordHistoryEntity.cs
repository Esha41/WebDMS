using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDataMigrationReordHistoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataMigrationReordHistories",
                columns: table => new
                {
                    TableName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecordId = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataMigrationReordHistories", x => new { x.TableName, x.RecordId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataMigrationReordHistories");
        }
    }
}
