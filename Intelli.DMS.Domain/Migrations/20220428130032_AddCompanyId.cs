using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

ALTER View [dbo].[DocumentCheckOutView] As
SELECT
bi.[Id]
, bi.[FileName]
, DocVer.[Id] as VersionId
, isNull((
select top 1 cl.FirstName+' '+cl.LastName  from Clients cl
inner join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
),'-') [ClientName]
,case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, bi.BatchId
, bi.CompanyId
, docCheckOut.CreatedAt as CreatedOn 
, docCheckOut.UpdatedAt as LastModifiedOn
, docCheckOut.CreatedAt 
, docCheckOut.UpdatedAt
, docCheckOut.IsActive
,SysUser.Id As UserId
FROM [DocumentsCheckedOut] docCheckOut
inner join [BatchItems] bi on bi.Id = docCheckOut.BatchItemId
inner join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
inner join [DocumentVersion] DocVer on DocVer.Id = bi.DocumentVersionId
inner join [SystemUsers] SysUser on docCheckOut.SystemUserId = SysUser.Id


                                  ");

            migrationBuilder.Sql(@"

ALTER View[dbo].[DocumentReviewView] As
SELECT bi.Id
, bi.[FileName]
, dc.DocumentClassName
, dtc.DocumentTypeName
, isNull((
select top 1 cl.FirstName + ' ' + cl.LastName  from Clients cl
    inner
                                               join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
), '-')[ClientName]
, isNull((
select top 1 su.FullName from BatchMetaHistory bmh
inner
                         join SystemUsers su on bmh.SystemUserId = su.Id
where bmh.BatchId = bi.BatchId
),'-') [LastModifiedBy]
, case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, isNull((
select top 1 docAppHis.RoleId from DocumentApprovalHistory docAppHis
where docAppHis.BatchItemReference = bi.BatchItemReference And docAppHis.Approved = 0
),'-') [CurrentReviewRole]
, bi.BatchId
, bi.IsActive
, bi.CreatedAt
, bi.UpdatedAt
, bi.CompanyId
, DocVer.[LastModifiedBy] as LastModifiedById
,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
(case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
                (select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id = dco.SystemUserId

                            where dco.BatchItemId = bi.Id )end) as CheckedOutBy
FROM[BatchItems] bi
left outer join[BatchItemPages] bip on bi.Id = bip.BatchItemId
left outer join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
left outer join[DocumentClasses] dc on bi.DocumentClassId = dc.Id
left outer join[DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
left outer join[DocumentVersion] DocVer on DocVer.Id = bi.DocumentVersionId
left outer join[Batches] bat on bi.BatchId = bat.Id
join
(
    SELECT max(id) as id
    FROM[BatchItems]
    GROUP BY[BatchItemReference]
) bi2 on bi.id = bi2.id

                                ");

            migrationBuilder.Sql(@"

  ALTER View [dbo].[DocumentSearchView] As
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
                            , 
                            case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
                            , isNull((
                            select top 1 batstatus.ID from BatchItemStatuses batstatus
                            where batstatus.Id = bi.BatchItemStatusId
                            ),'-') [FileStatus]
                            , isNull((
                            select top 1 batstatus.BatchStatus from BatchStatuses batstatus
                            where batstatus.Id = bat.BatchStatusId
                            ),'-') [ClientStatus]
                            , bi.CreatedAt As CreatedOn
                            , bi.UpdatedAt As LastModifiedOn
                            , bi.CreatedAt 
                            , bi.UpdatedAt 
                            , bi.IsActive
							, bi.CompanyId
                            , DocVer.[LastModifiedBy] as LastModifiedById
                            ,(select string_agg( ISNULL(meta.FieldValue, ' '), ',') from dbo.BatchMeta meta where meta.BatchItemReference=bi.BatchItemReference) AS DocumentMetaData
                            ,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
                            (case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
                            						 	(select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id=dco.SystemUserId
                            							where dco.BatchItemId=bi.Id )end) as CheckedOutBy
                            FROM [BatchItems] bi
                            left outer join [BatchItemPages] bip on bi.Id = bip.BatchItemId
                            left outer join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
                            left outer join [DocumentVersion] docver on docver.Id = bi.DocumentVersionId
                            left outer join [DocumentClasses] dc on bi.DocumentClassId = dc.Id
                            left outer join [DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
                            left outer join [Batches] bat on bi.BatchId = bat.Id
                            join
                            (
                                SELECT max(id) as id
                                FROM [BatchItems]  
                                GROUP BY [BatchItemReference]
                            ) bi2 on bi.id = bi2.id

                                 ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
