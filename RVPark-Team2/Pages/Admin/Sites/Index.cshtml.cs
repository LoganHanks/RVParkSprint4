using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Sites
{
    public class IndexModel : AdminPageModel
    {
        public void OnGet()
        {
            var dbSites = _context.Sites.AsQueryable();



            if (!string.IsNullOrEmpty(SearchSiteNumber))
            {

                dbSites = dbSites.Where(s => s.SiteNumber == SearchSiteNumber);


            }

            Sites = dbSites.ToList();



        }

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchSiteNumber { get; set; }

        public IList<Site> Sites { get; set; } = new List<Site>();

        public IActionResult OnPost()
        {
            var newSite = new Site
            {
                SiteNumber = "New",
                SiteTypeId = 1
            };

            _context.Sites.Add(newSite);
            _context.SaveChanges();

            return RedirectToPage("Edit", new { id = newSite.Id });
        }

        public IActionResult OnPostDelete(int id)
        {
            var site = _context.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }

            var photos = _context.SitePhotos.Where(p => p.SiteId == id).ToList();

            foreach(var photo in photos)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photo.PhotoUrl.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.SitePhotos.RemoveRange(photos);

            _context.Sites.Remove(site);
            _context.SaveChanges();

            return RedirectToPage();

        }
    }
}
