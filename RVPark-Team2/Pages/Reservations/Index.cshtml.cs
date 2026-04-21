using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: Once customer registration is implemented, email search will be removed and replaced with info for the current customer. This is a temporary solution so that I can test cancelling, modifying, and viewing reservations.
        
        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }

        public List<ReservationResult> Results { get; set; } = new();

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Email))
                return;

            Results = _context.Reservations
                .Where(r => r.CustomerEmail == Email)
                .Join(_context.Sites.Include(s => s.SiteType),
                    r => r.SiteId,
                    s => s.Id,
                    (r, s) => new ReservationResult
                    {
                        Id = r.Id,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate,
                        SiteName = "Site " + s.SiteNumber + " - " + (s.SiteType != null ? s.SiteType.Name : ""),
                        TotalPrice = r.TotalPrice,
                        IsCancelled = r.IsCancelled
                    })
                .OrderByDescending(r => r.StartDate)
                .ToList();
        }

        public class ReservationResult
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string SiteName { get; set; } = string.Empty;
            public decimal TotalPrice { get; set; }
            public bool IsCancelled { get; set; }
        }
    }
}
