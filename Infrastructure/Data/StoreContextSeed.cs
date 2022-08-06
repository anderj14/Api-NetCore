using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {

                if (!context.ProductBrands.Any())
                {
                    using var transaction = context.Database.BeginTransaction();

                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands OFF");
                    transaction.Commit();

                    await context.SaveChangesAsync();
                }
                
                
                
                if (!context.ProductTypes.Any())
                {
                    using var transaction = context.Database.BeginTransaction();

                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes OFF");
                    transaction.Commit();

                    await context.SaveChangesAsync();
                }
                
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(e.Message);
            }
        }
    }
}