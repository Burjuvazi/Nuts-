using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.Business.Abstract
{
    public interface ICustomerService
    {
        public ICustomerDal _customerDal { get; set; }

        Task<Customer> GetCustomerAsync(int id) => _customerDal.GetAsync(x => x.Id == id);
        async Task<List<Customer>> GetCustomerListAsync() => (await _customerDal.GetAllAsync()).OrderBy(x => x.Id).ToList();
        async Task<bool> UpdateCustomerAsync(Customer customer) => await _customerDal.UpdateAsync(customer) > 0;
        async Task<bool> AddCustomerAsync(Customer customer) => await _customerDal.AddAsync(customer) > 0;
        async Task<bool> DeleteCustomerAsync(int customerId) => await _customerDal.RemoveAsync(await GetCustomerAsync(customerId)) > 0;
    }
}
