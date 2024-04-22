using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Models;

namespace Infrastructure.Data
{
    public class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext _context)
        {

            if (!_context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                _context.Products.AddRange(products);
            }
            if (!_context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                _context.ProductBrands.AddRange(brands);
            }

            if (!_context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                _context.ProductTypes.AddRange(types);
            }


            if (_context.ChangeTracker.HasChanges()) await _context.SaveChangesAsync();
        }
    }
}
