using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task InitializeAsync()
        {
           try
            {
                // Create database if it doesnt exist and apply any pending migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                {
                    await _storeContext.Database.MigrateAsync();
                }


                //Apply Data Seeding
                if (!_storeContext.ProductTypes.Any())
                {
                    // Read Types From File
                    var TypesData = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistence\\Seeding\\types.json");
                    // Transform into C# Objects
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    // Add To DB & Save Changes
                    if (Types is not null && Types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(Types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.ProductBrands.Any())
                {
                    // Read Types From File
                    var BrandsData = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistence\\Seeding\\brands.json");
                    // Transform into C# Objects
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    // Add To DB & Save Changes
                    if (Brands is not null && Brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(Brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.Products.Any())
                {
                    // Read Types From File
                    var ProductsData = await File.ReadAllTextAsync(@"..\\Infrastructure\\Persistence\\Seeding\\products.json");
                    // Transform into C# Objects
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    // Add To DB & Save Changes
                    if (Products is not null && Products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(Products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception)
            {

            }
        }
    }
}
