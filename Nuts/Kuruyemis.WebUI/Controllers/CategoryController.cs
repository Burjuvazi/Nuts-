using Kuruyemis.Business.Abstract;
using Kuruyemis.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Kuruyemis.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Categories()
        {
            List<Category> categories = await _categoryService.GetCategoryAllAsync();   
            return View(categories);
        }
    }
}
