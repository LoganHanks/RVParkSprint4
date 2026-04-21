using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RVPark_Team2.Data;
using RVPark_Team2.Models;
using RVPark_Team2.Services;

namespace RVPark_Team2.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservationService _reservationService;

        public EditModel(ApplicationDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = new();

        [BindProperty]
        public int OriginalReservationId { get; set; }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        public List<SelectListItem> SiteOptions { get; set; } = new();

        public IActionResult OnGet(int id, string email)
        {
            var existing = _context.Reservations.Find(id);
            if (existing == null || existing.IsCancelled)
                return RedirectToPage("Index", new { Email = email });

            OriginalReservationId = existing.Id;
            Email = email;

            Reservation = new Reservation
            {
                CustomerName = existing.CustomerName,
                CustomerEmail = existing.CustomerEmail,
                SiteId = existing.SiteId,
                StartDate = existing.StartDate,
                EndDate = existing.EndDate,
                NumberOfAdults = existing.NumberOfAdults,
                Pets = existing.Pets,
                SpecialRequests = existing.SpecialRequests,
                VehicleLength = existing.VehicleLength
            };

            SiteOptions = _reservationService.LoadSiteOptions();
            return Page();
        }

        public IActionResult OnPost()
        {
            SiteOptions = _reservationService.LoadSiteOptions();

            if (Reservation.EndDate <= Reservation.StartDate)
            {
                ModelState.AddModelError(string.Empty, "Check-out date must be after check-in date.");
            }

            if (!_reservationService.SiteExists(Reservation.SiteId))
            {
                ModelState.AddModelError("Reservation.SiteId", "Please select a valid campsite.");
            }

            if (!_reservationService.CheckAvailability(Reservation.SiteId, Reservation.StartDate, Reservation.EndDate, OriginalReservationId))
            {
                ModelState.AddModelError(string.Empty, "This campsite is not available for the selected dates.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var original = _context.Reservations.Find(OriginalReservationId);
            if (original != null)
            {
                original.IsCancelled = true;
            }

            Reservation.TotalPrice = _reservationService.CalculatePrice(Reservation.SiteId, Reservation.StartDate, Reservation.EndDate);
            Reservation.IsCancelled = false;

            // TODO: Simulate payment processing before finalizing the reservation

            _context.Reservations.Add(Reservation);
            _context.SaveChanges();

            return RedirectToPage("Index", new { Email = Email });
        }
    }
}
