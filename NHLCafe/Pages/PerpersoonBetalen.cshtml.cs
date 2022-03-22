using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Helpers;

namespace NHLCafe.Pages
{
    public class PerpersoonBetalen : PageModel
    {
        public List<Product> SelectedProducts { get; set; } = new List<Product>();
        
        public void OnGet()
        {
            if (HttpContext.Session.Get("products") == null)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            foreach (var prod in SelectedProducts)
            {
                if (HttpContext.Session.GetInt32("countToRemove" + prod.ProductId) == null)
                {
                    HttpContext.Session.SetInt32("countToRemove" + prod.ProductId, 0);
                }
            }
        }
        
        public IActionResult OnPostDecrement(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Product product = ProductsRepository.GetProductById(productID);
            foreach (var prod in SelectedProducts)
            {
                if (prod.ProductId == product.ProductId)
                {
                    if (HttpContext.Session.GetInt32("countToRemove"+prod.ProductId) == 0)
                    {
                        return Page();
                    }
                    
                    HttpContext.Session.SetInt32("countToRemove"+prod.ProductId, SessionHelper.Decrement(HttpContext.Session, "countToRemove" + prod.ProductId).Value);
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
                    if (HttpContext.Session.GetInt32("countToRemove"+prod.ProductId) == HttpContext.Session.GetInt32("count"+prod.ProductId))
                    {
                        return Page();
                    }
                    HttpContext.Session.SetInt32("countToRemove"+prod.ProductId, SessionHelper.Increment(HttpContext.Session, "countToRemove" + prod.ProductId).Value);
                    return Page();
                }
            }
            return Page();
        }

        public IActionResult OnPostPay()
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            List<Product> tempSelectedProducts = new List<Product>();
            
            foreach (var prod in SelectedProducts)
            {
                var currentCount = HttpContext.Session.GetInt32("count" + prod.ProductId).Value;
                var countToRemove = HttpContext.Session.GetInt32("countToRemove" + prod.ProductId).Value;
                var newCount = currentCount - countToRemove;
                
                HttpContext.Session.SetInt32("countToRemove"+prod.ProductId, 0);
                
                if (newCount <= 0)
                {
                    tempSelectedProducts.Add(prod);
                }
                else
                    HttpContext.Session.SetInt32("count"+prod.ProductId, newCount);
            }
            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts.Except(tempSelectedProducts));
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            if (SelectedProducts.FirstOrDefault() == null)
            {
                return RedirectToPage("TableSelect");
            }
            return RedirectToPage("Overzicht");
        }
    }
}

