using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using WebApi.Models;

namespace WebApi1.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // loại bỏ tiền tố AspNet trước tên table
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            //SeedRole(modelBuilder);

            /* xử lý tham chiếu khóa chính, khóa ngoại */

            modelBuilder.Entity<City>()
                .HasKey(c => new { c.Id, c.CityCode })
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Hotels)
                .WithOne(h => h.City)
                .HasForeignKey(h => new { h.CityId, h.CityCode });

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Room>()
                .HasKey(r => r.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<RoomType>()
                .HasMany(rt => rt.Rooms)
                .WithOne(r => r.RoomType)
                .HasForeignKey(r => r.RoomTypeId);
            modelBuilder.Entity<HotelService>()
                .HasKey(s => s.ServiceId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<RoomPrice>()
                .HasKey(rp => new { rp.RoomId, rp.ValidFrom }).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomPrice)
                .WithOne(rp => rp.Room)
                .HasForeignKey<RoomPrice>(rp => rp.RoomId);
            modelBuilder.Entity<BookRoom>()
                .HasKey(br => new { br.RoomID, br.UserID, br.CheckInDate }).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            modelBuilder.Entity<Room>()
                 .HasOne(r => r.BookRoom)
                 .WithOne(br => br.Room)
                 .HasForeignKey<BookRoom>(br => br.RoomID);
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.BookRoom)
                .WithOne(br => br.User)
                .HasForeignKey<BookRoom>(br => br.UserID);
        }

        public DbSet<City> City { get; set; }

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<RoomType> RoomType { get; set; }

        public DbSet<HotelService> HotelService { get; set; }

        public DbSet<RoomPrice> RoomPrices { get; set; }

        public DbSet<BookRoom> BookRooms { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
