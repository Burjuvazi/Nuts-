using Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Maps;
using Kuruyemis.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Context
{
    public class KuruyemisContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-IMM0HP8;Initial Catalog=Nuts;Trusted_Connection=true;TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);                        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductOrder>().HasKey(x => new { x.OrderId, x.ProductId });
            builder.Entity<SellerProduct>().HasKey(x => new { x.SellerId, x.ProductId });
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new SellerMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new OrderMap());

            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<SellerProduct> SellerProducts { get; set; }

    }
}
