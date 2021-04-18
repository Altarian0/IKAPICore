using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IKAPICore.Models
{
    public partial class itkotdbContext : DbContext
    {
        public itkotdbContext()
        {
        }

        public itkotdbContext(DbContextOptions<itkotdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<AttractionComment> AttractionComments { get; set; }
        public virtual DbSet<AttractionImage> AttractionImages { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GidHelper> GidHelpers { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductComment> ProductComments { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourAttraction> TourAttractions { get; set; }
        public virtual DbSet<TourComment> TourComments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTour> UserTours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:itkotserver.database.windows.net,1433;Initial Catalog=itkotdb;Persist Security Info=False;User ID=dechimo;Password=error404&;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Agent_1");

                entity.ToTable("Agent");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent_Company1");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Agent)
                    .HasForeignKey<Agent>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent_Company");
            });

            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.ToTable("Attraction");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Attraction_Place");
            });

            modelBuilder.Entity<AttractionComment>(entity =>
            {
                entity.ToTable("AttractionComment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Text).HasColumnType("text");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.AttractionComments)
                    .HasForeignKey(d => d.AttractionId)
                    .HasConstraintName("FK_AttractionComment_Attraction");
            });

            modelBuilder.Entity<AttractionImage>(entity =>
            {
                entity.ToTable("AttractionImage");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.AttractionImages)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AttractionImages_Attraction");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<GidHelper>(entity =>
            {
                entity.ToTable("GidHelper");

                entity.Property(e => e.Gidid).HasColumnName("GIDId");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK_Product_Shop");
            });

            modelBuilder.Entity<ProductComment>(entity =>
            {
                entity.ToTable("ProductComment");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductComments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductComment_Product");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductImages_Product");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Telegram).HasMaxLength(50);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Shop_Place");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Tour_Tour");

                entity.HasOne(d => d.FromPlace)
                    .WithMany(p => p.TourFromPlaces)
                    .HasForeignKey(d => d.FromPlaceId)
                    .HasConstraintName("FK_Tours_Place");

                entity.HasOne(d => d.ToPlace)
                    .WithMany(p => p.TourToPlaces)
                    .HasForeignKey(d => d.ToPlaceId)
                    .HasConstraintName("FK_Tours_Place1");
            });

            modelBuilder.Entity<TourAttraction>(entity =>
            {
                entity.HasKey(e => new { e.TourId, e.AttractionId })
                    .HasName("PK_TourAttractions");

                entity.ToTable("TourAttraction");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.TourAttractions)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourAttractions_Attraction");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourAttractions)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourAttractions_Tours");
            });

            modelBuilder.Entity<TourComment>(entity =>
            {
                entity.ToTable("TourComment");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.TourComments)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_TourComment_User");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourComments)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourComments_Tours");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.AvatarImage).HasColumnType("image");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_User_Gender");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Userr_Role");
            });

            modelBuilder.Entity<UserTour>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TourId });

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.UserTours)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTours_Tours");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTours)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTours_Userr");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
