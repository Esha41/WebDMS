using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeBatchesDataTobeDeletedView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
 ALTER View [dbo].[BatchesDataTobeDeleted] As
								  SELECT B.*  
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.companies AS C 
												   ON B.companyid = C.id 
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 2 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.CreatedDate, Getdate()) > C.gdprdaystobekept )
											   ) )

");
			migrationBuilder.Sql(@"
 ALTER View [dbo].[BatchesDataTobeDeleted] As
								  SELECT B.*  
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.companies AS C 
												   ON B.companyid = C.id 
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 2 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.CreatedDate, Getdate()) > C.gdprdaystobekept )
											   ) )

");

			migrationBuilder.Sql(@"
  ALTER View [dbo].[ClientsDataToBeDeleted] As
								  SELECT B.* , C.id as cId
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.Clients AS C 
												   ON B.CustomerId = C.id
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 2 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.CreatedDate, Getdate()) > C.gdprdaystobekept )
											   ) 
		 
											 )
");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
