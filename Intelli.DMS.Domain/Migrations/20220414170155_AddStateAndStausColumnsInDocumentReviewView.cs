using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddStateAndStausColumnsInDocumentReviewView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                               DROP VIEW [dbo].[DocumentReviewView];
                    ");
            migrationBuilder.Sql(@"
Create View [dbo].[DocumentReviewView] As
SELECT bi.Id
, bip.[FileName]
, bip.[OriginalName] [OriginalFileName]
, dc.DocumentClassName
, dtc.DocumentTypeName
, cl.FirstName + ' ' + cl.LastName [ClientName]
, isNull((
select top 1 su.FullName from BatchMetaHistory bmh
inner join SystemUsers su on bmh.SystemUserId = su.Id
where bmh.BatchId = bi.BatchId
),'-') [LastModifiedBy]
, isNull((
select top 1 batstatus.BatchItemStatus from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [State]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, bi.BatchId
, bi.IsActive
, bi.CreatedAt
, bi.UpdatedAt
,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
(case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
						 	(select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id=dco.SystemUserId
							where dco.BatchItemId=bi.Id )end) as CheckedOutBy
FROM [BatchItems] bi
left outer join [BatchItemPages] bip on bi.Id = bip.BatchItemId
left outer join [DocumentClasses] dc on bi.DocumentClassId = dc.Id
left outer join [DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
left outer join [Clients] cl on bi.DocumentClassId = cl.Id
left outer join [Batches] bat on bi.BatchId = bat.Id
GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
