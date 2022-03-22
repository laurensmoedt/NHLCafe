using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;
using NHLCafe;
using NHLCafe.Pages;
using NHLCafe.Pages.Helpers;

namespace NHLCafe.Pages
{
    public class Bestellen : PageModel
    {
        public  List<Product> AllProducts { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public List<Product> SelectedProducts { get; set; } = new List<Product>();
        
        public Category SelectedCategory { get; set; } = new Category();
        
        
        public void OnGet()
        {
            AllProducts = ProductsRepository.GetProducts();
            Products = ProductsRepository.GetProductWithCategories(SelectedCategory.Name);
            Categories = CategoryRepository.GetCategories();

            if (HttpContext.Session.Get("products") == null)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
        }
        
        
        public IActionResult OnPostAddProduct(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Categories = CategoryRepository.GetCategories();
            Products = ProductsRepository.GetProductWithCategories(SelectedCategory.Name);
            AllProducts = ProductsRepository.GetProducts();
            
            Product product = ProductsRepository.GetProductById(productID);
            foreach (var prod in SelectedProducts)
            {
                if (prod.ProductId == product.ProductId)
                {
                    HttpContext.Session.SetInt32("count"+prod.ProductId, SessionHelper.Increment(HttpContext.Session, "count" + prod.ProductId).Value);
                    return Page();
                }
            }
            
            SelectedProducts.Add(product);
            HttpContext.Session.SetInt32("count"+product.ProductId, 1);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
            
            return Page();
        }

        public IActionResult OnPostDecrement(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Categories = CategoryRepository.GetCategories();
            Products = ProductsRepository.GetProductWithCategories(SelectedCategory.Name);
            AllProducts = ProductsRepository.GetProducts();
            
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
            
            Categories = CategoryRepository.GetCategories();
            Products = ProductsRepository.GetProductWithCategories(SelectedCategory.Name);
            AllProducts = ProductsRepository.GetProducts();
            
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

        public IActionResult OnpostRemove(int productID)
        {
            SelectedProducts = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "products");
            
            Categories = CategoryRepository.GetCategories();
            Products = ProductsRepository.GetProductWithCategories(SelectedCategory.Name);
            AllProducts = ProductsRepository.GetProducts();
            
            Product product = ProductsRepository.GetProductById(productID);

            foreach (var prod in SelectedProducts)
            {
                if (prod.ProductId == product.ProductId)
                {
                    SelectedProducts.Remove(prod);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "products", SelectedProducts);
                    return Page();
                }
            }
            
            return Page();
        }
    }
}

    