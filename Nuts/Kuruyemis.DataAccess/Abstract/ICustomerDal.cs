﻿using Kuruyemis.Entities.Concrete;
using Nuts.Core.DataAccess.EntityFramework;

namespace Kuruyemis.DataAccess.Abstract
{
    public interface ICustomerDal : IRepository<Customer>
    {
    }
}
