using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class Product : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Precision(18,2)]
    public decimal Price { get; set; }

    public byte[]? Picture { get; set; } 
    
    public string? PictureUrl { get; set; }

    public virtual ProductType ProductType { get; set; }

    public int ProductTypeId { get; set; }

    public virtual ProductBrand ProductBrand { get; set; }

    public int ProductBrandId { get; set; }
}