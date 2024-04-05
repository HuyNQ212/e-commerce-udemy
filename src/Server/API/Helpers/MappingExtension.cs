using API.Commons;
using API.Dtos;
using API.Entities;

namespace API.Helpers
{
    public static class MappingExtension
    {
        public static ResponseData<ProductReturnDto> MappingToProductReturnDtos(this ResponseData<Product> paginatedProduct)
        {
            return new ResponseData<ProductReturnDto>()
            {
                Count = paginatedProduct.Count,
                Data = paginatedProduct.Data.ConvertAll(new Converter<Product, ProductReturnDto>(product => new ProductReturnDto()
                {
                    ProductBrand = product.ProductBrand.Name,
                    Description = product.Description,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    ProductType = product.ProductType.Name,
                })),
            };
        }
    }
}
