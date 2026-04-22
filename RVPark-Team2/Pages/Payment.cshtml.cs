using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaymentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; }
        public decimal Total { get; set; }

        public IActionResult OnGet(int id)
        {
            Reservation = _context.Reservations
                .FirstOrDefault(r => r.Id == id);

            if (Reservation == null)
                return NotFound();

            int nights = (Reservation.EndDate - Reservation.StartDate).Days;

            var site = _context.Sites
                .FirstOrDefault(s => s.Id == Reservation.SiteId);

            var pricing = _context.SiteTypePrices
                .FirstOrDefault(p =>
                    p.SiteTypeId == site.SiteTypeId &&
                    Reservation.StartDate >= p.StartDate &&
                    (p.EndDate == null || Reservation.StartDate <= p.EndDate)
                );

            decimal baseTotal = nights * (pricing?.Price ?? 0);

            decimal feesTotal = _context.Fees
                .Where(f => f.ReservationId == id)
                .Sum(f => f.Amount);

            Total = baseTotal + feesTotal;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // Simulate successful payment
            return RedirectToPage("/Confirmation", new { id = id });
        }
    }
}