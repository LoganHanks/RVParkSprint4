using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Admin.Sites
{
    public class EditModel : PageModel
    {
        

        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Site Site { get; set; } = new Site();

        public IList<SitePhoto> Photos { get; set; }

        public IActionResult OnGet(int id)
        {
            var site = _context.Sites.Find(id);

            if (site == null)
            {
                return NotFound();
            }

            Site = site;

            SiteTypes = new SelectList(_context.SiteTypes, "Id", "Name");

            var dbPhotos = _context.SitePhotos.AsQueryable();

            dbPhotos = dbPhotos.Where(p => p.SiteId == id);

            Photos = dbPhotos.ToList();

            return Page();
        }

        public SelectList SiteTypes { get; set; }

        public IActionResult OnPostSave(int id)
        {
            var site = _context.Sites.Find(id);

            if (site == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(Site.SiteNumber))
            {
                ModelState.AddModelError("", "Site Number is required");
                return Page();
            }

            site.SiteNumber = Site.SiteNumber;
            site.SiteTypeId = Site.SiteTypeId;

            

            _context.SaveChanges();

            return RedirectToPage(new { id = Site.Id });
        }

        [BindProperty]
        public IFormFile PhotoFile { get; set; }

        public async Task<IActionResult> OnPostAddPhotoAsync(int id)
        {
            var site = _context.Sites.Find(id);

            if (site == null)
            {
                return NotFound();
            }

            if (PhotoFile == null || PhotoFile.Length == 0)
            {
                return RedirectToPage(new { id });
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoFile.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await PhotoFile.CopyToAsync(stream);
            }

            var photo = new SitePhoto
            {
                SiteId = id,
                PhotoUrl = "/images/" + fileName
            };

            _context.SitePhotos.Add(photo);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostDeletePhoto(int id)
        {
            var photo = _context.SitePhotos.Find(id);

            if (photo == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photo.PhotoUrl.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.SitePhotos.Remove(photo);
            _context.SaveChanges();

            return RedirectToPage(new { id = photo.SiteId });
        }
    }
}
