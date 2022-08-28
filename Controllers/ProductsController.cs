using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Examinationen.Models.Product;
using WebApi_Examinationen.Services;
using static WebApi_Examinationen.Services.ProductService;

namespace WebApi_Examinationen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var result = await _productService.CreateAsync(product);
            if (result != null)
                return new OkObjectResult(result);

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return new OkObjectResult(await _productService.GetAll());
        }

        [HttpPut("{artnr}")]
        public async Task<IActionResult> UpdateProduct(int artnr, Product request)
        {
            var item = await _productService.UpdateProductAsync(artnr, request);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();
        }

        [HttpDelete("{artnr}")]
        public async Task<IActionResult> DeleteProduct(string artnr)
        {
            if (await _productService.DeleteAsync(artnr))
                return new OkResult();

            return new BadRequestResult();
        }
    }
}
