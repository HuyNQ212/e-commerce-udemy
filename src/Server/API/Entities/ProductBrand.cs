using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class ProductBrand : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}