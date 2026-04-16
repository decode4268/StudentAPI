using SingletonDesignPattern.Interface;
using SingletonDesignPattern.Model;
using System.Net;

namespace SingletonDesignPattern.Service
{
    public class ProductService : IProduct
    {
        private readonly List<Product> _products;
        public ProductService()
        {
            _products = new List<Product>()
                {
                    new Product {Id = 1, Name= "Laptop", Price= 50000},
                    new Product {Id = 2, Name= "Mobile", Price= 4000}
                };
        }

        public List<Product> GetAllProduct()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }


        public Product AddProduct(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null) return null;
            existing.Name = product.Name;
            existing.Price = product.Price;
            return existing;
        }

        public bool DeleteProduct(int id)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return false;

            _products.Remove(existing);
            return true;

        }
        //Product AddProduct(Product product);
        //Product UpdateProduct(Product product);
        //bool DeleteProduct(int id);
    }
}
