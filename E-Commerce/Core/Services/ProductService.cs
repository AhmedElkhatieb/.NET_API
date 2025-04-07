using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
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

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            var products = await UnitOfWork.GetRepository<Product, int>().GetAllAsync();
            var productsResult = Mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
        {
            var types = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = Mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await UnitOfWork.GetRepository<Product, int>().GetAsync(id);
            var productsResult = Mapper.Map<ProductResultDto>(product);
            return productsResult;
        }
    }
}
