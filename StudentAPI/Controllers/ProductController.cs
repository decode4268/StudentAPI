using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
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
    }
}
