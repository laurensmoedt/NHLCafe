using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages;
using NHLCafe.Pages.Helpers;

namespace NHLCafe.Pages
{
    public class Overzicht : PageModel
    {
        public List<Product> SelectedProducts { get; set; } = new List<Product>();
        
        public void OnGet()
        {
            if (HttpContext.Session.Get("products") == null)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
        }
        
        public IActionResult OnPostDecrement(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Product product = ProductsRepository.GetProductById(productID);
            foreach (var prod in SelectedProducts)
            {
                if (prod.ProductId == product.ProductId)
                {
                    if (HttpContext.Session.GetInt32("count"+prod.ProductId) == 1)
                    {
                        SelectedProducts.Remove(prod);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
                    }
                    HttpContext.Session.SetInt32("count"+prod.ProductId, SessionHelper.Decrement(HttpContext.Session, "count" + prod.ProductId).Value);
                    return Page();
                }
            }
            
            return Page();
        }
        
        public IActionResult OnPostIncrement(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Product product = ProductsRepository.GetProductById(productID);
            foreach (var prod in SelectedProducts)
            {
                if (prod.ProductId == product.ProductId)
                {
                    HttpContext.Session.SetInt32("count"+prod.ProductId, SessionHelper.Increment(HttpContext.Session, "count" + prod.ProductId).Value);
                    return Page();
                }
            }
            return Page();
        }
    } 
}

