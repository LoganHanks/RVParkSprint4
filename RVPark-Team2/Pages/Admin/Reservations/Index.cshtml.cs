using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Reservations
{
    public class IndexModel : AdminPageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IList<Reservation> Results { get; set; } = new List<Reservation>();

        public void OnGet()
        {
            var query = _context.Reservations.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(r =>
                    r.CustomerName.Contains(SearchTerm) ||
                    r.Id.ToString() == SearchTerm);
            }

            Results = query.ToList();
        }
    }
}
