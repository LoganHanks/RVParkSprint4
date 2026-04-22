using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Reservations
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

            // Use stored total instead of recalculating
            Total = Reservation.TotalPrice;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            // Simulate successful payment
            return RedirectToPage("/Reservations/Confirmation", new { id = id });
        }
    }
}