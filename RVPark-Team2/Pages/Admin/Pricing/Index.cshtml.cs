using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Pricing
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SiteType> SiteTypes { get; set; } = new List<SiteType>();

        public Dictionary<int, int> SiteCounts { get; set; } = new();

        public void OnGet()
        {
            SiteTypes = _context.SiteTypes
                .Include(st => st.Prices)
                .ToList();

            SiteCounts = _context.Sites
                .GroupBy(s => s.SiteTypeId)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public IActionResult OnPostDelete(int id)
        {
            var siteType = _context.SiteTypes.Find(id);
            if (siteType == null)
            {
                return NotFound();
            }

            var hasSites = _context.Sites.Any(s => s.SiteTypeId == id);
            if (hasSites)
            {
                ModelState.AddModelError("", "Cannot delete a site type that has sites assigned to it.");
                SiteTypes = _context.SiteTypes
                    .Include(st => st.Prices)
                    .ToList();
                return Page();
            }

            _context.SiteTypes.Remove(siteType);
            _context.SaveChanges();

            return RedirectToPage();
        }
    }
}
