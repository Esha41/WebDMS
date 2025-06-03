using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumentsReviewView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Create View [dbo].[DocumentReviewView] As
SELECT bi.Id
, bip.[FileName]
, bip.[OriginalName] [OriginalFileName]
, dc.DocumentClassName
, dtc.DocumentTypeName
, cl.FirstName + ' ' + cl.LastName [ClientName]
, com.CompanyName
, isNull((
select top 1 su.FullName from BatchMetaHistory bmh
inner join SystemUsers su on bmh.SystemUserId = su.Id
where bmh.BatchId = bi.BatchId
),'-') [LastModifiedBy]
, bi.BatchId
, bi.IsActive
, bi.CreatedAt
, bi.UpdatedAt
FROM [BatchItems] bi
left outer join [BatchItemPages] bip on bi.Id = bip.BatchItemId
left outer join [DocumentClasses] dc on bi.DocumentClassId = dc.Id
left outer join [DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
left outer join [Clients] cl on bi.DocumentClassId = cl.Id
left outer join [Batches] bat on bi.BatchId = bat.Id
left outer join [Companies] com on bat.CompanyId = com.Id
GO
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
