using System;

namespace RVPark_Team2.Services
{
    // Simple DTO used by the API to return booked ranges
    public class SiteReservationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
