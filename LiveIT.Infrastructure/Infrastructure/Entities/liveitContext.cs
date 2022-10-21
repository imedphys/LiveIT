using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Entities
{
    public partial class liveitContext : DbContext
    {
        public liveitContext()
        {
        }

        public liveitContext(DbContextOptions<liveitContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccesibilityMenyIp> AccesibilityMenyIps { get; set; }
        public virtual DbSet<AccessibilityMenu> AccessibilityMenus { get; set; }
        public virtual DbSet<Catalogue> Catalogues { get; set; }
        public virtual DbSet<CatalogueType> CatalogueTypes { get; set; }
        public virtual DbSet<CatalogueTypeCatalogue> CatalogueTypeCatalogues { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RatingIp> RatingIps { get; set; }
        public virtual DbSet<Stakeholder> Stakeholders { get; set; }
        public virtual DbSet<StakeholderType> StakeholderTypes { get; set; }
        public virtual DbSet<StakeholderTypeStakeholder> StakeholderTypeStakeholders { get; set; }
        public virtual DbSet<SubType> SubTypes { get; set; }
        public virtual DbSet<SubTypeTool> SubTypeTools { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<TypeSubType> TypeSubTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=liveit.database.windows.net, 1433;Database=liveit;User ID=liveit;Password=medphysauth123@;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccesibilityMenyIp>(entity =>
            {
                entity.HasKey(e => e.AccessibilityMenuIpid);

                entity.ToTable("AccesibilityMenyIP");

                entity.Property(e => e.AccessibilityMenuIpid).HasColumnName("AccessibilityMenuIPID");

                entity.Property(e => e.AccesibilityMenuId).HasColumnName("AccesibilityMenuID");

                entity.Property(e => e.RemoteAddress)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AccessibilityMenu>(entity =>
            {
                entity.HasKey(e => e.AccesibilityMenuId);

                entity.ToTable("AccessibilityMenu");

                entity.Property(e => e.AccesibilityMenuId).HasColumnName("AccesibilityMenuID");
            });

            modelBuilder.Entity<Catalogue>(entity =>
            {
                entity.ToTable("Catalogue");

                entity.Property(e => e.CatalogueId).HasColumnName("CatalogueID");

                entity.Property(e => e.Author).HasMaxLength(250);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Link).HasMaxLength(750);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TargetPopulation).HasMaxLength(50);

                entity.Property(e => e.Theme).HasMaxLength(50);
            });

            modelBuilder.Entity<CatalogueType>(entity =>
            {
                entity.ToTable("CatalogueType");

                entity.Property(e => e.CatalogueTypeId).HasColumnName("CatalogueTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CatalogueTypeCatalogue>(entity =>
            {
                entity.ToTable("CatalogueTypeCatalogue");

                entity.Property(e => e.CatalogueTypeCatalogueId).HasColumnName("CatalogueTypeCatalogueID");

                entity.Property(e => e.CatalogueId).HasColumnName("CatalogueID");

                entity.Property(e => e.CatalogueTypeId).HasColumnName("CatalogueTypeID");

                entity.HasOne(d => d.Catalogue)
                    .WithMany(p => p.CatalogueTypeCatalogues)
                    .HasForeignKey(d => d.CatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogueTypeCatalogue_Catalogue");

                entity.HasOne(d => d.CatalogueType)
                    .WithMany(p => p.CatalogueTypeCatalogues)
                    .HasForeignKey(d => d.CatalogueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogueTypeCatalogue_CatalogueType");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.ToolId).HasColumnName("ToolID");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Tool");
            });

            modelBuilder.Entity<RatingIp>(entity =>
            {
                entity.HasKey(e => e.Ipid);

                entity.ToTable("RatingIP");

                entity.Property(e => e.Ipid).HasColumnName("IPId");

                entity.Property(e => e.RemoteAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubmittedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Stakeholder>(entity =>
            {
                entity.ToTable("Stakeholder");

                entity.Property(e => e.StakeholderId).HasColumnName("StakeholderID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FacebookUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LinkedinUrl)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TwitterUrl)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.YoutubeUrl)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Stakeholders)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stakeholder_Country");
            });

            modelBuilder.Entity<StakeholderType>(entity =>
            {
                entity.ToTable("StakeholderType");

                entity.Property(e => e.StakeholderTypeId).HasColumnName("StakeholderTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StakeholderTypeStakeholder>(entity =>
            {
                entity.ToTable("StakeholderTypeStakeholder");

                entity.Property(e => e.StakeholderTypeStakeholderId).HasColumnName("StakeholderTypeStakeholderID");

                entity.Property(e => e.StakeholderId).HasColumnName("StakeholderID");

                entity.Property(e => e.StakeholderTypeId).HasColumnName("StakeholderTypeID");

                entity.HasOne(d => d.Stakeholder)
                    .WithMany(p => p.StakeholderTypeStakeholders)
                    .HasForeignKey(d => d.StakeholderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StakeholderTypeStakeholderID_Stakeholder");

                entity.HasOne(d => d.StakeholderType)
                    .WithMany(p => p.StakeholderTypeStakeholders)
                    .HasForeignKey(d => d.StakeholderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StakeholderTypeStakeholderID_StakeholderType");
            });

            modelBuilder.Entity<SubType>(entity =>
            {
                entity.ToTable("SubType");

                entity.Property(e => e.SubTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("SubTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NameEl)
                    .HasMaxLength(150)
                    .HasColumnName("NameEL");

                entity.Property(e => e.NamePt)
                    .HasMaxLength(150)
                    .HasColumnName("NamePT");
            });

            modelBuilder.Entity<SubTypeTool>(entity =>
            {
                entity.HasKey(e => e.TypeToolId)
                    .HasName("PK_TypeTool");

                entity.ToTable("SubTypeTool");

                entity.Property(e => e.TypeToolId).HasColumnName("TypeToolID");

                entity.Property(e => e.SubTypeId).HasColumnName("SubTypeID");

                entity.Property(e => e.ToolId).HasColumnName("ToolID");

                entity.HasOne(d => d.SubType)
                    .WithMany(p => p.SubTypeTools)
                    .HasForeignKey(d => d.SubTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TypeTool_Tool");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.SubTypeTools)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTypeTool_Tool");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.ToTable("Tool");

                entity.Property(e => e.ToolId).HasColumnName("ToolID");

                entity.Property(e => e.Description).HasMaxLength(2500);

                entity.Property(e => e.DescriptionEl)
                    .HasMaxLength(2500)
                    .HasColumnName("DescriptionEL");

                entity.Property(e => e.DescriptionPt)
                    .HasMaxLength(2500)
                    .HasColumnName("DescriptionPT");

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.Link).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.RegistrationDateTime).HasColumnType("datetime");

                entity.Property(e => e.VideoUrl).HasMaxLength(250);
            });

            modelBuilder.Entity<Translation>(entity =>
            {
                entity.ToTable("Translation");

                entity.Property(e => e.TranslationId).ValueGeneratedNever();

                entity.Property(e => e.TextEl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("TextEL");

                entity.Property(e => e.TextEn)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("TextEN");

                entity.Property(e => e.TextPt)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("TextPT");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameEl)
                    .HasMaxLength(50)
                    .HasColumnName("NameEL");

                entity.Property(e => e.NamePt)
                    .HasMaxLength(50)
                    .HasColumnName("NamePT");
            });

            modelBuilder.Entity<TypeSubType>(entity =>
            {
                entity.ToTable("TypeSubType");

                entity.Property(e => e.TypeSubTypeId).HasColumnName("TypeSubTypeID");

                entity.Property(e => e.SubTypeId).HasColumnName("SubTypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.SubType)
                    .WithMany(p => p.TypeSubTypes)
                    .HasForeignKey(d => d.SubTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TypeSubType_SubType");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TypeSubTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TypeSubType_Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdentityId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IdentityID");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
