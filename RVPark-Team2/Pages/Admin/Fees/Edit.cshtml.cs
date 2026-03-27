using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Fees
{
    public class EditModel : AdminPageModel
    {
        private readonly RVPark_Team2.Data.ApplicationDbContext _context;

        public EditModel(RVPark_Team2.Data.ApplicationDbContext context)
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

            var fee =  await _context.Fees.FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }
            Fee = fee;
           ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Fee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(Fee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { reservationId = Fee.ReservationId });
        }

        private bool FeeExists(int id)
        {
            return _context.Fees.Any(e => e.Id == id);
        }
    }
}
