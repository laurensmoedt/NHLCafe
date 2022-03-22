using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHLCafe.Pages
{
    public class Update : PageModel
    {
        public List<Product> products { get; set; }
        
        public List<Category> categories { get; set; }
        
        [BindProperty] 
        public Product product { get; set; } = new Product();

        [BindProperty] 
        public int productSelect { get; set; } = 1;

        public void OnGet()
        {
            product = ProductsRepository.GetProductById(productSelect);
            products = ProductsRepository.GetProducts();
            categories = CategoryRepository.GetCategories();
        }

        public IActionResult OnPost()
        {
            product.ProductId = productSelect;
            product.Category = CategoryRepository.GetCategoryById(product.CategoryId);
            
            ProductsRepository.UpdateProduct(product);
            return RedirectToPage("Index");
        }
    }
}