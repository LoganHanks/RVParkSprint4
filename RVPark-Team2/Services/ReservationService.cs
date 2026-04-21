using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;

namespace RVPark_Team2.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> LoadSiteOptions()
        {
            return _context.Sites
                .Include(s => s.SiteType)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Site {s.SiteNumber} - {s.SiteType.Name}"
                })
                .ToList();
        }

        public bool CheckAvailability(int siteId, DateTime startDate, DateTime endDate, int? excludeReservationId = null)
        {
            return !_context.Reservations.Any(r =>
                r.SiteId == siteId &&
                !r.IsCancelled &&
                (excludeReservationId == null || r.Id != excludeReservationId) &&
                startDate < r.EndDate &&
                endDate > r.StartDate);
        }

        public bool SiteExists(int siteId)
        {
            return _context.Sites.Any(s => s.Id == siteId);
        }

        public decimal CalculatePrice(int siteId, DateTime startDate, DateTime endDate)
        {
            int nights = (endDate - startDate).Days;
            if (nights < 1) return 0;

            var site = _context.Sites
                .Include(s => s.SiteType)
                .FirstOrDefault(s => s.Id == siteId);

            if (site == null)
                return nights * 50m;

            var priceRecord = _context.SiteTypePrices
                .Where(p => p.SiteTypeId == site.SiteTypeId &&
                            p.StartDate <= startDate &&
                            (p.EndDate == null || p.EndDate >= startDate))
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefault();

            decimal nightlyRate = priceRecord?.Price ?? 50m;

            return nights * nightlyRate;
        }
    }
}
