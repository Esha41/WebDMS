using Intelli.DMS.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace Intelli.DMS.Domain.Database
{
    public partial class DMSAuditContext : DbContext
    {
        public DMSAuditContext()
        {
        }

        public DMSAuditContext(DbContextOptions<DMSAuditContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Development.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("AuditConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
