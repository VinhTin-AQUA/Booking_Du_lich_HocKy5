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

        modelBuilder.Entity<Contact>()
            .HasKey(c => c.Id).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Poster)
            .WithMany(p => p.PostTours)
            .HasForeignKey(t => t.PosterID);
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Approver)
            .WithMany(a => a.ApprovalTours)
            .HasForeignKey(t => t.ApproverID);

        modelBuilder.Entity<Package>()
            .HasKey(p => p.PackageID);
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Packages)
            .WithOne(p => p.Tour)
            .HasForeignKey(p => p.TourID);

        modelBuilder.Entity<PackagePrice>()
            .HasKey(pp => pp.PriceId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        modelBuilder.Entity<Package>()
            .HasMany(p => p.PackagePrices)
            .WithOne(pp => pp.Package)
            .HasForeignKey(pp => pp.PackageId);

        modelBuilder.Entity<BookTour>()
            .HasKey(bt => new { bt.PackageId, bt.UserID, bt.DepartureDate });

        modelBuilder.Entity<Package>()
             .HasMany(p => p.BookTours)
             .WithOne(bt => bt.Package)
             .HasForeignKey(bt => bt.PackageId);
        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.BookTours)
            .WithOne(bt => bt.User)
            .HasForeignKey(bt => bt.UserID);

        //modelBuilder.Entity<TouristAttraction>().HasKey(ta => ta.TouristAttractionId).HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        //modelBuilder.Entity<City>()
        //    .HasMany(c => c.TouristAttractions)
        //    .WithOne(t => t.City)
        //    .HasForeignKey(t => t.CityId);

        //modelBuilder.Entity<Visiting>()
        //    .HasKey(v => new { v.TourId, v.TouristAttractionId});
        //modelBuilder.Entity<Tour>()
        //    .HasMany(t => t.Visitings)
        //    .WithOne(v => v.Tour)
        //    .HasForeignKey(v => v.TourId);
        //modelBuilder.Entity<TouristAttraction>()
        //    .HasMany(ta => ta.Visitings)
        //    .WithOne(v => v.TouristAttraction)
        //    .HasForeignKey(v => v.TouristAttractionId);

        modelBuilder.Entity<TourCategory>()
            .HasKey(tt => new { tt.TourId, tt.CategoryId });
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.TourTypes)
            .WithOne(tt => tt.Tour)
            .HasForeignKey(tt => tt.TourId);
        modelBuilder.Entity<Category>()
            .HasMany(c => c.TourTypes)
            .WithOne(tt => tt.Category)
            .HasForeignKey(tt => tt.CategoryId);

        modelBuilder.Entity<CityTour>()
            .HasKey(ct => new { ct.TourId, ct.CityId });
        modelBuilder.Entity<Tour>()
            .HasMany(t => t.CityTours)
            .WithOne(ct => ct.Tour)
            .HasForeignKey(ct => ct.TourId);
        modelBuilder.Entity<City>()
            .HasMany(c => c.CityTours)
            .WithOne(ct => ct.City)
            .HasForeignKey(ct => ct.CityId);
    }

	public DbSet<City> City { get; set; }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackagePrice> PackagePrices { get; set; }
    public DbSet<BusinessPartner> BusinessPartner { get; set; }
    public DbSet<BookTour> BookTours { get; set; }
    //public DbSet<TouristAttraction> TouristAttractions { get; set; }

    //public DbSet<Visiting> Visitings { get; set; }
    public DbSet<TourCategory> TourTypes { get; set; }
    public DbSet<CityTour> CityTour { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
