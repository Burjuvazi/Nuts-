
using Kuruyemis.Business.Abstract;
using Kuruyemis.Business.AutoMapper;
using Kuruyemis.Business.Concrete;
using Kuruyemis.DataAccess.Abstract;
using Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore;
using Kuruyemis.DataAccess.Concrete.EntityFrameWorkCore.Context;
using Kuruyemis.Entities.Concrete;
using Kuruyemis.WebUI.BasketTransaction;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KuruyemisContext>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<KuruyemisContext>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});

//MapperConfiguration mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
//IMapper mapper = mapperConfiguration.CreateMapper();
//builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddScoped<IOrderProcessService, OrderProcessManager>();
builder.Services.AddScoped<IProductOrderDal, ProductOrderDal>();
builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<ISellerDal, SellerDal>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<ICustomerDal, CustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<ISellerService, SellerManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddTransient<IBasketTransaction, BasketTransaction>();


var app = builder.Build();

// Add services to the container.
//builder.Services.AddControllersWithViews();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=HomePage}/{id?}");

app.Run();
