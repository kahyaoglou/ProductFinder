using ProductFinder.Business.Abstract;
using ProductFinder.DataAccess.Abstract;
using ProductFinder.DataAccess.Concrete;
using ProductFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;
        //IProductRepository interface türünden bir değişken oluşturduk.
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            //Bu değişken, bir ProductRepository örneği olsun dedik.
        }

        public async Task<Product> CreateProduct(Product product)
        {
            return await _productRepository.CreateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            if (id > 0)
            {
                return await _productRepository.GetProductById(id);
            }

            throw new Exception("id can not be less than 1");
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }
    }
}
