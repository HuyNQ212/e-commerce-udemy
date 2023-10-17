using API.Entities;
using API.Services.BaseService;

namespace API.Services.CommonServices.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<List<Product>> GetProductsIncludeBrandAndType();
    }
}
