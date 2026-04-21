using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();

        public List<SelectListItem> SiteOptions { get; set; } = new();

        public void OnGet(int? siteId)
        {
            LoadSiteOptions();

            if (siteId.HasValue)
            {
                Reservation.SiteId = siteId.Value;
            }
        }

        public IActionResult OnPost()
        {
            LoadSiteOptions();

            if (Reservation.EndDate <= Reservation.StartDate)
            {
                ModelState.AddModelError(string.Empty, "Check-out date must be after check-in date.");
            }

            bool siteExists = _context.Sites.Any(s => s.Id == Reservation.SiteId);
            if (!siteExists)
            {
                ModelState.AddModelError("Reservation.SiteId", "Please select a valid campsite.");
            }

            bool isAvailable = !_context.Reservations.Any(r =>
                r.SiteId == Reservation.SiteId &&
                !r.IsCancelled &&
                Reservation.StartDate < r.EndDate &&
                Reservation.EndDate > r.StartDate);

            if (!isAvailable)
            {
                ModelState.AddModelError(string.Empty, "This campsite is not available for the selected dates.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reservation.TotalPrice = CalculatePrice(Reservation);
            Reservation.IsCancelled = false;

            _context.Reservations.Add(Reservation);
            _context.SaveChanges();

            return RedirectToPage("Confirmation", new { id = Reservation.Id });
        }

        private void LoadSiteOptions()
        {
            SiteOptions = _context.Sites
                .Include(s => s.SiteType)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Site {s.SiteNumber} - {s.SiteType.Name}"
                })
                .ToList();
        }

        private decimal CalculatePrice(Reservation reservation)
        {
            int nights = (reservation.EndDate - reservation.StartDate).Days;
            if (nights < 1) return 0;

            var site = _context.Sites
                .Include(s => s.SiteType)
                .FirstOrDefault(s => s.Id == reservation.SiteId);

            if (site == null)
            {
                return nights * 50m;
            }

            var priceRecord = _context.SiteTypePrices
                .Where(p => p.SiteTypeId == site.SiteTypeId &&
                            p.StartDate <= reservation.StartDate &&
                            (p.EndDate == null || p.EndDate >= reservation.StartDate))
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefault();

            decimal nightlyRate = priceRecord?.Price ?? 50m;

            return nights * nightlyRate;
        }
    }
}