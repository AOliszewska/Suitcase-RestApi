using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public partial class VacationContext : DbContext
    {
        public VacationContext()
        {
            
        }
        
        public VacationContext(DbContextOptions<VacationContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<UserVacation> Users { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Suitcase> Suitcases { get; set; }
        public virtual DbSet<SuitcaseItem> SuitcaseItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("vacation")
                .HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<UserVacation>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pk");

                entity.ToTable("UserVacation");

                entity.Property(e => e.IdUser).UseIdentityColumn();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(120);
            });
            modelBuilder.Entity<Suitcase>(entity =>
            {
                entity.HasKey(e => e.IdSuitcase)
                    .HasName("Suitcase_pk");

                entity.Property(e => e.IdCity).IsRequired();
                entity.Property(e => e.IdUser).IsRequired();

                entity.ToTable("Suitcase");

                entity.Property(e => e.IdSuitcase).UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Suitcases)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users");
                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.Suitcases)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cities");
            });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("Country_pk");

                entity.ToTable("Country");

                entity.Property(e => e.IdCountry).UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.IdCity)
                    .HasName("CityId_pk");

                entity.ToTable("City");

                entity.Property(e => e.IdCity).UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.IdCountry);
                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("City_Country_pk");
            });
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem).HasName("Item_pk");
                entity.ToTable("Item");
                entity.Property(e => e.IdItem).UseIdentityColumn();
                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.IsPacked).IsRequired();
            });
            modelBuilder.Entity<SuitcaseItem>(entity =>
            {
                entity.HasKey(e => new {e.IdItem, e.IdSuitcase})
                    .HasName("SuitcaseItem_pk");
                entity.ToTable("SuitcaseItem");
                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.SuitcaseItems)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SuitcaseItem_Item");
                entity.HasOne(d => d.IdSuitcaseNavigation)
                    .WithMany(p => p.SuitcaseItems)
                    .HasForeignKey(p => p.IdSuitcase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SuitcaseItem_suitcase");

            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}