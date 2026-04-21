using System.ComponentModel.DataAnnotations;

namespace RVPark_Team2.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        public int SiteId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "At least 1 adult is required.")]
        public int NumberOfAdults { get; set; }

        public bool Pets { get; set; }

        [StringLength(500, ErrorMessage = "Special requests cannot exceed 500 characters.")]
        public string? SpecialRequests { get; set; }

        [Range(0, 100, ErrorMessage = "Vehicle length must be between 0 and 100.")]
        public int? VehicleLength { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsCancelled { get; set; }
    }
}
