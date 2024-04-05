using API.Commons;
using API.Dtos;
using API.Entities;
using API.Repositories.Interfaces;
using API.Services.BaseService;
using API.Services.CommonServices.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Services.CommonServices.Implements
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<Paginated<Product>> GetProductsIncludeBrandAndType(int pageIndex, int pageSize)
        {
            var products = _genericRepository.GetQuery()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType);

            return await Paginated<Product>.CreateAsync(products, pageIndex, pageSize);
        }

        public async Task<List<Product>> GetProductsIncludeBrandAndType()
        {
            return await _genericRepository.GetQuery()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetBrands()
        {
            return await _unitOfWork.GenericRepository<ProductBrand>().Get();
        }

        public async Task<IEnumerable<ProductType>> GetTypes()
        {
            return await _unitOfWork.GenericRepository<ProductType>().Get();
        }

        public async Task<ResponseData<Product>> GetProductsIncludeBrandAndType(int pageIndex, int pageSize, int? brandId, int? typeId, string? filter = "name")
        {
            var products = _genericRepository.GetQuery()
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .AsQueryable();


            if (brandId != null)
            {
                products = products.Where(p => p.ProductBrandId == brandId);
            }

            if (typeId != null)
            {
                products = products.Where(p => p.ProductTypeId == typeId);
            }

            switch (filter)
            {
                case "name": products = products.OrderBy(p => p.Name); break;
                case "priceAsc": products = products.OrderBy(p => p.Price); break;
                case "priceDesc": products = products.OrderByDescending(p => p.Price); break;
                default: break;
            }

            var count = await products.CountAsync();

            products = products.Skip((pageIndex - 1)*pageSize).Take(pageSize);

            var productList = await products.ToListAsync();

            return new ResponseData<Product>()
            {
                Count = count,
                Data = productList,
            };
        }
    }
}
