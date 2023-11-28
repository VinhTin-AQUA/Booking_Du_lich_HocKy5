using System;
using System.Collections.Generic;
using Booking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Booking.Data;

public partial class BookingContext : IdentityDbContext<AppUser>
{
    public BookingContext(DbContextOptions<BookingContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);

        // loại bỏ tiền tố AspNet trước tên table
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        modelBuilder.Entity<City>()
                .HasKey(c => new { c.Id })
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity<Hotel>()
            .HasKey(h => h.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity<City>()
            .HasMany(c => c.Hotels)
            .WithOne(h => h.City)
            .HasForeignKey(h => new { h.CityId });

        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);
        modelBuilder.Entity<Hotel>()
            .HasOne(h => h.Poster)
            .WithMany(p => p.PostHotels)
            .HasForeignKey(p => p.PosterID);

        modelBuilder.Entity<Hotel>()
            .HasOne(h => h.Approver)
            .WithMany(p => p.ApprovalHotels)
            .HasForeignKey(p => p.ApproverID);

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
            .HasMany(r => r.RoomPrices)
            .WithOne(rp => rp.Room)
            .HasForeignKey(rp => rp.RoomId);

        modelBuilder.Entity<BookRoom>()
            .HasKey(br => new { br.RoomID, br.UserID, br.CheckInDate }).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        modelBuilder.Entity<Room>()
             .HasOne(r => r.BookRoom)
             .WithOne(br => br.Room)
             .HasForeignKey<BookRoom>(br => br.RoomID);
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.BookRoom)
            .WithOne(br => br.User)
            .HasForeignKey<BookRoom>(br => br.UserID);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity<BusinessPartner>()
            .HasKey(b => b.Id).HasAnnotation("SqlServer:ValueGenerationStrategy",
              SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity<BusinessPartner>()
            .HasMany(bp => bp.PartnerUser)
            .WithOne(b => b.BusinessPartner)
            .HasForeignKey(b => b.PartnerId);

        modelBuilder.Entity<Tour>()
            .HasKey(t => t.TourId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        modelBuilder.Entity<City>()
            .HasMany(c => c.Tours)
            .WithOne(t => t.City)
            .HasForeignKey(t => new { t.CityId });
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Poster)
            .WithMany(p => p.PostTours)
            .HasForeignKey(t => t.PosterID);
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Approver)
            .WithMany(a => a.ApprovalTours)
            .HasForeignKey(t => t.ApproverID);

        modelBuilder.Entity<HasService>()
            .HasKey(hs => new { hs.ServiceID, hs.HotelID });
        modelBuilder.Entity<HasService>()
            .HasOne(hs => hs.Service)
            .WithMany(s => s.HasServices)
            .HasForeignKey(s => s.ServiceID);
        modelBuilder.Entity<HasService>()
            .HasOne(hs => hs.Hotel)
            .WithMany(h => h.HasServices)
            .HasForeignKey(h => h.HotelID);

        modelBuilder.Entity<Package>()
            .HasKey(p => p.PackageID);
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Packages)
            .WithOne(p => p.Tour)
            .HasForeignKey(p => p.TourID);

        modelBuilder.Entity<PackagePrice>()
            .HasKey(pp => pp.PriceId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        modelBuilder.Entity<Package>()
            .HasOne(p => p.PackagePrice)
            .WithOne(pp => pp.Package)
            .HasForeignKey<PackagePrice>(pp => pp.PackageId);

        modelBuilder.Entity<BookTour>()
            .HasKey(bt => new { bt.PackageId, bt.UserID, bt.DepartureDate });

        modelBuilder.Entity<Package>()
             .HasOne(p => p.BookTour)
             .WithOne(bt => bt.Package)
             .HasForeignKey<BookTour>(bt => bt.PackageId);
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.BookTour)
            .WithOne(bt => bt.User)
            .HasForeignKey<BookTour>(bt => bt.UserID);

        modelBuilder.Entity<TouristAttraction>().HasKey(ta => ta.TouristAttractionId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        modelBuilder.Entity<City>()
            .HasMany(c => c.TouristAttractions)
            .WithOne(t => t.City)
            .HasForeignKey(t => t.CityId);

        modelBuilder.Entity<Visiting>()
            .HasKey(v => new { v.TourId, v.TouristAttractionId});
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Visitings)
            .WithOne(v => v.Tour)
            .HasForeignKey(v => v.TourId);
        modelBuilder.Entity<TouristAttraction>()
            .HasMany(ta => ta.Visitings)
            .WithOne(v => v.TouristAttraction)
            .HasForeignKey(v => v.TouristAttractionId);
    }

    public DbSet<City> City { get; set; }
    public DbSet<Hotel> Hotel { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<RoomType> RoomType { get; set; }
    public DbSet<HotelService> HotelService { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RoomPrice> RoomPrices { get; set; }
    public DbSet<BookRoom> BookRooms { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<HasService> HasServices { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackagePrice> PackagePrices { get; set; }
    public DbSet<BusinessPartner> BusinessPartner { get; set; }
    public DbSet<BookTour> BookTours { get; set; }
    public DbSet<TouristAttraction> TouristAttractions { get; set; }

    public DbSet<Visiting> Visitings { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
