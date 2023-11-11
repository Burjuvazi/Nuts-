using Kuruyemis.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuts.Core.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Maps
{
    public class ProductMap : CoreMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();

            builder.HasMany(x => x.ProductOrders).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.SellerProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            base.Configure(builder);
        }
    }
}
