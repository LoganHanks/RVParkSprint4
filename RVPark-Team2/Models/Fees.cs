using RVPark_Team2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Fee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [Required]
    public string FeeName { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    // Navigation property
    [ForeignKey("ReservationId")]
    public Reservation? Reservation { get; set; }
}