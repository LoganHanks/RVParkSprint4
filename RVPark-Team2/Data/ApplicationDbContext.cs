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
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Models;

namespace RVPark_Team2.Data
{
    public class ApplicationDbContext : DbContext
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
                },
                new Reservation
                {
                    Id = 6,
                    CustomerName = "Customer Customer",
                    CustomerEmail = "customer@customer.com",
                    SiteId = 5,
                    StartDate = new DateTime(2026, 3, 1),
                    EndDate = new DateTime(2026, 3, 4),
                    TotalPrice = 105,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 7,
                    CustomerName = "Customer Customer",
                    CustomerEmail = "customer@customer.com",
                    SiteId = 34,
                    StartDate = new DateTime(2026, 4, 10),
                    EndDate = new DateTime(2026, 4, 15),
                    TotalPrice = 175,
                    IsCancelled = false
                },
                new Reservation
                {
                    Id = 8,
                    CustomerName = "Customer Customer",
                    CustomerEmail = "customer@customer.com",
                    SiteId = 50,
                    StartDate = new DateTime(2026, 2, 14),
                    EndDate = new DateTime(2026, 2, 17),
                    TotalPrice = 75,
                    IsCancelled = true
                }
            );

            // SiteType
            modelBuilder.Entity<SiteType>().HasData(
                new SiteType { Id = 1, Name = "Back-In", Description = "Standard back-in RV site" },
                new SiteType { Id = 2, Name = "Pull-Through", Description = "Standard pull-through RV site" },
                new SiteType { Id = 3, Name = "Dry", Description = "Dry camping site with no hook-ups" },
                new SiteType { Id = 4, Name = "Tent", Description = "Tent-only camping site" }
            );

            // SiteTypePrice
            modelBuilder.Entity<SiteTypePrice>().HasData(
                new SiteTypePrice { Id = 1, SiteTypeId = 1, StartDate = new DateTime(2026, 1, 1), EndDate = null, Price = 35.00m },
                new SiteTypePrice { Id = 2, SiteTypeId = 2, StartDate = new DateTime(2026, 1, 1), EndDate = null, Price = 35.00m },
                new SiteTypePrice { Id = 3, SiteTypeId = 3, StartDate = new DateTime(2026, 1, 1), EndDate = null, Price = 25.00m },
                new SiteTypePrice { Id = 4, SiteTypeId = 4, StartDate = new DateTime(2026, 1, 1), EndDate = null, Price = 25.00m }
            );

            // Site — Back-In (33 sites)
            modelBuilder.Entity<Site>().HasData(
                new Site { Id = 1, SiteNumber = "1", SiteTypeId = 1 },
                new Site { Id = 2, SiteNumber = "2", SiteTypeId = 1 },
                new Site { Id = 3, SiteNumber = "3", SiteTypeId = 1 },
                new Site { Id = 4, SiteNumber = "4", SiteTypeId = 1 },
                new Site { Id = 5, SiteNumber = "5", SiteTypeId = 1 },
                new Site { Id = 6, SiteNumber = "6", SiteTypeId = 1 },
                new Site { Id = 7, SiteNumber = "7", SiteTypeId = 1 },
                new Site { Id = 8, SiteNumber = "8", SiteTypeId = 1 },
                new Site { Id = 9, SiteNumber = "9", SiteTypeId = 1 },
                new Site { Id = 10, SiteNumber = "10", SiteTypeId = 1 },
                new Site { Id = 11, SiteNumber = "11", SiteTypeId = 1 },
                new Site { Id = 12, SiteNumber = "11B", SiteTypeId = 1 },
                new Site { Id = 13, SiteNumber = "12", SiteTypeId = 1 },
                new Site { Id = 14, SiteNumber = "12B", SiteTypeId = 1 },
                new Site { Id = 15, SiteNumber = "13", SiteTypeId = 1 },
                new Site { Id = 16, SiteNumber = "14", SiteTypeId = 1 },
                new Site { Id = 17, SiteNumber = "15", SiteTypeId = 1 },
                new Site { Id = 18, SiteNumber = "17", SiteTypeId = 1 },
                new Site { Id = 19, SiteNumber = "18", SiteTypeId = 1 },
                new Site { Id = 20, SiteNumber = "19", SiteTypeId = 1 },
                new Site { Id = 21, SiteNumber = "20", SiteTypeId = 1 },
                new Site { Id = 22, SiteNumber = "21", SiteTypeId = 1 },
                new Site { Id = 23, SiteNumber = "22", SiteTypeId = 1 },
                new Site { Id = 24, SiteNumber = "23", SiteTypeId = 1 },
                new Site { Id = 25, SiteNumber = "24", SiteTypeId = 1 },
                new Site { Id = 26, SiteNumber = "25", SiteTypeId = 1 },
                new Site { Id = 27, SiteNumber = "26", SiteTypeId = 1 },
                new Site { Id = 28, SiteNumber = "27", SiteTypeId = 1 },
                new Site { Id = 29, SiteNumber = "28", SiteTypeId = 1 },
                new Site { Id = 30, SiteNumber = "29", SiteTypeId = 1 },
                new Site { Id = 31, SiteNumber = "30", SiteTypeId = 1 },
                new Site { Id = 32, SiteNumber = "31", SiteTypeId = 1 },
                new Site { Id = 33, SiteNumber = "C", SiteTypeId = 1 },

                // Pull-Through (16 sites)
                new Site { Id = 34, SiteNumber = "32", SiteTypeId = 2 },
                new Site { Id = 35, SiteNumber = "33", SiteTypeId = 2 },
                new Site { Id = 36, SiteNumber = "34", SiteTypeId = 2 },
                new Site { Id = 37, SiteNumber = "35", SiteTypeId = 2 },
                new Site { Id = 38, SiteNumber = "36", SiteTypeId = 2 },
                new Site { Id = 39, SiteNumber = "37", SiteTypeId = 2 },
                new Site { Id = 40, SiteNumber = "38", SiteTypeId = 2 },
                new Site { Id = 41, SiteNumber = "39", SiteTypeId = 2 },
                new Site { Id = 42, SiteNumber = "40", SiteTypeId = 2 },
                new Site { Id = 43, SiteNumber = "41", SiteTypeId = 2 },
                new Site { Id = 44, SiteNumber = "42", SiteTypeId = 2 },
                new Site { Id = 45, SiteNumber = "43", SiteTypeId = 2 },
                new Site { Id = 46, SiteNumber = "44", SiteTypeId = 2 },
                new Site { Id = 47, SiteNumber = "45", SiteTypeId = 2 },
                new Site { Id = 48, SiteNumber = "A", SiteTypeId = 2 },
                new Site { Id = 49, SiteNumber = "B", SiteTypeId = 2 },

                // Dry (7 sites)
                new Site { Id = 50, SiteNumber = "D-1", SiteTypeId = 3 },
                new Site { Id = 51, SiteNumber = "D-2", SiteTypeId = 3 },
                new Site { Id = 52, SiteNumber = "D-3", SiteTypeId = 3 },
                new Site { Id = 53, SiteNumber = "D-4", SiteTypeId = 3 },
                new Site { Id = 54, SiteNumber = "D-5", SiteTypeId = 3 },
                new Site { Id = 55, SiteNumber = "D-6", SiteTypeId = 3 },
                new Site { Id = 56, SiteNumber = "D-7", SiteTypeId = 3 },

                // Tent (1 site)
                new Site { Id = 57, SiteNumber = "T-1", SiteTypeId = 4 }
            );

            // SitePhoto
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
