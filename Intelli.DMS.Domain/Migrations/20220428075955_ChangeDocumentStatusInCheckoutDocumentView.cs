using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class ChangeDocumentStatusInCheckoutDocumentView : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
