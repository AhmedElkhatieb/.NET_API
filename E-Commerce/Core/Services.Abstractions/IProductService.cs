using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get All Products
        public Task<IEnumerable<ProductResultDto>> GetAllProductAsync();
        //Get All Brands
        public Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        //Get All Types
        public Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
        //Get Product By ID
        public Task<ProductResultDto> GetProductByIdAsync(int id);

    }
}
