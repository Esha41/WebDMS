using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class updateClientView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
USE [A1_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ClientView]
AS
SELECT        cl.Id, cl.FirstName, cl.LastName, cl.JMBG, cl.IsActive, cl.CreatedAt, cl.UpdatedAt, cl.IsNotValidForTransaction, cl.Reason, cl.GdprdaysToBeKept, cl.ExternalId, cl.CompanyId, ISNULL
                             ((SELECT        TOP (1) BatchStatus
                                 FROM            dbo.BatchStatuses AS stat
                                 WHERE        (bat.BatchStatusId = Id)), 'Created') AS ClientStatus
FROM            dbo.Clients AS cl LEFT OUTER JOIN
                         dbo.Batches AS bat ON bat.CustomerId = cl.Id
GO



");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
