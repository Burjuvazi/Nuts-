using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.Business.Abstract
{
    public interface IOrderService
    {
        public IOrderDal _orderDal { get; set; }

        Task<Order> GetOrderAsync(int id) => _orderDal.GetAsync(x => x.Id == id);
        async Task<List<Order>> GetOrdersAsync() => (await _orderDal.GetAllAsync()).ToList();
        async Task<bool> UpdateOrderAsync(Order order) => await _orderDal.UpdateAsync(order) > 0;
        async Task<bool> AddOrderAsync(Order order) => await _orderDal.AddAsync(order) > 0;
        async Task<bool> DeleteOrderAsync(int orderId) => await _orderDal.RemoveAsync(await GetOrderAsync(orderId)) > 0;
    }
}
