using API.Entities;
using System.Linq.Expressions;

namespace API.Specifications
{
    public class ProductsWithTypeAndBrandsSpecification : Specification<Product>
    {
        public ProductsWithTypeAndBrandsSpecification()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductsWithTypeAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
