using Microsoft.AspNetCore.Mvc;
using RVPark_Team2.Services;
using System.Linq;

namespace RVPark_Team2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public SitesController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET api/sites/availability/5
        [HttpGet("availability/{siteId}")]
        public IActionResult GetAvailability(int siteId)
        {
            try
            {
                if (!_reservationService.SiteExists(siteId))
                    return NotFound($"Site {siteId} not found.");

                var reservations = _reservationService.GetReservationsForSite(siteId);

                var events = reservations.Select(r => new
                {
                    title = "Booked",
                    start = r.StartDate.ToString("yyyy-MM-dd"),
                    end = r.EndDate.ToString("yyyy-MM-dd")
                });

                return Ok(events);
            }
            catch (System.Exception ex)
            {
                // Return a 500 with the error message to help debugging in the UI
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
