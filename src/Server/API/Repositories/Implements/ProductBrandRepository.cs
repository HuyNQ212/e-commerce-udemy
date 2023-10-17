using API.Data;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Repositories.Implements;

public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
{
    public ProductBrandRepository(StoreContext context) : base(context)
    {
    }
}