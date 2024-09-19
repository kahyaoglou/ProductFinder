using ProductFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder.DataAccess.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        //Geriye liste türünden Product'leri döndürecek. Parametre almayacak.
        Task<Product> GetProductById(int id);
        //Geriye Product döndürecek. Parametre id alacak.
        Task<Product> CreateProduct(Product product);
        //Geriye Product döndürecek. Parametre Product alacak.
        Task<Product> UpdateProduct(Product product);
        //Geriye Product döndürecek. Parametre Product alacak.
        Task DeleteProduct(int id);
        //Geriye bir şey döndürmeyecek. Parametre id alacak.
    }
}
