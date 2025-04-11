using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions.Product;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork UnitOfWork, IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
            //Retreive All Brands => UnitOfWork
            var brands = await UnitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //Map To BrandResultDto => IMapper
            var brandsResult = Mapper.Map<IEnumerable<BrandResultDto>>(brands);
            //Return
            return brandsResult;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters parameters)
        {
            var products = await UnitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var productsResult = Mapper.Map<IEnumerable<ProductResultDto>>(products);
            // for count
            var count = productsResult.Count();
            var TotalCount = await UnitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters));
            var result = new PaginatedResult<ProductResultDto>(
                parameters.PageIndex, // Page Index 
                count, // Page Size
                count, // Total Count
                productsResult);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
        {
            var types = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = Mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await UnitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationsAsync(new ProductWithBrandAndTypeSpecifications(id));
            return product is null? throw new ProductNotFoundException(id) : Mapper.Map<ProductResultDto>(product);
        }
    }
}
