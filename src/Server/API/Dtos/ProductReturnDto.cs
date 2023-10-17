namespace API.Dtos
{
    public class ProductReturnDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public byte[]? Picture { get; set; }

        public string? PictureUrl { get; set; }

        public string? ProductType { get; set; }

        public string? ProductBrand { get; set; }

    }
}
