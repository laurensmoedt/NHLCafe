using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Msg { get; set; }


        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ober") != null)
                return new RedirectToPageResult("AccountOverview");
            return Page();
        }

        public IActionResult OnPost()
        {
            if(AccountRepository.GetAccount(Username, Password) != null)
            {
                HttpContext.Session.SetString("ober", Username);
                return RedirectToPage("AccountOverview");
            }

            if (Username == "")
                Msg = "Enter a username";
            else if (Password == "")
                Msg = "Enter a password";

            Msg = "Wrong username and/or password";
            return Page();
        }
    }
}
