using Kuruyemis.Business.Abstract;
using Kuruyemis.Entities.Concrete;
using Kuruyemis.Entities.Concrete.Dtos;
using Kuruyemis.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace Kuruyemis.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        IOrderProcessService _orderProcessService;
      

        public AccountController(IAuthService authService, IOrderProcessService orderProcessService)
        {
            _orderProcessService = orderProcessService;
            _authService = authService;
        }
    
        [HttpGet]
        public IActionResult Login()
        {
        //    SentrySdk.CaptureMessage("");
            
            return View();
        }
        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult test()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                string ass = User.Identity.Name;
                var result = await _authService.Login(loginDto);
                if (result.Succeeded)
                {
                    var user = _authService.GetUserByEmail(loginDto.Email);
                    var role = await _authService.GetRoleByUserName(user.UserName);
                    switch (role)
                    {
                        case "Customer": return RedirectToAction("Categories", "Category");
                        default: return RedirectToAction("Categories", "Category");
                    }
                }
            }
            ModelState.AddModelError("", "Lütfen girilen değerleri kontrol ediniz!");
            return View(loginDto);
        }
        [HttpGet]
        public IActionResult CustomerRegister()
        {
            //var _customerService = HttpContext.RequestServices.GetRequiredService<ICustomerService>();
            //List<Customer> customers = await _customerService.GetCustomerListAsync();
            //ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            IdentityResult result = await _authService.CustomerRegister(customerRegisterDto);
            if (result.Succeeded)
            {
                //uyeSayisi++;
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Lütfen girilen değerleri kontrol ediniz!");
            return View(customerRegisterDto);
        }
        [HttpGet]
        public IActionResult SellerRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellerRegister(SellerRegisterDto sellerRegisterDto)
        {
            IdentityResult result = await _authService.SellerRegister(sellerRegisterDto);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Lütfen girilen değerleri kontrol ediniz!");
            return View(sellerRegisterDto);
        }
        public async Task<IActionResult> LogOut()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
