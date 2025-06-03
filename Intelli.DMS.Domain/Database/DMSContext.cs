using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Intelli.DMS.Domain.Model;
using Microsoft.Extensions.Configuration;
using Intelli.DMS.Shared.DTO;
using Intelli.DMS.Api.Helpers;

#nullable disable

namespace Intelli.DMS.Domain.Database
{
    public partial class DMSContext
    {
        public IConfiguration _configuration;

        private static string _connectionString="";
        public DMSContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DMSContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DMSContext(DbContextOptions<DMSContext> options , IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if(_configuration != null)
                {
                  optionsBuilder.UseSqlServer(GetSqlConnectionString("DBConnections:DMSDB"));
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }
        /// <summary>
        /// Gets the sql connection string.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <returns>A string.</returns>
        private string GetSqlConnectionString(string sectionName)
        {
            SqlConnectionConfigurationDTO config = _configuration.GetSection(sectionName)
                                                                .Get<SqlConnectionConfigurationDTO>();
            return SqlConnectionHelper.ToConnectionString(config);
        }
        public virtual DbSet<AdvancedLogging> AdvancedLoggings { get; set; }
        public virtual DbSet<AdvancedSignatureCallHistory> AdvancedSignatureCallHistories { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<BatchItem> BatchItems { get; set; }
        public virtual DbSet<BatchItemField> BatchItemFields { get; set; }
        public virtual DbSet<BatchItemPage> BatchItemPages { get; set; }
        public virtual DbSet<BatchItemStatus> BatchItemStatuses { get; set; }
        public virtual DbSet<BatchMetaHistory> BatchMetaHistories { get; set; }
        public virtual DbSet<BatchMetum> BatchMeta { get; set; }
        public virtual DbSet<BatchSource> BatchSources { get; set; }
        public virtual DbSet<BatchSourceDocumentsSpecification> BatchSourceDocumentsSpecifications { get; set; }
        public virtual DbSet<BatchStatus> BatchStatuses { get; set; }
        public virtual DbSet<BopConfig> BopConfigs { get; set; }
        public virtual DbSet<BopDictionary> BopDictionaries { get; set; }
        public virtual DbSet<Bu> buses { get; set; }
        public virtual DbSet<CatalogNameProduct> CatalogNameProducts { get; set; }
        public virtual DbSet<ColumnPreference> ColumnPreferences { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyAbbyTemplate> CompanyAbbyTemplates { get; set; }
        public virtual DbSet<CompanyAbbyTemplateField> CompanyAbbyTemplateFields { get; set; }
        public virtual DbSet<CompanyFlow> CompanyFlows { get; set; }
        public virtual DbSet<CompanySigningDocument> CompanySigningDocuments { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DashboardMenu> DashboardMenus { get; set; }
        public virtual DbSet<DictionaryType> DictionaryTypes { get; set; }
        public virtual DbSet<DocumentClass> DocumentClasses { get; set; }
        public virtual DbSet<DocumentClassField> DocumentClassFields { get; set; }
        public virtual DbSet<DocumentClassFieldType> DocumentClassFieldTypes { get; set; }
        public virtual DbSet<DocumentRejectionReason> DocumentRejectionReasons { get; set; }
        public virtual DbSet<DocumentRejectionReasonCompany> DocumentRejectionReasonCompanies { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentsPerCompany> DocumentsPerCompanies { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<LivenessToken> LivenessTokens { get; set; }
        public virtual DbSet<Nlog> Nlogs { get; set; }
        public virtual DbSet<Ocrengine> Ocrengines { get; set; }
        public virtual DbSet<OcrenginesDocumentClass> OcrenginesDocumentClasses { get; set; }
        public virtual DbSet<PasswordHistory> PasswordHistories { get; set; }
        public virtual DbSet<ProgrammeList> ProgrammeLists { get; set; }
        public virtual DbSet<RoleScreen> RoleScreens { get; set; }
        public virtual DbSet<RoleScreenColumn> RoleScreenColumns { get; set; }
        public virtual DbSet<RoleScreenElement> RoleScreenElements { get; set; }
        public virtual DbSet<RulesXref> RulesXrefs { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<ScreenColumn> ScreenColumns { get; set; }
        public virtual DbSet<ScreenElement> ScreenElements { get; set; }
        public virtual DbSet<ServiceLastExcecution> ServiceLastExcecutions { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<StationVariable> StationVariables { get; set; }
        public virtual DbSet<StationVariableType> StationVariableTypes { get; set; }
        public virtual DbSet<SystemRole> SystemRoles { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserCountry> SystemUserCountries { get; set; }
        public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; }
        public virtual DbSet<TblMissingTemplateField> TblMissingTemplateFields { get; set; }
        public virtual DbSet<TempBatchPage> TempBatchPages { get; set; }
        public virtual DbSet<UserCompany> UserCompanies { get; set; }
        public virtual DbSet<UserPreference> UserPreferences { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<DocumentsCheckedOut> DocumentsCheckedOut { get; set; }
        public virtual DbSet<DocumentsCheckedOutLog> DocumentsCheckedOutLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdvancedLogging>(entity =>
            {
                entity.ToTable("AdvancedLogging");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Browser).HasMaxLength(100);

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Device).HasMaxLength(500);

                entity.Property(e => e.ExitDate).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .HasColumnName("IP");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestTime).HasColumnType("datetime");

                entity.Property(e => e.RequestUrl)
                    .HasMaxLength(3000)
                    .HasColumnName("RequestURL");

                entity.Property(e => e.ResponceStatus).HasMaxLength(50);

                entity.Property(e => e.ResponceTime).HasColumnType("datetime");

                entity.Property(e => e.System).HasMaxLength(100);
            });

            modelBuilder.Entity<AdvancedSignatureCallHistory>(entity =>
            {
                entity.ToTable("AdvancedSignatureCallHistory");

                entity.HasIndex(e => e.CompanyId, "IX_AdvancedSignatureCallHistory_CompanyId");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AdvancedSignatureCallHistories)
                    .HasForeignKey(d => d.CompanyId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.UserName, "IX_AspNetUsers")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "IX_AspNetUsers_1")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.HasIndex(e => e.SystemUserId, "IX_AspNetUsers_SystemUserId");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.AuditUser)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ChangedColumns).IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.KeyValues).IsRequired();

                entity.Property(e => e.NewValues).IsRequired();

                entity.Property(e => e.OldValues).IsRequired();

                entity.Property(e => e.TableName).IsRequired();
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasIndex(e => e.BatchSourceId, "IX_Batches_BatchSourceId");

                entity.HasIndex(e => e.BatchStatusId, "IX_Batches_BatchStatusId");

                entity.HasIndex(e => e.BusinessUnitId, "IX_Batches_BusinessUnitId");

                entity.HasIndex(e => e.LockedByNavigationId, "IX_Batches_LockedByNavigationId");

                entity.Property(e => e.AppliedGdpr)
                    .HasColumnName("AppliedGDPR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BatchSourceId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentOtp).HasColumnName("CurrentOTP");

                entity.Property(e => e.OtpvalidUntil)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPValidUntil");

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");

                entity.Property(e => e.RecognizedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RetriesCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartProcessDate).HasColumnType("datetime");

                entity.Property(e => e.VerifiedEndDate).HasColumnType("datetime");

                entity.Property(e => e.VerifiedStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_BatchSources");

                entity.HasOne(d => d.BatchStatus)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BatchStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batches_BatchStatuses1");

                entity.HasOne(d => d.BusinessUnit)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BusinessUnitId)
                    .HasConstraintName("FK_Batches_BUs");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Batches_Customers");
            });

            modelBuilder.Entity<BatchItem>(entity =>
            {
                entity.HasIndex(e => e.BatchId, "IX_BatchItems_BatchId");

                entity.HasIndex(e => e.BatchItemStatusId, "IX_BatchItems_BatchItemStatusId");

                entity.HasIndex(e => e.DocumentClassId, "IX_BatchItems_DocumentClassId");

                entity.HasIndex(e => e.ParentId, "IX_BatchItems_ParentId");

                entity.Property(e => e.OccuredAt).HasColumnType("datetime");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItems_Batches");

                entity.HasOne(d => d.BatchItemStatus)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.BatchItemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItems_BatchItemStatuses");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.BatchItems)
                    .HasForeignKey(d => d.DocumentClassId)
                    .HasConstraintName("FK_BatchItems_DocumentClasses");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_BatchItems_BatchItems_PARENT");
            });

            modelBuilder.Entity<BatchItemField>(entity =>
            {
                entity.HasIndex(e => e.BatchItemId, "IX_BatchItemFields_BatchItemId");

                entity.HasIndex(e => e.DictionaryValueId, "IX_BatchItemFields_DictionaryValueId");

                entity.HasIndex(e => e.DocumentClassFieldId, "IX_BatchItemFields_DocumentClassFieldId");

                entity.Property(e => e.DictionaryValueIdOld).HasColumnName("DictionaryValueId_old");

                entity.Property(e => e.IsLast)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RegisteredFieldValue).HasMaxLength(2000);

                entity.Property(e => e.RegisteredFieldValueOld)
                    .HasMaxLength(255)
                    .HasColumnName("RegisteredFieldValue_old");

                entity.HasOne(d => d.BatchItem)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.BatchItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemFields_BatchItems");

                entity.HasOne(d => d.DictionaryValue)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.DictionaryValueId)
                    .HasConstraintName("FK_BatchItemFields_BopDictionaries");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.BatchItemFields)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemFields_DocumentClassFields");
            });

