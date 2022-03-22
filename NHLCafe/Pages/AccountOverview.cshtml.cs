using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class AccountOverviewModel : PageModel
    {
        public string Username { get; set; } = "";
        
        [BindProperty]
        public List<string> ProductNames { get; set; }
        
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("ober");
            
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("ober");
            return RedirectToPage("Login");
        }
    }
}
