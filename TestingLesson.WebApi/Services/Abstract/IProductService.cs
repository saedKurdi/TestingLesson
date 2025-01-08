using TestingLesson.Entities;

namespace TestingLesson.WebApi.Services.Abstract
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int top = 0);
        Product? GetProductById(int productId);
        Product Add(Product product);
        Product? Update(Product product);
        bool Delete(int productId);
    }
}
