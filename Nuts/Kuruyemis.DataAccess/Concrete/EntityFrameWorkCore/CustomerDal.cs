using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Context;
using Kuruyemis.Entities.Concrete;
using Nuts.Core.DataAccess.EntityFramework;

namespace Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore
{
    public class CustomerDal : RepositoryBase<Customer> , ICustomerDal
    {
        public CustomerDal(KuruyemisContext context) : base(context)
        {
            
        }
    }
}
