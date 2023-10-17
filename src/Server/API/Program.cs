using API.Data;
using API.Repositories.Implements;
using API.Repositories.Interfaces;
using API.Services.CommonServices.Implements;
using API.Services.CommonServices.Interfaces;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Default");
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            context.Database.Migrate();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while migrating");
        }
        
        app.Run();
    }
}