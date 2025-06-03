using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Intelli.DMS.Domain.OLDDBModel
{
    public partial class DMSOLDDBContext : DbContext
    {
        public static string ConnectionString = "";
        public DMSOLDDBContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DMSOLDDBContext(DbContextOptions<DMSOLDDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filing> Filings { get; set; }
        public virtual DbSet<FilingCategory> FilingCategories { get; set; }
        public virtual DbSet<PelaXr> PelaXrs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Filing>(entity =>
            {
                entity.ToTable("Filing");

                entity.Property(e => e.FilingId).HasColumnName("FilingID");

                entity.Property(e => e.FilingFolder)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FilingInsertDate).HasColumnType("datetime");

                entity.Property(e => e.FilingPathname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FilingUserInsert).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FilingXacode)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("FilingXACode");
            });

            modelBuilder.Entity<FilingCategory>(entity =>
            {
                entity.Property(e => e.FilingCategoryId).HasColumnName("FilingCategoryID");

                entity.Property(e => e.FilingCategoryDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PelaXr>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PELA_XR");

                entity.Property(e => e.PelEpon)
                    .HasMaxLength(50)
                    .HasColumnName("PEL_EPON");

                entity.Property(e => e.PelOnom)
                    .HasMaxLength(29)
                    .HasColumnName("PEL_ONOM");

                entity.Property(e => e.PelProf)
                    .HasMaxLength(4)
                    .HasColumnName("PEL_PROF");

                entity.Property(e => e.PelXaas)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PEL_XAAS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
