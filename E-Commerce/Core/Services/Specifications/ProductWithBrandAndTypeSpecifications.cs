using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        //2 ctors
        // one to retreive object b id
        public ProductWithBrandAndTypeSpecifications(int id) 
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
        //one to get all products
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters)
            :base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            #region Sort
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortingOptions.PriceDesc:
                        SetOrderByDesc(p => p.Price);
                        break;
                    case ProductSortingOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortingOptions.NameDesc:
                        SetOrderByDesc(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
            #endregion
        }

    }
}
