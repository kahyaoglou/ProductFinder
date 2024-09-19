using Microsoft.EntityFrameworkCore;
using ProductFinder.DataAccess.Abstract;
using ProductFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder.DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        public async Task<Product> CreateProduct(Product product)
        {
            using (var ProductDbContext = new ProductDbContext())
            {
                ProductDbContext.Products.Add(product);
                await ProductDbContext.SaveChangesAsync();
                return product;
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (var ProductDbContext = new ProductDbContext())
            {
                var deletedProduct = await GetProductById(id);
                ProductDbContext.Products.Remove(deletedProduct);
                await ProductDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (var ProductDbContext = new ProductDbContext())
            {
                return await ProductDbContext.Products.ToListAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (var ProductDbContext = new ProductDbContext())
            {
                return await ProductDbContext.Products.FindAsync(id);
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            using (var ProductDbContext = new ProductDbContext())
            {
                ProductDbContext.Products.Update(product);
                await ProductDbContext.SaveChangesAsync();
                return product;
            }
        }
    }
}
