using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http.Json;
using TestingLesson.Entities;
using TestingLesson.WebApi;

namespace TestingLesson.WebApiTests
{
    [TestFixture]
    public class ProductTest
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                });
            });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            // Arrange 
            var response = await _client.GetAsync("/api/product?top=10");

            // Assert
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            Assert.That(products != null);
            // Assert.That(products,Is.EqualTo(null));
        }

        [Test]
        public async Task GetProduct_ReturnsCorrectProduct()
        {
            // Arrange
            var response = await _client.GetAsync("/api/product?top=1");

            // Assert
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            var product = products?.FirstOrDefault();
            if(product != null)
            {
                var responseProduct = await _client.GetAsync($"/api/product/{product.Id}");
                Assert.That(responseProduct != null);
                var productResult = await responseProduct.Content.ReadFromJsonAsync<Product>();
                Assert.That(productResult != null);
                Assert.That(productResult.Id,Is.EqualTo(product.Id));
            }
        }

        [Test]
        public async Task PostProduct_AddsNewProduct()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "New Product",
                Price = 350
            };

            // Action
            var response = await _client.PostAsJsonAsync("/api/product", newProduct);

            // Assert
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.That(createdProduct != null);
            Assert.That(createdProduct?.Id > 0);
            Assert.That(newProduct.Name, Is.EqualTo(createdProduct?.Name));
        }
    }
}
