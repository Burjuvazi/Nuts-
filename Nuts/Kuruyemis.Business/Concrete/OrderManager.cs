using Kuruyemis.Business.Abstract;
using Kuruyemis.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        public IOrderDal _orderDal { get; set; }

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

    }
}
