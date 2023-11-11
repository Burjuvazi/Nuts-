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
    public class OrderMap : CoreMap<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProductOrders).WithOne(x => x.Order).HasForeignKey(x => x.ProductId);

            base.Configure(builder);
        }
    }
}
