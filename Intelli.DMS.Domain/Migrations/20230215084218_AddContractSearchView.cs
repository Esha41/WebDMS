using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddContractSearchView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW [dbo].[ContractSearchView]
AS
SELECT        bi.Id, bi.BatchId AS RepositoryName, ISNULL
                             ((SELECT        TOP (1) cl.FirstName + ' ' + cl.LastName AS Expr1
                                 FROM            dbo.Clients AS cl INNER JOIN
                                                          dbo.Batches AS batch ON batch.CustomerId = cl.Id
                                 WHERE        (batch.Id = bi.BatchId)), '-') AS ClientName, dc.DocumentClassName, dtc.DocumentTypeName, bi.CreatedAt, bi.UpdatedAt, bi.IsActive, bi.CompanyId,
                             (SELECT DISTINCT string_agg(DocClasFields.UILabel + ':' + meta.FieldValue, ',') AS Expr1
                               FROM            dbo.BatchMeta AS meta INNER JOIN
                                                         dbo.DocumentClassFields AS DocClasFields ON meta.DocumentClassFieldId = DocClasFields.Id AND meta.BatchItemReference = bi.BatchItemReference
                               WHERE        (meta.DocumentVersionId =
                                                             (SELECT        MAX(DocumentVersionId) AS Expr1
                                                               FROM            dbo.BatchMeta AS meta
                                                               WHERE        (BatchItemReference = bi.BatchItemReference)))) AS ContractMetaData,
                             (SELECT        TOP (1) meta.FieldValue AS Expr1
                               FROM            dbo.BatchMeta AS meta LEFT OUTER JOIN
                                                         dbo.DocumentClassFields AS DocClasFields ON meta.DocumentClassFieldId = DocClasFields.Id
                               WHERE        (meta.DocumentVersionId =
                                                             (SELECT        MAX(DocumentVersionId) AS Expr1
                                                               FROM            dbo.BatchMeta AS meta
                                                               WHERE        (BatchItemReference = bi.BatchItemReference))) AND (meta.DocumentClassFieldId = 7 OR
                                                         meta.DocumentClassFieldId = 8)) AS BoxNumber
FROM            dbo.BatchItems AS bi LEFT OUTER JOIN
                         dbo.BatchItemPages AS bip ON bi.Id = bip.BatchItemId LEFT OUTER JOIN
                         dbo.BatchItemStatuses AS BatItemStatu ON BatItemStatu.ID = bi.BatchItemStatusId LEFT OUTER JOIN
                         dbo.DocumentVersion AS docver ON docver.Id = bi.DocumentVersionId LEFT OUTER JOIN
                         dbo.DocumentClasses AS dc ON bi.DocumentClassId = dc.Id LEFT OUTER JOIN
                         dbo.DocumentTypes AS dtc ON dc.DocumentTypeId = dtc.Id LEFT OUTER JOIN
                         dbo.Batches AS bat ON bi.BatchId = bat.Id INNER JOIN
                             (SELECT        MAX(Id) AS id
                               FROM            dbo.BatchItems
                               GROUP BY BatchItemReference) AS bi2 ON bi.Id = bi2.id
GO
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
