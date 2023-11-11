using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuruyemis.Business.Abstract
{
    public interface ICategoryService
    {
        public ICategoryDal _categoryDal { get; set; }

        Task<Category> GetCategoryAsync(int id) => _categoryDal.GetAsync(x => x.Id == id);
        async Task<List<Category>> GetCategoryAllAsync() => (await _categoryDal.GetAllAsync()).OrderBy(x => x.Id).ToList();
        async Task<bool> UpdateCategoryAsync(Category category) => await _categoryDal.UpdateAsync(category) > 0;
        async Task<bool> AddCategoryAsync(Category category) => await _categoryDal.AddAsync(category) > 0;
        async Task<bool> DeleteCategoryAsync(int categoryId) => await _categoryDal.RemoveAsync(await GetCategoryAsync(categoryId)) > 0;
    }
}
