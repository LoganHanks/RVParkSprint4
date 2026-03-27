using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Pricing
{
    public class CreateModel : AdminPageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SiteType SiteType { get; set; } = new SiteType();

        [BindProperty]
        public decimal InitialPrice { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(SiteType.Name))
            {
                ModelState.AddModelError("", "Name is required.");
                return Page();
            }

            if (InitialPrice <= 0)
            {
                ModelState.AddModelError("", "Price must be greater than 0.");
                return Page();
            }

            var siteType = new SiteType
            {
                Name = SiteType.Name,
                Description = SiteType.Description ?? ""
            };

            _context.SiteTypes.Add(siteType);
            _context.SaveChanges();

            var price = new SiteTypePrice
            {
                SiteTypeId = siteType.Id,
                StartDate = DateTime.Today,
                EndDate = null,
                Price = InitialPrice
            };

            _context.SiteTypePrices.Add(price);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Site type created successfully!";
            return RedirectToPage("Index");

        }
    }
}
