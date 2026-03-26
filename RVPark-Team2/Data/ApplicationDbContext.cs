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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    CustomerName = "Test User",
                    SiteId = 1,
                    StartDate = new DateTime(2026, 1, 1),
                    EndDate = new DateTime(2026, 1, 4),
                    TotalPrice = 150,
                    IsCancelled = false
                }
            );
        }
    }

}
