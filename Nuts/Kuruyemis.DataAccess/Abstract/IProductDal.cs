using Kuruyemis.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Nuts.Core.DataAccess.EntityFramework;


namespace Kuruyemis.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        public List<Product> ProductWithCategory() => _set.Include(x => x.Category).ToList();
        public Product GetProductWithCategoryById(int id) => _set.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }
}
