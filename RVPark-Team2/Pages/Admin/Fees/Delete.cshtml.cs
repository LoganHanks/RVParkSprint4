using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Fees
{
    public class DeleteModel : PageModel
    {
        private readonly RVPark_Team2.Data.ApplicationDbContext _context;

        public DeleteModel(RVPark_Team2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Fee Fee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .Include(f => f.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fee is not null)
            {
                Fee = fee;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees.FindAsync(id);
            if (fee != null)
            {
                Fee = fee;
                _context.Fees.Remove(Fee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { reservationId = Fee.ReservationId });
        }
    }
}
