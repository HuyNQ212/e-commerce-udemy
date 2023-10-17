using System.Text.Json;
using API.Data;

namespace API.Commons;

public class DbInitializer
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            
        }
    }
}