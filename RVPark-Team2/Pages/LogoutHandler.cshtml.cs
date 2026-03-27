using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RVPark_Team2.Pages
{
    public class LogoutHandlerModel : PageModel
    {
        public IActionResult OnPost()
        {
            // Clear the session data
            HttpContext.Session.Clear();

            // Remove the session cookie
            Response.Cookies.Delete(".AspNetCore.Session");

            Debug.WriteLine("User logged out, session cleared.");

            // Redirect to main index after logout
            return RedirectToPage("Index");
        }
    }
}