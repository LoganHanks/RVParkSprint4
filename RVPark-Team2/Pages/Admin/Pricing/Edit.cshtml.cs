using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Pricing
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SiteType SiteType { get; set; } = new SiteType();

        public IList<SiteTypePrice> Prices { get; set; } = new List<SiteTypePrice>();

        [BindProperty]
        public SiteTypePrice NewPrice { get; set; } = new SiteTypePrice();

        public IActionResult OnGet(int id)
        {
            var siteType = _context.SiteTypes.Find(id);

            if (siteType == null)
            {
                return NotFound();
            }

            SiteType = siteType;

            Prices = _context.SiteTypePrices
                .Where(p => p.SiteTypeId == id)
                .OrderByDescending(p => p.StartDate)
                .ToList();

            return Page();
        }

        public IActionResult OnPostSave(int id)
        {
            var siteType = _context.SiteTypes.Find(id);

            if (siteType == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(SiteType.Name))
            {
                ModelState.AddModelError("", "Name is required.");
                Prices = _context.SiteTypePrices
                    .Where(p => p.SiteTypeId == id)
                    .OrderByDescending(p => p.StartDate)
                    .ToList();
                return Page();
            }

            siteType.Name = SiteType.Name;
            siteType.Description = SiteType.Description ?? "";
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostAddPrice(int id)
        {
            var siteType = _context.SiteTypes.Find(id);

            if (siteType == null)
            {
                return NotFound();
            }

            if (NewPrice.Price <= 0)
            {
                ModelState.AddModelError("", "Price must be greater than 0.");
                SiteType = siteType;
                Prices = _context.SiteTypePrices
                    .Where(p => p.SiteTypeId == id)
                    .OrderByDescending(p => p.StartDate)
                    .ToList();
                return Page();
            }

            var price = new SiteTypePrice
            {
                SiteTypeId = id,
                StartDate = NewPrice.StartDate,
                EndDate = NewPrice.EndDate,
                Price = NewPrice.Price
            };

            _context.SiteTypePrices.Add(price);
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostEditStartDate(int id, int priceId, DateTime newStartDate)
        {
            var price = _context.SiteTypePrices.Find(priceId);
            if (price == null) return NotFound();

            price.StartDate = newStartDate;
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostEditEndDate(int id, int priceId, string? newEndDate)
        {
            var price = _context.SiteTypePrices.Find(priceId);
            if (price == null) return NotFound();

            if (string.IsNullOrEmpty(newEndDate))
            {
                price.EndDate = null;
            }
            else
            {
                price.EndDate = DateTime.Parse(newEndDate);
            }

            _context.SaveChanges();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostEditPrice(int id, int priceId, decimal newPrice)
        {
            var price = _context.SiteTypePrices.Find(priceId);
            if (price == null) return NotFound();

            if (newPrice <= 0) return RedirectToPage(new { id });

            price.Price = newPrice;
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostDeletePrice(int id, int priceId)
        {
            var price = _context.SiteTypePrices.Find(priceId);

            if (price == null)
            {
                return NotFound();
            }

            _context.SiteTypePrices.Remove(price);
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }
    }
}
