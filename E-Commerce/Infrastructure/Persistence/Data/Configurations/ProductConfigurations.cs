using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product
            builder.Property(P => P.Price).HasColumnType("decimal(18,3)");
            #endregion
            #region ProductBrand
            builder.HasOne(P => P.ProductBrand).WithMany()
                .HasForeignKey(P => P.BrandId);
            #endregion
            #region ProductType
            builder.HasOne(P => P.ProductType).WithMany()
                .HasForeignKey(P => P.TypeId);
            #endregion
        }
    }
}
