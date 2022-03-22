using System;
using System.ComponentModel.DataAnnotations;

namespace NHLCafe.Pages
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
    }
}