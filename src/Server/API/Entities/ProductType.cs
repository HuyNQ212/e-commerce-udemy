﻿using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class ProductType : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
    }
}