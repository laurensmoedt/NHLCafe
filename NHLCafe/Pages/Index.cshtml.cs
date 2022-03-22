using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        
        public void OnGet(string category)
        {
            Products = ProductsRepository.GetProductWithCategories(category);
            Categories = CategoryRepository.GetCategories();
            
        }
    }
}