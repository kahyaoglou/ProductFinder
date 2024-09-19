using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProductFinder.Business.Abstract;
using ProductFinder.Business.Concrete;
using ProductFinder.Business.Validators;
using ProductFinder.Entities;
using Serilog;

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
            Log.Information("Product list request received.");
            var products = await _productService.GetAllProducts();

            if (products != null)
            {
                Log.Information("Product list retrieved successfully.");
                return Ok(products);
            }

            Log.Warning("No products found.");
            return NotFound();
        }

        //Api'den id alan,
        //Geriye Product döndüren,
        //Aldığı id'yi parametre kullanan bir controller metot.
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information("Product details request received for ProductId: {ProductId}", id);
            var product = await _productService.GetProductById(id);

            if (product != null)
            {
                Log.Information("Product retrieved successfully: {@Product}", product);
                return Ok(product);
            }

            Log.Warning("Product with Id: {ProductId} not found.", id);
            return NotFound();
        }

        //Geriye Product döndüren
        //Product türünden bir Product parametre alan
        //Body kısmında Product barındıran bir controller metot.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            Log.Information("Product creation request received: {@Product}", product);

            var validationResult = new ProductValidator().Validate(product);
            if (!validationResult.IsValid)
            {
                Log.Warning("Product validation failed: {@ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var createdProduct = await _productService.CreateProduct(product);
            Log.Information("Product created successfully: {@Product}", createdProduct);
            return CreatedAtAction("Get", new { id = createdProduct.Id }, createdProduct); //201 + Data
        }

        //Geriye Product döndüren
        //Product türünden bir Product parametre alan
        //Body kısmında Product barındıran bir controller metot.
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            Log.Information("Product update request received for ProductId: {ProductId}", product.Id);

            var validationResult = new ProductValidator().Validate(product);
            if (!validationResult.IsValid)
            {
                Log.Warning("Product update validation failed: {@ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            if (await _productService.GetProductById(product.Id) != null)
            {
                Log.Information("Product with Id: {ProductId} found. Updating product...", product.Id);
                var updatedProduct = await _productService.UpdateProduct(product);
                Log.Information("Product updated successfully: {@Product}", updatedProduct);
                return Ok(updatedProduct); // 200 + Data
            }

            Log.Warning("Product with Id: {ProductId} not found for update.", product.Id);
            return NotFound(); // 404
        }

        //Api'den id alan,
        //Geriye bir şey döndürmeyen,
        //Aldığı id'yi parametre kullanan bir controller metot.
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Product deletion request received for ProductId: {ProductId}", id);

            if (await _productService.GetProductById(id) != null)
            {
                await _productService.DeleteProduct(id);
                Log.Information("Product with Id: {ProductId} deleted successfully.", id);
                return Ok(); // 200
            }

            Log.Warning("Product with Id: {ProductId} not found for deletion.", id);
            return NotFound(); // 404
        }
    }
}
