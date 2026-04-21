using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Admin.Employees
{
    public class EditModel : AdminPageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmployeeViewModel EmployeeVM { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            // Map entity → view model
            EmployeeVM = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                AccessLevel = employee.AccessLevel,
                IsLocked = employee.IsLocked
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var employee = await _context.Employees.FindAsync(EmployeeVM.Id);
            if (employee == null)
                return NotFound();

            // Update fields
            employee.FirstName = EmployeeVM.FirstName;
            employee.LastName = EmployeeVM.LastName;
            employee.Email = EmployeeVM.Email;
            employee.AccessLevel = EmployeeVM.AccessLevel;
            employee.IsLocked = EmployeeVM.IsLocked;

            // Update password ONLY if entered
            if (!string.IsNullOrEmpty(EmployeeVM.Password))
            {
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(EmployeeVM.Password);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}