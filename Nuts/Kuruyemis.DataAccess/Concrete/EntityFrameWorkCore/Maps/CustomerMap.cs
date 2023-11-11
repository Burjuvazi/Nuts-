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
    public class CustomerMap : CoreMap<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired();

            builder.HasOne(x => x.AppUser).WithOne(x => x.Customer).HasPrincipalKey<AppUser>(x => x.Id);
            builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

            base.Configure(builder);
        }
    }
}
