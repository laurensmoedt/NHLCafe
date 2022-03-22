using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class Create : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        
        [TempData]
        public string ProductMessage { get; set; }

        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = CategoryRepository.GetCategories();
        }

        public IActionResult OnPost()
        {
            
            ProductsRepository.AddProduct(Product);

            return RedirectToPage("Index");
        }
    }
}