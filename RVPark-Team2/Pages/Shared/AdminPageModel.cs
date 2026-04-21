using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RVPark_Team2.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        // Runs before any handler (OnGet, OnPost, etc.)
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            // Check if EmployeeId exists in session
            var employeeId = HttpContext.Session.GetInt32("EmployeeId");
            if (employeeId == null)
            {
                // Redirect to login page if session missing
                context.Result = RedirectToPage("/Login");
            }
        }
    }
}