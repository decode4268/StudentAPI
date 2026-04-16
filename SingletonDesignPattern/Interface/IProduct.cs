using SingletonDesignPattern.Model;

namespace SingletonDesignPattern.Interface
{
    public interface IProduct
    {
        List<Product> GetAllProduct();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }
}
