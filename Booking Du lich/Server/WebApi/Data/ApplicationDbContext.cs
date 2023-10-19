using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using WebApi.Models;

namespace WebApi1.Data
{
    public partial class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // loại bỏ tiền tố AspNet trước tên table
            //foreach ( var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if(entityType.GetTableName().StartsWith("AspNet"))
            //    {
            //        entityType.SetTableName(entityType.GetTableName().Substring(6));
            //    }
            //}

            //SeedRole(modelBuilder);

            /* xử lý tham chiếu khóa chính, khóa ngoại */       

            modelBuilder.Entity<City>()
                .HasKey(c => c.Id)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Agents)
                .WithOne(u => u.Hotel)
                .HasForeignKey(u => u.HotelId);
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Hotels)
                .WithOne(h => h.City)
                .HasForeignKey(h => h.CityId);

            modelBuilder.Entity<Room>()
                .HasKey(r => r.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        public DbSet<City> City { get; set; }

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Room> Room { get; set; }  

        public DbSet<Contact> Contact { get; set; }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole() { Name = "Agent", ConcurrencyStamp = "2", NormalizedName = "Agent" },
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
