using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeAfmColumnToText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter table Clients
                                   alter Column AFM nvarchar(9) not null
                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
