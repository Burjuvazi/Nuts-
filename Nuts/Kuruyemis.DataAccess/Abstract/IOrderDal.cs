using Kuruyemis.Entities.Concrete;
using Kuruyemis.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using Nuts.Core.DataAccess.EntityFramework;

namespace Kuruyemis.DataAccess.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        public OrderProcessDto GetOrderProcess()
        {
            OrderProcessDto join =
                _set.Include(x => x.ProductOrders).Select(x => new OrderProcessDto()
                {
                    ProductOrders = x.ProductOrders.ToList(),
                    Orders = _set.ToList(),
                    Products = context.Set<Product>().ToList()
                }).FirstOrDefault();
            return join;
        }
    }
}
