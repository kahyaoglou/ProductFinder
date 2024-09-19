using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProductFinder.Business.Abstract;
using ProductFinder.Business.Concrete;
using ProductFinder.Entities;

namespace ProductFinder.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Geriye liste türünden Product döndüren bir controller metot
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        //Api'den id alan,
        //Geriye Product döndüren,
        //Aldığı id'yi parametre kullanan bir controller metot.
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        //Geriye Product döndüren
        //Product türünden bir Product parametre alan
        //Body kısmında Product barındıran bir controller metot.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            return CreatedAtAction("Get", new { id = createdProduct.Id }, createdProduct); //201 + Data
        }

        //Geriye Product döndüren
        //Product türünden bir Product parametre alan
        //Body kısmında Product barındıran bir controller metot.
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            if (await _productService.GetProductById(product.Id) != null)
            {
                return Ok(await _productService.UpdateProduct(product)); //200 + Data
            }
            return NotFound(); //404
        }

        //Api'den id alan,
        //Geriye bir şey döndürmeyen,
        //Aldığı id'yi parametre kullanan bir controller metot.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _productService.GetProductById(id) != null)
            {
                await _productService.DeleteProduct(id);
                return Ok(); //200
            }
            return NotFound(); //404
        }
    }
}
