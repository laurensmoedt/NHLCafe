using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;

namespace NHLCafe.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty] 
        public string Username { get; set; } = "";

        [BindProperty]
        public string Email { get; set; }= "";

        [BindProperty]
        public string Password { get; set; }= "";

        [BindProperty]
        public string ConfirmPassword { get; set; }= "";

        [BindProperty]
        public string Msg { get; set; }= "";

        public Account Gebruiker { get; set; } = new Account();
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            
            if (!(string.IsNullOrEmpty(Username)) && !(string.IsNullOrEmpty(Password)))
            {
                Gebruiker.Username = Username;
                Gebruiker.Password = Password;
                Gebruiker.Email = Email;

                if (Password != ConfirmPassword)
                {
                    Msg = "Passwords must be the same";
                    return Page();
                }
                else if (Password.Length < 8)
                {
                    Msg = "Password must be atleast 8 characters";
                    return Page();
                }

                if (!AccountRepository.AccountEmailNotExists(Email))
                {
                    Msg = "This e-mail adres is already in use, enter a diffrent e-mail adres.";
                    return Page();
                }

                AccountRepository.AddAccount(Gebruiker);
                return RedirectToPage("Login");
            }
            else
                Msg = "Username or Password cannot be empty";
                return Page();

        }
    }
}
