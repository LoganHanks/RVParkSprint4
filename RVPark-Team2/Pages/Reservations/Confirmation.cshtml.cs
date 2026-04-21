using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Reservations
{
    public class ConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; } = new Reservation();

        public Site? Site { get; set; }

        public IActionResult OnGet(int id)
        {
            Reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            Site = _context.Sites
                .Include(s => s.SiteType)
                .FirstOrDefault(s => s.Id == Reservation.SiteId);

            return Page();
        }
    }
}
