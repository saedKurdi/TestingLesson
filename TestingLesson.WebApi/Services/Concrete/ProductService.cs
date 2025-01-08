using TestingLesson.Entities;
using TestingLesson.WebApi.Services.Abstract;

namespace TestingLesson.WebApi.Services.Concrete
{
    public class ProductService : IProductService
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Acer",
                Price = 3200,
            },
            new Product
            {
                Id = 2,
                Name = "Apple",
                Price = 4850,
            }
        };

        public Product Add(Product product)
        {
            var lastNumber = products.Count > 0 ? products.Max(x => x.Id) + 1 : 1;
            product.Id = lastNumber;
            products.Add(product);
            return product;
        }

        public bool Delete(int productId)
        {
           var item = products.FirstOrDefault(p => p.Id == productId);
            if (item == null) 
                return false;
            return products.Remove(item);
        }

        public Product? GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.Id == productId);
        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return top == 0 ? products : products.Take(top);
        }

        public Product? Update(Product product)
        {
            var item = products.FirstOrDefault(p =>p.Id == product.Id);
            if (item == null)
                return null;
            item.Name = product.Name;
            item.Price = product.Price;
            return item;
        }
    }
}
