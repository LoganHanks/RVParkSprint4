using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Admin.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public List<ReportRow> Completed { get; set; } = new();
        public List<ReportRow> InProgress { get; set; } = new();
        public List<ReportRow> Upcoming { get; set; } = new();
        public bool HasSearched { get; set; }

        public void OnGet()
        {
            if (StartDate == null || EndDate == null)
                return;

            HasSearched = true;
            var rangeStart = StartDate.Value.Date;
            var rangeEnd = EndDate.Value.Date;

            var rows = (from r in _context.Reservations
                        join s in _context.Sites on r.SiteId equals s.Id
                        where !r.IsCancelled
                        select new ReportRow
                        {
                            CustomerName = r.CustomerName,
                            CustomerEmail = r.CustomerEmail,
                            SiteNumber = s.SiteNumber,
                            StartDate = r.StartDate,
                            EndDate = r.EndDate,
                            Status = r.EndDate < rangeStart ? "Completed"
                                   : r.StartDate > rangeEnd ? "Upcoming"
                                   : "InProgress"
                        }).ToList();

            Completed = rows
                .Where(r => r.Status == "Completed")
                .OrderBy(r => r.StartDate)
                .ToList();

            InProgress = rows
                .Where(r => r.Status == "InProgress")
                .OrderBy(r => r.StartDate)
                .ToList();

            Upcoming = rows
                .Where(r => r.Status == "Upcoming")
                .OrderBy(r => r.StartDate)
                .ToList();
        }

        public class ReportRow
        {
            public string CustomerName { get; set; } = string.Empty;
            public string CustomerEmail { get; set; } = string.Empty;
            public string SiteNumber { get; set; } = string.Empty;
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }
}
