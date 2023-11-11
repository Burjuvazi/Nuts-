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
    public class SellerMap : CoreMap<Seller>
    {
        public override void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(75).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(75).IsRequired();
            builder.Property(x => x.Address).IsRequired();

            builder.HasOne(x => x.AppUser).WithOne(x => x.Seller).HasPrincipalKey<AppUser>(x => x.Id);

            builder.HasMany(x => x.SellerProducts).WithOne(x => x.Seller).HasForeignKey(x => x.SellerId);

            base.Configure(builder);
        }

    }
}
