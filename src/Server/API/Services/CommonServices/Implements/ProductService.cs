using API.Entities;
using API.Repositories.Interfaces;
using API.Services.BaseService;
using API.Services.CommonServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CommonServices.Implements
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Product>> GetProductsIncludeBrandAndType()
        {
            return await _genericRepository.GetQuery()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }
    }
}
