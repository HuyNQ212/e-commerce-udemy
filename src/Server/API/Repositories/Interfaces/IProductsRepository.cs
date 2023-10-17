using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task<List<ProductBrand>> GetProductBrandsAsync();
        Task<List<ProductType>> GetProductTypesAsync();

    }
}