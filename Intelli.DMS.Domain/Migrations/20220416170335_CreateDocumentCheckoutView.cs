using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class CreateDocumentCheckoutView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Create View [dbo].[DocumentCheckOutView] As
SELECT
bi.[Id]
, bi.[FileName]
, DocVer.[Id] as VersionId
, isNull((
select top 1 cl.FirstName+' '+cl.LastName  from Clients cl
inner join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
),'-') [ClientName]
, isNull((
select top 1 batstatus.BatchItemStatus from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [State]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, bi.BatchId
, docCheckOut.CreatedAt 
, docCheckOut.UpdatedAt
, docCheckOut.IsActive
,SysUser.Id As UserId
FROM [DocumentsCheckedOut] docCheckOut
inner join [BatchItems] bi on bi.Id = docCheckOut.BatchItemId
inner join [DocumentVersion] DocVer on DocVer.Id = bi.DocumentVersionId
inner join [SystemUsers] SysUser on docCheckOut.SystemUserId = SysUser.Id
GO
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
