// This class helps us set up the database
/*
===========================================================
DATABASE SETUP INSTRUCTIONS (FOR TEAM MEMBERS)
===========================================================

This project uses Entity Framework Core with SQL Server.

When you clone this repository, the database will NOT
automatically exist on your machine. You must create it
locally using migrations.

Follow these steps:

1. Make sure SQL Server is installed and running
   - Open SQL Server Management Studio (SSMS)
   - Connect to:
       localhost
       OR
       localhost\SQLEXPRESS

2. Check the connection string in appsettings.json

   It should look like this:

   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=RVParkDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }

   If your SQL Server instance is different, update it accordingly.

3. Open Visual Studio and build the project

   (Ctrl + Shift + B)

4. Open Package Manager Console

   Tools → NuGet Package Manager → Package Manager Console

5. Run the following command:

   Update-Database

   This will:
   - Create the database (RVParkDb)
   - Create all tables (Reservations, etc.)
   - Apply seed data if present

6. Verify in SQL Server Management Studio:

   - Refresh Databases
   - Look for "RVParkDb"
   - Expand Tables → dbo.Reservations

7. Run the application and navigate to:

   /Admin/Reservations

   You should now see data (if seeded) or be able to add/test data.

-----------------------------------------------------------
NOTES:

- Do NOT manually edit the database schema unless necessary.
- Always use migrations for schema changes:
      Add-Migration <Name>
      Update-Database

- If your database gets into a bad state (early in project):
      Drop-Database
      Update-Database

- The actual database is NOT stored in GitHub.
  Only the migration files are.

===========================================================
*/
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Models;

namespace RVPark_Team2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<SitePhoto> SitePhotos { get; set; }

        public DbSet<SiteType> SiteTypes { get; set; }

        public DbSet<SiteTypePrice> SiteTypePrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // in order to make sure that when a site is deleted, all associated photos are also deleted
            modelBuilder.Entity<SitePhoto>()
                .HasOne(sp => sp.PSite)
                .WithMany(s => s.Photos)
                .HasForeignKey(sp => sp.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // site belongs to a site type
            modelBuilder.Entity<Site>()
                .HasOne(s => s.SiteType)
                .WithMany(st => st.Sites)
                .HasForeignKey(s => s.SiteTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // maps SiteTypePrice model to the existing SiteTypePricings table
            modelBuilder.Entity<SiteTypePrice>()
                .ToTable("SiteTypePricings");

            // when a site type is deleted, all associated prices are also deleted
            modelBuilder.Entity<SiteTypePrice>()
                .HasOne(sp => sp.SiteType)
                .WithMany(st => st.Prices)
                .HasForeignKey(sp => sp.SiteTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Example Reservations (multiple)
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    CustomerName = "John Doe",
                    CustomerEmail = "test@example.com",
                    SiteId = 1,
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = new DateTime(2026, 1, 4),
                    TotalPrice = 150,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 2,
                    CustomerName = "Jane Smith",
                    CustomerEmail = "test@example.com",
                    SiteId = 2,
                    StartDate = new DateTime(2026, 1, 5),
                    EndDate = new DateTime(2026, 1, 8),
                    TotalPrice = 150,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 3,
                    CustomerName = "Mike Johnson",
                    CustomerEmail = "test@example.com",
                    SiteId = 1,
                    StartDate = new DateTime(2026, 1, 10),
                    EndDate = new DateTime(2026, 1, 12),
                    TotalPrice = 100,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 4,
                    CustomerName = "Emily Davis",
                    CustomerEmail = "test@example.com",
                    SiteId = 3,
                    StartDate = new DateTime(2026, 1, 3),
                    EndDate = new DateTime(2026, 1, 6),
                    TotalPrice = 150,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 5,
                    CustomerName = "Chris Brown",
                    CustomerEmail = "test@example.com",
                    SiteId = 2,
                    StartDate = new DateTime(2026, 1, 7),
                    EndDate = new DateTime(2026, 1, 9),
                    TotalPrice = 100,
                    IsCancelled = true
                }
            );

            // Example Site
            modelBuilder.Entity<Site>().HasData(
                new Site
                {
                    Id = 1,
                    SiteNumber = "A1",
                    SiteTypeId = 1
                },
                new Site
                {
                    Id = 2,
                    SiteNumber = "B1",
                    SiteTypeId = 1
                },
                new Site
                {
                    Id = 3,
                    SiteNumber = "C1",
                    SiteTypeId = 1
                }
            );

            // Example SitePhoto
            modelBuilder.Entity<SitePhoto>().HasData(
                new SitePhoto
                {
                    Id = 1,
                    SiteId = 1,
                    PhotoUrl = "/images/site1MapPhoto.png"
                },
                new SitePhoto
                {
                    Id = 2,
                    SiteId = 1,
                    PhotoUrl = "/images/site1Photo1.jpg"
                }
            );

            // Example SiteType
            modelBuilder.Entity<SiteType>().HasData(
                new SiteType
                {
                    Id = 1,
                    Name = "Full Hookup",
                    Description = "Site with all utility hookups"
                }
            );

            // Example SiteTypePrice
            modelBuilder.Entity<SiteTypePrice>().HasData(
                new SiteTypePrice
                {
                    Id = 1,
                    SiteTypeId = 1,
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = null,
                    Price = 50.00m
                }
            );

            modelBuilder.Entity<Employee>().HasData(
            new Employee
                {
                    Id = 1, // Use a fixed ID that doesn’t conflict with other seeded IDs
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    PasswordHash = "$2a$11$iLPVgQQZV5f3NI1gSGTKwuHrvT4iHnPVavbFn6f5AvGLj9eZBwrQi",
                    AccessLevel = "Admin",
                    IsLocked = false
                }
            );
        }
    }
}
