using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace NHLCafe.Pages
{
    public static class ProductsRepository
    {
        public static Product GetProductById(int id)
        {
            using var db = DBUtils.Connect();
            var product =
                db.QueryFirst<Product>("SELECT * FROM Product WHERE ProductId = @ProductId",
                    new {ProductId = id});
            return product;
        }

        public static List<Product> GetProducts()
        {
            using var db = DBUtils.Connect();
            var products =
                db.Query<Product>("SELECT * FROM Product")
                    .ToList();
            return products;
        }

        public static List<Product> GetProductWithCategories(string category)
        {
            //https://www.learndapper.com/relationships
            string sql =
                @"SELECT p.ProductId, p.ProductName, p.Description, p.Price, p.CategoryId, c.CategoryId, c.Name 
                            FROM Product p JOIN Category c 
                                on c.CategoryId = p.CategoryId WHERE @Category IS NULL OR c.Name = @Category";

            using var db = DBUtils.Connect();
            var products = db.Query<Product, Category, Product>(sql, (product, category) =>
            {
                product.Category = category;
                return product;
            }, splitOn: "CategoryId", param: new {Category = category}).ToList();

            return products;
        }

        public static bool DeleteProduct(int productId)
        {
            using var db = DBUtils.Connect();
            var result = db.Execute("DELETE FROM Product WHERE ProductId = @ProductId", new
            {
                ProductId = productId
            });
            return result == 1;
        }

        public static Product AddProduct(Product product)
        {
            using var db = DBUtils.Connect();
            int newProductId = db.ExecuteScalar<int>(
                @"INSERT INTO Product (ProductName, Description, Price, CategoryId) 
                    VALUES (@ProductName, @Description, @Price, @CategoryId); 
                    SELECT LAST_INSERT_ID();", new
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                });
            product.ProductId = newProductId;
            product.Category = CategoryRepository.GetCategoryById(product.CategoryId);

            return product;
        }
        
        public static Product UpdateProduct(Product product)
        {
            using var db = DBUtils.Connect();
            var updatedProduct = db.QuerySingle<Product>(@"UPDATE Product 
                                                                SET ProductName = @ProductName,  Description = @Description, Price = @Price, CategoryId = @CategoryId 
                                                                WHERE ProductId = @ProductId;
                                                                SELECT * FROM Product WHERE ProductId = @ProductId", new
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                });
            return updatedProduct;
        }

        public static bool ProductNameNotExists(string productName)
        {
            if (!string.IsNullOrWhiteSpace(productName))
            {
                productName = productName.Trim().ToLower();
            }

            using var db = DBUtils.Connect();
            int rowCount = db.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Product WHERE Name = @Name", 
                new { Name = productName}
            );

            return rowCount < 1;
        }

    }
}
