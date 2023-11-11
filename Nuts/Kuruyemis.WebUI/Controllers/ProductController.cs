using Kuruyemis.Business.Abstract;
using Kuruyemis.Entities.Concrete;
using Kuruyemis.WebUI.BasketTransaction.BasketModels;
using Kuruyemis.WebUI.BasketTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kuruyemis.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Products(int id)
        {
            List<Product> products = _productService.ProductsWithCategory();
            products = id < 1 ? products : products.Where(x => x.CategoryId == id).ToList();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            Product product = _productService.ProductWithCategoryById(id);
            return product != null ? View(product) : View("CustomError");
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Add()
        {
            var a = HttpContext.RequestServices.GetRequiredService<ICategoryService>();
            List<Category> categories = await a.GetCategoryAllAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Add(Product product)
        {
            bool response = await _productService.AddProductAsync(product);
            return response ? RedirectToAction("Products") : View();
        }

        [Authorize]
        public async Task<IActionResult> AddBasketItem(int id)
        {
            Product product = await _productService.GetProductAsync(id);
            var _basketTransaction = HttpContext.RequestServices.GetRequiredService<IBasketTransaction>();
            if (product != null)
            {
                BasketItemDto basketItemDto = new BasketItemDto()
                {
                    Description = product.Description,
                    ProductId = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Quantity = 1,
                    UserName = User.Identity.Name
                };
                _basketTransaction.SaveUpdateBasketItem(basketItemDto);
            }
            return RedirectToAction("Products");
        }
    }
}
