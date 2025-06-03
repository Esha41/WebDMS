using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class SetDocumentSreachViewDocumentMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Alter View [dbo].[DocumentSearchView] As
SELECT 
bi.Id As Id
,bi.BatchId As RepositoryName
, bi.[FileName]
, isNull((
select top 1 cl.FirstName+''+cl.LastName  from Clients cl
inner join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
),'-') [ClientName]
,docver.Version As FileVersion
, dc.DocumentClassName
, dtc.DocumentTypeName

, isNull((
select top 1 su.FullName from BatchMetaHistory bmh
inner join SystemUsers su on bmh.SystemUserId = su.Id
where bmh.BatchId = bi.BatchId
),'-') [LastModifiedBy]
, isNull((
select top 1 batstatus.BatchItemStatus from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [DocumentState]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [FileStatus]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [ClientStatus]
, bi.CreatedAt As CreatedOn
, bi.UpdatedAt As LastModifiedOn
, bi.CreatedAt 
, bi.UpdatedAt 
, bi.IsActive
,(select string_agg( ISNULL(meta.FieldValue, ' '), ',') from dbo.BatchMeta meta where meta.FileName=bi.FileName) AS DocumentMetaData
,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
(case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
						 	(select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id=dco.SystemUserId
							where dco.BatchItemId=bi.Id )end) as CheckedOutBy
FROM [BatchItems] bi
left outer join [BatchItemPages] bip on bi.Id = bip.BatchItemId
left outer join [DocumentVersion] docver on docver.Id = bi.DocumentVersionId
left outer join [DocumentClasses] dc on bi.DocumentClassId = dc.Id
left outer join [DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
left outer join [Batches] bat on bi.BatchId = bat.Id
join
(
    SELECT max(id) as id
    FROM [BatchItems]  
    GROUP BY [FileName]
) bi2 on bi.id = bi2.id

GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
