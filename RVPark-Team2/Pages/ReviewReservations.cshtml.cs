using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages
{
    public class ReviewReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; }

        public int Nights { get; set; }
        public decimal BaseTotal { get; set; }
        public decimal FeesTotal { get; set; }
        public decimal Total { get; set; }

        public IActionResult OnGet(int id)
        {
            Reservation = _context.Reservations
                .FirstOrDefault(r => r.Id == id);

            if (Reservation == null)
                return NotFound();

            // Nights
            Nights = (Reservation.EndDate - Reservation.StartDate).Days;

            // Get the site for the reservation
            var site = _context.Sites
                .FirstOrDefault(s => s.Id == Reservation.SiteId);

            // Get pricing for that site type
            var pricing = _context.SiteTypePrices
                .FirstOrDefault(p =>
                    p.SiteTypeId == site.SiteTypeId &&
                    Reservation.StartDate >= p.StartDate &&
                    (p.EndDate == null || Reservation.StartDate <= p.EndDate)
                );

            // Use DB price
            decimal pricePerNight = pricing?.Price ?? 0;

            // Calculate base total
            BaseTotal = Nights * pricePerNight;

            // Fees
            FeesTotal = _context.Fees
                .Where(f => f.ReservationId == id)
                .Sum(f => f.Amount);

            // Total
            Total = BaseTotal + FeesTotal;

            return Page();
        }
    }
}