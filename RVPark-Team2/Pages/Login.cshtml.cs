using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;
using BCrypt.Net;

namespace RVPark_Team2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                return Page();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == Email);

            if (employee == null)
            {
                ErrorMessage = "Invalid login credentials.";
                return Page();
            }

            if (employee.IsLocked)
            {
                ErrorMessage = "This account is locked.";
                return Page();
            }

            if (employee.AccessLevel != "Admin")
            {
                ErrorMessage = "You do not have permission to access this page.";
                return Page();
            }

            // Verify hashed password
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(Password, employee.PasswordHash);

            if (!isValidPassword)
            {
                ErrorMessage = "Invalid login credentials.";
                return Page();
            }

            // Log the user in (store ID in session)
            HttpContext.Session.SetInt32("EmployeeId", employee.Id);

            return RedirectToPage("/Admin/Index");
        }
    }
}