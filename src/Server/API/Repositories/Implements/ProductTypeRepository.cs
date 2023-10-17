using API.Data;
using API.Entities;
using API.Repositories.Interfaces;

namespace API.Repositories.Implements;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    public ProductTypeRepository(StoreContext context) : base(context)
    {
    }
}