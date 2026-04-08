using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentAPI.DTO;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics,", Price = 800 },
            new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 600 },
            new Product { Id = 3, Name = "Tablet", Category = "Electronics", Price = 400 },
            new Product { Id = 4, Name = "Headphones", Category = "Electronics", Price = 120 },
            new Product { Id = 5, Name = "Smartwatch", Category = "Electronics", Price = 250 },
            new Product { Id = 6, Name = "Camera", Category = "Electronics", Price = 900 },
            new Product { Id = 7, Name = "Television", Category = "Electronics", Price = 1100 },
            new Product { Id = 8, Name = "Bluetooth Speaker", Category = "Electronics", Price = 150 },
            new Product { Id = 9, Name = "Gaming Console", Category = "Electronics", Price = 500 },
            new Product { Id = 10, Name = "Monitor", Category = "Electronics", Price = 300 },

            new Product { Id = 11, Name = "Refrigerator", Category = "Appliances", Price = 1200 },
            new Product { Id = 12, Name = "Microwave", Category = "Appliances", Price = 200 },
            new Product { Id = 13, Name = "Washing Machine", Category = "Appliances", Price = 700 },
            new Product { Id = 14, Name = "Air Conditioner", Category = "Appliances", Price = 1500 },
            new Product { Id = 15, Name = "Vacuum Cleaner", Category = "Appliances", Price = 350 },
            new Product { Id = 16, Name = "Dishwasher", Category = "Appliances", Price = 800 },
            new Product { Id = 17, Name = "Coffee Maker", Category = "Appliances", Price = 100 },
            new Product { Id = 18, Name = "Blender", Category = "Appliances", Price = 80 },
            new Product { Id = 19, Name = "Toaster", Category = "Appliances", Price = 60 },
            new Product { Id = 20, Name = "Water Purifier", Category = "Appliances", Price = 250 },

            new Product { Id = 21, Name = "Sofa", Category = "Furniture", Price = 900 },
            new Product { Id = 22, Name = "Dining Table", Category = "Furniture", Price = 700 },
            new Product { Id = 23, Name = "Bed", Category = "Furniture", Price = 1000 },
            new Product { Id = 24, Name = "Wardrobe", Category = "Furniture", Price = 600 },
            new Product { Id = 25, Name = "Office Chair", Category = "Furniture", Price = 150 },
            new Product { Id = 26, Name = "Bookshelf", Category = "Furniture", Price = 200 },
            new Product { Id = 27, Name = "Coffee Table", Category = "Furniture", Price = 180 },
            new Product { Id = 28, Name = "TV Stand", Category = "Furniture", Price = 220 },
            new Product { Id = 29, Name = "Mattress", Category = "Furniture", Price = 400 },
            new Product { Id = 30, Name = "Dresser", Category = "Furniture", Price = 350 },

            new Product { Id = 31, Name = "T-Shirt", Category = "Clothing", Price = 25 },
            new Product { Id = 32, Name = "Jeans", Category = "Clothing", Price = 50 },
            new Product { Id = 33, Name = "Jacket", Category = "Clothing", Price = 120 },
            new Product { Id = 34, Name = "Sneakers", Category = "Clothing", Price = 90 },
            new Product { Id = 35, Name = "Dress", Category = "Clothing", Price = 80 },
            new Product { Id = 36, Name = "Suit", Category = "Clothing", Price = 300 },
            new Product { Id = 37, Name = "Hat", Category = "Clothing", Price = 20 },
            new Product { Id = 38, Name = "Scarf", Category = "Clothing", Price = 30 },
            new Product { Id = 39, Name = "Gloves", Category = "Clothing", Price = 25 },
            new Product { Id = 40, Name = "Boots", Category = "Clothing", Price = 150 },

            new Product { Id = 41, Name = "Novel", Category = "Books", Price = 15 },
            new Product { Id = 42, Name = "Textbook", Category = "Books", Price = 80 },
            new Product { Id = 43, Name = "Cookbook", Category = "Books", Price = 35 },
            new Product { Id = 44, Name = "Children's Book", Category = "Books", Price = 20 },
            new Product { Id = 45, Name = "Biography", Category = "Books", Price = 25 },
            new Product { Id = 46, Name = "Science Journal", Category = "Books", Price = 40 },
            new Product { Id = 47, Name = "Comic Book", Category = "Books", Price = 12 },
            new Product { Id = 48, Name = "Travel Guide", Category = "Books", Price = 30 },
            new Product { Id = 49, Name = "Poetry Collection", Category = "Books", Price = 18 },
            new Product { Id = 50, Name = "History Book", Category = "Books", Price = 28 },
        };
        private readonly IMemoryCache _cache;
        public ProductController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            string cacheKey = $"product_{id}"; // product_1
            if (!_cache.TryGetValue(cacheKey, out string product))
            {
                // db logic to get the product value. 
                product = $"Product {id} fetched at {DateTime.Now}";

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                    .SetPriority(CacheItemPriority.High);

                _cache.Set(cacheKey, product, cacheOptions);


            }
            return Ok(product);
        }

        //[HttpGet("Pagination")]
        //public IActionResult GetPaginationData(int pageNumber = 1, int pageSize = 2)
        //{
        //    return Ok(new
        //    {
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        TotalItem = Products.Count,
        //        Data = Products
        //    });

        //}
    }
}