            modelBuilder.Entity<BatchItemPage>(entity =>
            {
                entity.HasIndex(e => e.BatchItemId, "IX_BatchItemPages_BatchItemId");

                entity.HasIndex(e => new { e.FileName, e.BatchItemId }, "UX_BatchItemPages_FileName")
                    .IsUnique();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BatchItem)
                    .WithMany(p => p.BatchItemPages)
                    .HasForeignKey(d => d.BatchItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchItemPages_BatchItems");
            });

            modelBuilder.Entity<BatchItemStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchItemStatusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("BatchItemStatus");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<BatchMetaHistory>(entity =>
            {
                entity.ToTable("BatchMetaHistory");

                entity.HasIndex(e => e.BatchId, "IX_BatchMetaHistory_BatchId");

                entity.Property(e => e.CurrentValues).IsRequired();

                entity.Property(e => e.OccuredAt).HasColumnType("datetime");

                entity.Property(e => e.PreviousValues).IsRequired();

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchMetaHistories)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchMetaHistory_Batches");
            });

            modelBuilder.Entity<BatchMetum>(entity =>
            {
                entity.HasIndex(e => e.BatchId, "IX_BatchMeta_BatchId");

                entity.HasIndex(e => e.DictionaryValueId, "IX_BatchMeta_DictionaryValueId");

                entity.HasIndex(e => e.DocumentClassFieldId, "IX_BatchMeta_DocumentClassFieldId");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchMeta)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchMeta_Batches");

                entity.HasOne(d => d.DictionaryValue)
                    .WithMany(p => p.BatchMeta)
                    .HasForeignKey(d => d.DictionaryValueId)
                    .HasConstraintName("FK_BatchMeta_Dictionaries");

                entity.HasOne(d => d.DocumentClassField)
                    .WithMany(p => p.BatchMeta)
                    .HasForeignKey(d => d.DocumentClassFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchMeta_DocumentClassFields");
            });

            modelBuilder.Entity<BatchSource>(entity =>
            {
                entity.Property(e => e.BatchSourceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BatchSource");

                entity.Property(e => e.BatchSourceCode).HasMaxLength(10);

                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BatchSourceDocumentsSpecification>(entity =>
            {
                entity.HasIndex(e => e.BatchSourceId, "IX_BatchSourceDocumentsSpecifications_BatchSourceId");

                entity.HasIndex(e => e.DocumentClassId, "IX_BatchSourceDocumentsSpecifications_DocumentClassId");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.BatchSourceDocumentsSpecifications)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceDocumentsSpecifications_BatchSources");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.BatchSourceDocumentsSpecifications)
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceDocumentsSpecifications_DocumentClasses");
            });

            modelBuilder.Entity<BatchStatus>(entity =>
            {
                entity.Property(e => e.BatchStatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("BatchStatus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BopConfig>(entity =>
            {
                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Setting)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BopDictionary>(entity =>
            {
                entity.HasIndex(e => new { e.DictionaryTypeId, e.Value }, "IX_Dictionaries")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.DictionaryType)
                    .WithMany(p => p.BopDictionaries)
                    .HasForeignKey(d => d.DictionaryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dictionaries_DictionaryTypes");
            });

            modelBuilder.Entity<Bu>(entity =>
            {
                entity.ToTable("BUs");
            });

            modelBuilder.Entity<CatalogNameProduct>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ColumnPreference>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ColumnPreferences_ScreenId");

                entity.HasIndex(e => e.SystemUserId, "IX_ColumnPreferences_SystemUserId");

                entity.Property(e => e.ColumnName).HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ColumnPreferences)
                    .HasForeignKey(d => d.ScreenId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ColumnPreferences)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<Company>(entity =>
            {


                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(200);



                entity.Property(e => e.GdprdaysToBeKept).HasColumnName("GDPRDaysToBeKept");



            });

            modelBuilder.Entity<CompanyAbbyTemplate>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.CompanyId, "IX_CompanyAbbyTemplates_CompanyID");

                entity.HasIndex(e => e.DocumentClassId, "IX_CompanyAbbyTemplates_DocumentClassID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DocumentClassId).HasColumnName("DocumentClassID");

                entity.HasOne(d => d.Company)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyAbbyTemplates_Companies");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany()
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyAbbyTemplates_DocumentClasses");
            });

            modelBuilder.Entity<CompanyAbbyTemplateField>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.CompanyFlowId, "IX_CompanyAbbyTemplateFields_CompanyFlowId");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DocumentClassFieldId).HasColumnName("DocumentClassFieldID");

                entity.HasOne(d => d.CompanyFlow)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyFlowId)
                    .HasConstraintName("FK__CompanyAb__Compa__3587F3E0");
            });

            modelBuilder.Entity<CompanyFlow>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FlowName).HasMaxLength(50);
            });

            modelBuilder.Entity<CompanySigningDocument>(entity =>
            {
                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Code2D)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Code3D)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.MobileCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.JMBG, "IX_Customer_JMBG")
                    .IsUnique();

                entity.Property(e => e.JMBG)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("JMBG");



                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DashboardMenu>(entity =>
            {
                entity.ToTable("DashboardMenu");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ViewName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DictionaryType>(entity =>
            {
                entity.HasIndex(e => e.DictionaryTypeName, "IX_DictionaryTypes")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.Property(e => e.DictionaryTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DictionaryType");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");
            });

            modelBuilder.Entity<DocumentClass>(entity =>
            {
                entity.HasIndex(e => e.DocumentTypeId, "IX_DocumentClasses_DocumentTypeId");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.Property(e => e.DocumentClassName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DocumentClassCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentClasses)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentClasses_DocumentTypes");
            });

            modelBuilder.Entity<DocumentClassField>(entity =>
            {
                entity.HasIndex(e => e.DictionaryTypeId, "IX_DocumentClassFields_DictionaryTypeId");

                entity.HasIndex(e => e.DocumentClassFieldTypeId, "IX_DocumentClassFields_DocumentClassFieldTypeId");

                entity.HasIndex(e => e.DocumentClassId, "IX_DocumentClassFields_DocumentClassId");


                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.Property(e => e.DocumentClassFieldTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsMandatory)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Uilabel)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("UILabel")
                    .HasDefaultValueSql("('')");


                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.HasOne(d => d.DictionaryType)
                    .WithMany(p => p.DocumentClassFields)
                    .HasForeignKey(d => d.DictionaryTypeId)
                    .HasConstraintName("FK_DocumentClassFields_DictionaryTypes");

                entity.HasOne(d => d.DocumentClassFieldType)
                    .WithMany(p => p.DocumentClassFields)
                    .HasForeignKey(d => d.DocumentClassFieldTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentClassFields_DocumentClassFieldTypes");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.DocumentClassFields)
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentClassFields_DocumentClasses");
            });

            modelBuilder.Entity<DocumentClassFieldType>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(CONVERT([bigint],(0)))");
            });

            modelBuilder.Entity<DocumentRejectionReason>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(70);

                entity.Property(e => e.Descr).HasMaxLength(70);
            });

            modelBuilder.Entity<DocumentRejectionReasonCompany>(entity =>
            {
                entity.ToTable("DocumentRejectionReasonCompany");

                entity.HasIndex(e => e.CompanyId, "IX_DocumentRejectionReasonCompany_CompanyId");

                entity.HasIndex(e => e.DocumentRejectionReasonId, "IX_DocumentRejectionReasonCompany_DocumentRejectionReasonId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.DocumentRejectionReasonCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentRejectionReasonCompany_Companies");

                entity.HasOne(d => d.DocumentRejectionReason)
                    .WithMany(p => p.DocumentRejectionReasonCompanies)
                    .HasForeignKey(d => d.DocumentRejectionReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentRejectionReasonCompany_DocumentRejectionReasons");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeName).HasMaxLength(50);
                entity.Property(e=>e.DocumentTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<DocumentsPerCompany>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_DocumentsPerCompanies_CompanyId");

                entity.HasIndex(e => e.DocumentClassId, "IX_DocumentsPerCompanies_DocumentClassId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.DocumentsPerCompanies)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.DocumentsPerCompanies)
                    .HasForeignKey(d => d.DocumentClassId);
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Comments).HasMaxLength(50);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("License");
            });

            modelBuilder.Entity<LivenessToken>(entity =>
            {
                entity.Property(e => e.En)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumberResult).HasMaxLength(5);
            });

            modelBuilder.Entity<Nlog>(entity =>
            {
                entity.ToTable("NLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassName).HasMaxLength(100);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LoggedOn).HasColumnType("int");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Ocrengine>(entity =>
            {
                entity.ToTable("OCREngines");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OcrenginesDocumentClass>(entity =>
            {
                entity.ToTable("OCREnginesDocumentClasses");

                entity.HasIndex(e => e.DocumentClassId, "IX_OCREnginesDocumentClasses_DocumentClassId");

                entity.HasIndex(e => e.OcrengineId, "IX_OCREnginesDocumentClasses_OCREngineId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OcrengineDocumentClassCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("OCREngineDocumentClassCode");

                entity.Property(e => e.OcrengineId).HasColumnName("OCREngineId");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.OcrenginesDocumentClasses)
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OCREnginesDocumentClasses_DocumentClasses");

                entity.HasOne(d => d.Ocrengine)
                    .WithMany(p => p.OcrenginesDocumentClasses)
                    .HasForeignKey(d => d.OcrengineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OCREnginesDocumentClasses_OCREngines");
            });

            modelBuilder.Entity<PasswordHistory>(entity =>
            {
                entity.ToTable("PasswordHistory");

                entity.HasIndex(e => e.SystemUserId, "IX_PasswordHistory_SystemUserId");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.PasswordHistories)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<ProgrammeList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Programme_List");

                entity.Property(e => e.AgreedPower)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Agreed_Power");

                entity.Property(e => e.DayCharge)
                    .HasMaxLength(150)
                    .HasColumnName("Day_Charge");

                entity.Property(e => e.HronProgrammeApplicationForm)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("Hron_Programme_ApplicationForm");

                entity.Property(e => e.NightCharge)
                    .HasMaxLength(50)
                    .HasColumnName("Night_Charge");
            });

            modelBuilder.Entity<RoleScreen>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_RoleScreens_ScreenId");

                entity.HasIndex(e => e.SystemRoleId, "IX_RoleScreens_SystemRoleId");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.RoleScreens)
                    .HasForeignKey(d => d.ScreenId);

                entity.HasOne(d => d.SystemRole)
                    .WithMany(p => p.RoleScreens)
                    .HasForeignKey(d => d.SystemRoleId);
            });

            modelBuilder.Entity<RoleScreenColumn>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleScreenColumns_RoleId");

                entity.HasIndex(e => e.ScreenColumnId, "IX_RoleScreenColumns_ScreenColumnId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleScreenColumns)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.ScreenColumn)
                    .WithMany(p => p.RoleScreenColumns)
                    .HasForeignKey(d => d.ScreenColumnId);
            });

            modelBuilder.Entity<RoleScreenElement>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleScreenElements_RoleId");

                entity.HasIndex(e => e.ScreenElementId, "IX_RoleScreenElements_ScreenElementId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleScreenElements)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.ScreenElement)
                    .WithMany(p => p.RoleScreenElements)
                    .HasForeignKey(d => d.ScreenElementId);
            });

            modelBuilder.Entity<RulesXref>(entity =>
            {
                entity.ToTable("Rules_xref");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DocClassFieldId).HasColumnName("DocClassFieldID");

                entity.Property(e => e.Formula)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.Property(e => e.ScreenName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ScreenColumn>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ScreenColumns_ScreenId");

                entity.Property(e => e.ColumnName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScreenColumns)
                    .HasForeignKey(d => d.ScreenId);
            });

            modelBuilder.Entity<ScreenElement>(entity =>
            {
                entity.HasIndex(e => e.ScreenId, "IX_ScreenElements_ScreenId");

                entity.Property(e => e.ScreenElementName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScreenElements)
                    .HasForeignKey(d => d.ScreenId);
            });

            modelBuilder.Entity<ServiceLastExcecution>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ServiceLastExcecution");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<StationVariable>(entity =>
            {
                entity.HasIndex(e => new { e.StationId, e.StationVariableTypeId }, "IX_StationVariables")
                    .IsUnique()
                    .HasFilter("([StationId] IS NOT NULL)");

                entity.HasIndex(e => e.StationVariableTypeId, "IX_StationVariables_StationVariableTypeId");

                entity.Property(e => e.VariableValue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.StationVariables)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_StationVariables_Stations");

                entity.HasOne(d => d.StationVariableType)
                    .WithMany(p => p.StationVariables)
                    .HasForeignKey(d => d.StationVariableTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationVariables_StationVariableTypes");
            });

            modelBuilder.Entity<StationVariableType>(entity =>
            {
                entity.Property(e => e.Comments).HasMaxLength(250);

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.StationVariableTypeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("StationVariableType");

                entity.Property(e => e.SupportsGlobal)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SystemRole>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_SystemRoles_CompanyId");

                entity.HasIndex(e => new { e.Name, e.CompanyId }, "UC_SystemRoles_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SystemRoles)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_SystemRoles_company");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.HasIndex(e => e.Email, "UC_SystemUsers_Email")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Jmbg).HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUserCountry>(entity =>
            {
                entity.HasIndex(e => e.CountryId, "IX_SystemUserCountries_CountryId");

                entity.HasIndex(e => e.SystemUserId, "IX_SystemUserCountries_SystemUserId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SystemUserCountries)
                    .HasForeignKey(d => d.CountryId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserCountries)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<SystemUserRole>(entity =>
            {
                entity.ToTable("SystemUserRole");

                entity.HasIndex(e => e.SystemRoleId, "IX_SystemUserRoles_SystemRoleId");

                entity.HasIndex(e => e.SystemUserId, "IX_SystemUserRoles_SystemUserId");

                entity.HasOne(d => d.SystemRole)
                    .WithMany(p => p.SystemUserRoles)
                    .HasForeignKey(d => d.SystemRoleId);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserRoles)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<TblMissingTemplateField>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblMissingTemplateFields");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DocumentClassFieldId).HasColumnName("DocumentClassFieldID");
            });

            modelBuilder.Entity<TempBatchPage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.KeyFileName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasIndex(e => e.CompanyId, "IX_UserCompanies_CompanyId");

                entity.HasIndex(e => e.SystemUserId, "IX_UserCompanies_SystemUserId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companyies_Companyid");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.UserCompanies)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemUserId");
            });

            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.HasIndex(e => e.SystemUserId, "IX_UserPreferences_SystemUserId");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.UserPreferences)
                    .HasForeignKey(d => d.SystemUserId);
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.SystemUserId);

                entity.HasIndex(e => e.SystemUserId, "IX_UserSessions_SystemUserId");

                entity.Property(e => e.SystemUserId).ValueGeneratedNever();

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.SystemUser)
                    .WithOne(p => p.UserSession)
                    .HasForeignKey<UserSession>(d => d.SystemUserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
