using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Models;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Reservations
{
    public class EditModel : AdminPageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();

        [TempData]
        public string? StatusMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            Reservation = _context.Reservations.Find(id);

            if (Reservation == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            var existing = _context.Reservations
                .FirstOrDefault(r => r.Id == Reservation.Id);

            if (existing == null)
                return NotFound();

            if (Reservation.EndDate <= Reservation.StartDate)
            {
                ModelState.AddModelError(string.Empty, "End date must be after start date.");
                return Page();
            }

            bool isAvailable = !_context.Reservations.Any(r =>
                r.SiteId == Reservation.SiteId &&
                r.Id != Reservation.Id &&
                !r.IsCancelled &&
                Reservation.StartDate < r.EndDate &&
                Reservation.EndDate > r.StartDate
            );

            if (!isAvailable)
            {
                ModelState.AddModelError(string.Empty, "Site not available for selected dates.");
                return Page();
            }

            decimal oldPrice = existing.TotalPrice;
            decimal newPrice = CalculatePrice(Reservation);
            decimal priceDifference = newPrice - oldPrice;

            existing.StartDate = Reservation.StartDate;
            existing.EndDate = Reservation.EndDate;
            existing.SiteId = Reservation.SiteId;
            existing.TotalPrice = newPrice;

            _context.SaveChanges();

            if (priceDifference > 0)
            {
                StatusMessage = $"Reservation updated. Additional charge: {priceDifference:C}";
            }
            else if (priceDifference < 0)
            {
                StatusMessage = $"Reservation updated. Refund due: {Math.Abs(priceDifference):C}";
            }
            else
            {
                StatusMessage = "Reservation updated. No price change.";
            }

            return RedirectToPage(new { id = Reservation.Id });
        }

        public IActionResult OnPostCancel(int id)
        {
            var reservation = _context.Reservations.Find(id);

            if (reservation == null)
                return NotFound();

            reservation.IsCancelled = true;
            _context.SaveChanges();

            StatusMessage = "Reservation cancelled successfully.";

            return RedirectToPage("Index");
        }

        private decimal CalculatePrice(Reservation r)
        {
            int nights = (r.EndDate - r.StartDate).Days;
            return nights * 50m;
        }
    }
}
