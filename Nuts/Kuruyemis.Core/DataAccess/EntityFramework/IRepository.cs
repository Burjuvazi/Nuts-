
using Microsoft.EntityFrameworkCore;
using Nuts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nuts.Core.DataAccess.EntityFramework
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
      public  DbContext context { get; set; }
        DbSet<T> _set { get; set; }
        Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _set.Where(x=>x.Status == false).FirstOrDefaultAsync(expression);
        }
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null) 
        {
            return expression is null ? _set.Where(x=>x.Status == false).ToListAsync() : _set.Where(expression).Where(x=>x.Status == false).ToListAsync();
        }
        Task<int> AddAsync(T entity)
        {
            _set.Add(entity);
            return context.SaveChangesAsync();
        }
        Task<int> UpdateAsync(T entity) 
        { 
            _set.Update(entity);
            return context.SaveChangesAsync();
        }
        Task<int> RemoveAsync(T entity)
        {
            _set.Remove(entity); 
            return context.SaveChangesAsync();
        }
        Task<int> RemoveUpdate(T entity)
        {
            entity.Status = true;
            _set.Update(entity);
            return context.SaveChangesAsync();
        }
    }
}
