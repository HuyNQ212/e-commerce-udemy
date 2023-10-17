using API.Data;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implements
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(StoreContext context) : base(context)
        {
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<List<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }
        
        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
