using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class CustomerIdMustBeEnterInBatchhEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    ALTER TABLE dbo.[Batches] DROP CONSTRAINT FK_Batches_Customers;
                    
                    UPDATE [Batches] SET [CustomerId] = 1 WHERE[CustomerId] IS NULL;
                    
                    DROP INDEX IX_Batches_CustomerId ON [Batches];
                    
                    ALTER TABLE [Batches] ALTER COLUMN [CustomerId] int NOT NULL;
                    
                    ALTER TABLE [Batches]
                    ADD CONSTRAINT FK_Batches_Customers
                    FOREIGN KEY ([CustomerId]) REFERENCES [Clients]([Id]);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
