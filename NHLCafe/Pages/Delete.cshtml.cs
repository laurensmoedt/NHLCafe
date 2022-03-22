using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class Delete : PageModel
    {
        public List<Product> products { get; set; }
        
        [BindProperty]
        public int productSelect { get; set; }
        
        public void OnGet()
        {
            products = ProductsRepository.GetProducts();
        }

        public IActionResult OnPost()
        {    
            if (ModelState.IsValid)
            {
                ProductsRepository.DeleteProduct(productSelect);
                return RedirectToPage("Index");    
            }
            
            return Page();
        }
    }
}