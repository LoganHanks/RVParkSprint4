using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RVPark_Team2.Data;
using RVPark_Team2.Models;
using RVPark_Team2.Services;

namespace RVPark_Team2.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservationService _reservationService;

        public CreateModel(ApplicationDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();

        public List<SelectListItem> SiteOptions { get; set; } = new();

        public void OnGet(int? siteId, DateOnly? startDate, DateOnly? endDate)
        {
            SiteOptions = _reservationService.LoadSiteOptions();

            Reservation.StartDate = DateTime.Today;
            Reservation.EndDate = DateTime.Today.AddDays(1);

            if (siteId.HasValue)
            {
                Reservation.SiteId = siteId.Value;
            }

            if (startDate.HasValue)
            {
                Reservation.StartDate = startDate.Value.ToDateTime(TimeOnly.MinValue);
            }

            if (endDate.HasValue)
            {
                Reservation.EndDate = endDate.Value.ToDateTime(TimeOnly.MinValue);
            }
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

            if (!_reservationService.CheckAvailability(Reservation.SiteId, Reservation.StartDate, Reservation.EndDate))
            {
                ModelState.AddModelError(string.Empty, "This campsite is not available for the selected dates.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reservation.TotalPrice = _reservationService.CalculatePrice(Reservation.SiteId, Reservation.StartDate, Reservation.EndDate);
            Reservation.IsCancelled = false;

            _context.Reservations.Add(Reservation);
            _context.SaveChanges();

            return RedirectToPage("ReviewReservations", new { id = Reservation.Id });
        }
    }
}
