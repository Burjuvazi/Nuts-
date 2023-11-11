using Kuruyemis.Business.Abstract;
using Kuruyemis.DataAccess.Abstract;



namespace Kuruyemis.Business.Concrete
{
    public class OrderProcessManager : IOrderProcessService
    {
        public IOrderDal _orderDal { get; set; }
        public IProductDal _productDal { get; set; }
        public IProductOrderDal _productOrderDal { get; set; }
        public OrderProcessManager(IOrderDal orderDal, IProductDal productDal, IProductOrderDal productOrderDal)
        {
            _orderDal = orderDal;
            _productDal = productDal;
            _productOrderDal = productOrderDal;
        }
    }
}
