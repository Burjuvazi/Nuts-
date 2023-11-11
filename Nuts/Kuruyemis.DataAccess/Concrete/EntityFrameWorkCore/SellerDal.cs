using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Context;
using Kuruyemis.Entities.Concrete;
using Nuts.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore
{
    public class SellerDal : RepositoryBase<Seller> , ISellerDal
    {
        public SellerDal(KuruyemisContext context) : base(context)
        {
            
        }
    }
}
