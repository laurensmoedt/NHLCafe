using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace NHLCafe.Pages
{
    public static class CategoryRepository
    {
        //add CRUD operations

        public class CategoryInfo
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }
            public decimal MaxPrice { get; set; }
            public decimal MinPrice { get; set; }
            public decimal AvgPrice { get; set; }
        }

        public static List<Category> GetCategories()
        {
            using (var db = DBUtils.Connect())
            {
                return db.Query<Category>("SELECT * FROM Category ORDER BY Name").ToList();
            }
        }

        public static List<CategoryInfo> GetCategoryInfos(string categoryNameFilter = null)
        {
            using (var db = DBUtils.Connect())
            {
                var result = db.Query<CategoryInfo>(@"
                    SELECT c.Name, 
                           COUNT(p.ProductId) as ProductCount, 
                           MAX(p.Price) as MaxPrice, 
                           MIN(p.Price) as MinPrice, 
                           ROUND(AVG(p.Price), 2) as AvgPrice
                    FROM Product p JOIN Category c on c.CategoryId = p.CategoryId
                    WHERE @Category IS NULL OR c.Name = @Category
                    GROUP BY c.Name, c.CategoryId
                    ORDER BY c.Name
                ", new {Category = categoryNameFilter}).ToList();

                return result;
            }
        }

        public static Category GetCategoryById(int categoryId)
        {
            using (var db = DBUtils.Connect())
            {
                //https://dapper-tutorial.net/querysingleordefault
                return db.QuerySingleOrDefault<Category>("SELECT * FROM Category WHERE CategoryId = @CategoryId", new
                {
                    CategoryId = categoryId
                });
            }
        }
    }
}
