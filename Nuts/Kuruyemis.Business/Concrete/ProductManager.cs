using Kuruyemis.Business.Abstract;
using Kuruyemis.DataAccess.Abstract;


namespace Kuruyemis.Business.Concrete
{
    public class ProductManager : IProductService
    {
        public IProductDal _productDal { get; set; }
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
    }
}
