using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace NHLCafe.Pages
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        // [PageRemote(
        //     ErrorMessage ="Product name already exists",
        //     AdditionalFields = "__RequestVerificationToken",
        //     HttpMethod ="GET", //POST will not work, posting to the wrong page even with PageName set correct!
        //     PageHandler ="CheckProductName"
        // )]
        [Required] [MinLength(2)] [MaxLength(12)]
        public string ProductName { get; set; }

        [Required] [MaxLength(128)]
        public string Description { get; set; }
        
        [Required] [MaxLength(7)]
        public string Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [BindProperty]
        public Category Category { get; set; }
    }
}
