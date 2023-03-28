using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class CreateDocumentSearchView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW [dbo].[DocumentSearchView]
AS
SELECT                 dbo.BatchItemPages.Id AS Id, cast(dbo.Batches.Id AS varchar(100)) RepositoryName, dbo.Batches.CreatedAt CreatedOn, dbo.Batches.UpdatedAt AS LastModifiedOn,  (select CONCAT(FIRSTNAME, ' ', LASTNAME) FROM dbo.Clients where dbo.Clients.Id=dbo.Batches.CustomerId) AS ClientName, dbo.BatchItemPages.FileName, 
                           dbo.BatchItemStatuses.BatchItemStatus AS FileStatus, '' AS DocumentState,
						cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = dbo.BatchItems.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
						 (case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = dbo.BatchItems.Id), 0) when 0 then null else
						 	(select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id=dco.SystemUserId
							where dco.BatchItemId=dbo.BatchItems.Id )end) as CheckedOutBy,
							(select string_agg( ISNULL(meta.FieldValue, ' '), ',') from dbo.BatchMeta meta where meta.BatchId=dbo.Batches.Id) AS DocumentMetaData, 
							 0 AS FileVersion, '' AS ClientStatus,
						dbo.BatchItemPages.IsActive, dbo.BatchItemPages.CreatedAt, dbo.BatchItemPages.UpdatedAt
							  
FROM            dbo.Batches INNER JOIN
                         dbo.Clients ON dbo.Batches.CustomerId = dbo.Clients.Id INNER JOIN
                         dbo.BatchItems ON dbo.Batches.Id = dbo.BatchItems.BatchId INNER JOIN
                         dbo.BatchItemPages ON dbo.BatchItems.Id = dbo.BatchItemPages.BatchItemId INNER JOIN
                         dbo.BatchItemStatuses ON dbo.BatchItems.BatchItemStatusId = dbo.BatchItemStatuses.ID 
GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
