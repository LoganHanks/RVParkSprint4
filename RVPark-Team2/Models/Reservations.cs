namespace RVPark_Team2.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public int SiteId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsCancelled { get; set; }
    }
}
