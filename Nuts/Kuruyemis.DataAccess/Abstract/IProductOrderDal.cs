using Kuruyemis.Entities.Concrete;
using Nuts.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.DataAccess.Abstract
{
    public interface IProductOrderDal : IRepository<ProductOrder>
    {
    }
}
