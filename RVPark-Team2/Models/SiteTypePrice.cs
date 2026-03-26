using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RVPark_Team2.Models
{
    public class SiteTypePrice
    {
        public int Id { get; set; }

        [Required]
        public int SiteTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [ForeignKey("SiteTypeId")]
        public SiteType? SiteType { get; set; }
    }
}
