using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class AddDocumenTVersionEntityRelationshipsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    INSERT INTO [dbo].[DocumentVersion]
                                               ([Version]
                                               ,[BatchId]
                                               ,[IsActive]
                                               ,[CreatedAt]
                                               ,[UpdatedAt])
                                         VALUES
                                               (1,14,1,0,0)
                                    GO
                                    
                                    UPDATE BatchItems SET DocumentVersionId = 1
                                    UPDATE BatchItemPages SET DocumentVersionId = 1
                                    UPDATE BatchItemFields SET DocumentVersionId = 1
                                    UPDATE BatchMeta SET DocumentVersionId = 1
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
