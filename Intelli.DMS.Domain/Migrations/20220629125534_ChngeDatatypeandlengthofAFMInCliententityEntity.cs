using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChngeDatatypeandlengthofAFMInCliententityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" update c Set Afm=c.Id  from Clients c");
            migrationBuilder.Sql(@" ALTER TABLE Clients
                                    ALTER  COLUMN Afm int;");
            
            //migrationBuilder.AlterColumn<int>(
            //    name: "AFM",
            //    table: "Clients",
            //    type: "int",
            //    maxLength: 9,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(50)",
            //    oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
