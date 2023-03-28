using Intelli.DMS.Domain.Model;
using Intelli.DMS.Domain.Model.StoredProceduresOutput;
using Intelli.DMS.Domain.Model.Views;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intelli.DMS.Domain.Database
{
    /// <summary>
    /// The agent portal database context.
    /// </summary>
    public partial class DMSContext : IdentityDbContext<AspNetUser>
    {
        /// <summary>
        /// Gets or sets the batches count.
        /// Used for batches_count stored procedure output.
        /// </summary>
        public DbSet<BatchesCount> BatchesCount { get; set; }

        /// <summary>
        /// Gets or sets ApplyGDPR
        /// </summary>
        public DbSet<ApplyGDPR> ApplyGDPR { get; set; }

        /// <summary>
        /// Gets or sets SystemUsersView
        /// </summary>
        public virtual DbSet<SystemUserView> SystemUsersView { get; set; }

        /// <summary>
        /// Gets or sets ClientRepositoryView
        /// </summary>
        public virtual DbSet<ClientRepositoryView> ClientRepositoryView { get; set; }

        /// <summary>
        /// Gets or sets DocumentReviewView
        /// </summary>
        public virtual DbSet<DocumentReviewView> DocumentReviewView { get; set; }

        /// <summary>
        /// Gets or sets DocumentSearchView
        /// </summary>
        public virtual DbSet<DocumentSearchView> DocumentSearchView { get; set; }

        /// <summary>
        /// Gets or sets ContractSearchView
        /// </summary>
        public virtual DbSet<ContractSearchView> ContractSearchView { get; set; }

        /// <summary>
        /// Gets or sets DocumentCheckOutView
        /// </summary>
        public virtual DbSet<DocumentCheckOutView> DocumentCheckOutView { get; set; }

        /// <summary>
        /// Gets or sets BatchesDataTobeDeleted
        /// </summary>
        public virtual DbSet<BatchesDataTobeDeleted> BatchesDataTobeDeleted { get; set; }

        /// <summary>
        /// Gets or sets BatchesDataTobeDeleted
        /// </summary>
        public virtual DbSet<ClientsDataToBeDeleted> ClientsDataToBeDeleted { get; set; }

        /// <summary>
        /// Gets or sets ClientTag
        /// </summary>
        public virtual DbSet<ClientTag> ClientTag { get; set; }

        /// <summary>
        /// Gets or sets DocumentVersion
        /// </summary>
        public virtual DbSet<DocumentVersion> DocumentVersion { get; set; }
        
        /// <summary>
        /// Gets or sets DocumentTypeRoles
        /// </summary>
        public virtual DbSet<DocumentTypeRoles> DocumentTypeRoles { get; set; }

        /// <summary>
        /// Gets or sets DocumentApprovalHistory
        /// </summary>
        public virtual DbSet<DocumentApprovalHistory> DocumentApprovalHistory { get; set; }

        /// <summary>
        /// Gets or sets ClientView
        /// </summary>
        public virtual DbSet<ClientView> ClientView { get; set; }

        /// <summary>
        /// Gets or sets DocumentCheckOutLogsView
        /// </summary>
        public virtual DbSet<DocumentCheckOutLogsView> DocumentCheckOutLogsView { get; set; }

        /// <summary>
        /// Gets or sets DocumentTypeRoleAccess
        /// </summary>
        public virtual DbSet<DocumentTypeRoleAccess> DocumentTypeRoleAccess { get; set; }

        /// <summary>
        /// Gets or sets CompanyCustomFieldes
        /// </summary>
        public virtual DbSet<CompanyCustomField> CompanyCustomFieldes { get; set; }

        /// <summary>
        /// Gets or sets ClientCompanyCustomFieldValues
        /// </summary>
        public virtual DbSet<ClientCompanyCustomFieldValue> ClientCompanyCustomFieldValues { get; set; }
        
        /// <summary>
        /// Gets or sets Data Migration Histories
        /// </summary>
        public virtual DbSet<DataMigrationHistory> DataMigrationHistories { get; set; }

        /// <summary>
        /// Gets or sets Data Migration Histories
        /// </summary>
        public virtual DbSet<DMSOutLookAddInTempFile> DMSOutLookAddInTempFiles { get; set; }

        /// <summary>
        /// Gets or sets Data Migration Reord History
        /// </summary>
        public virtual DbSet<DataMigrationReordHistory> DataMigrationReordHistories { get; set; }

        

        /// <summary>
        /// On model creating partial.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BatchesCount>(x => x.HasNoKey());
            
            modelBuilder.Entity<ApplyGDPR>(x => x.HasNoKey());

            modelBuilder.Entity<SystemUserView>().ToTable(nameof(SystemUsersView), t => t.ExcludeFromMigrations());
           
            modelBuilder.Entity<ClientRepositoryView>().ToTable(nameof(ClientRepositoryView), t => t.ExcludeFromMigrations());
          
            modelBuilder.Entity<ContractSearchView>().ToTable(nameof(ContractSearchView), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<DocumentReviewView>().ToTable(nameof(DocumentReviewView), t => t.ExcludeFromMigrations());
           
            modelBuilder.Entity<DocumentCheckOutView>().ToTable(nameof(DocumentCheckOutView), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<DocumentSearchView>().ToTable(nameof(DocumentSearchView), t => t.ExcludeFromMigrations());
           
            modelBuilder.Entity<DocumentCheckOutLogsView>().ToTable(nameof(DocumentCheckOutLogsView), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<BatchesDataTobeDeleted>().ToTable(nameof(BatchesDataTobeDeleted), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<ClientsDataToBeDeleted>().ToTable(nameof(ClientsDataToBeDeleted), t => t.ExcludeFromMigrations());
            
            modelBuilder.Entity<ClientView>().ToTable(nameof(ClientView), t => t.ExcludeFromMigrations());
           
            modelBuilder.Entity<DataMigrationReordHistory>()
            .HasKey(o => new { o.TableName, o.RecordId });
        }
    }
}
