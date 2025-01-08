using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingLesson.Entities;
using TestingLesson.WebApi.Services.Abstract;

namespace TestingLesson.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll(int top = 0)
        {
            var products = _productService.GetProducts(top);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _productService.GetProductById(id);
            if(item == null) return BadRequest();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            var addedProduct = _productService.Add(product);
            if (addedProduct.Id == 0 || addedProduct == null) return BadRequest();
            return Ok(addedProduct);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            var item = _productService.Update(product);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _productService.Delete(id);
            if (!item) return BadRequest();
            return Ok("Item deleted succesfully .");
        }
    }
}
