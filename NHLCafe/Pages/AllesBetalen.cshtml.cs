using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Helpers;

namespace NHLCafe.Pages
{
    public class AllesBetalen : PageModel
    {
        public List<Product> SelectedProducts { get; set; } = new List<Product>();
        
        public void OnGet()
        {
            if (HttpContext.Session.Get("products") == null)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
        }
    }
}

