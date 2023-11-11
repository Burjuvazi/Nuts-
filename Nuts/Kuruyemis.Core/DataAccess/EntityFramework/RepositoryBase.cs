
using Microsoft.EntityFrameworkCore;
using Nuts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuts.Core.DataAccess.EntityFramework
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntity, new()
    {
        public DbContext context { get; set; }
        public DbSet<T> _set { get;  set; }

        public RepositoryBase(DbContext dbContext)
        {
            context = dbContext;
            _set = dbContext.Set<T>();
        }

    }
}
