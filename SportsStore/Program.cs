using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
}

);

builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
{

    opts.UseSqlServer
        (builder.Configuration["ConnectionStrings:IdentityConnection"]);
}
);

// When user wants to access authorize page that lead it to login page to verify his identity
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login"; 
});


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddScoped <IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IOrderRepository,EFOrderRepository>();
builder.Services.AddServerSideBlazor();


builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}

app.UseRequestLocalization(options =>
{
    options.AddSupportedCultures("en-US").AddSupportedUICultures("en-US")
        .SetDefaultCulture("en-US");

});

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.MapControllerRoute(
    name: "catpage",
    pattern: "{category}/page{productPage:int}",
    defaults: new { Controller = "Home", action = "Index" }
    );

app.MapControllerRoute(
    name: "page",
    pattern: "page{productPage:int}",
    defaults: new { Controller = "Home", action = "Index" , productPage = 1 }
    );

app.MapControllerRoute(
    name: "category",
    pattern: "{category}", 
    defaults: new { Controller = "Home", action = "Index" , productPage= 1 }
    );


app.MapControllerRoute(
    name: "pagination", 
    pattern: "Products/Page{productPage}",
    defaults: new { Controller = "Home", action = "Index", productPage = 1  }
    );

app.MapDefaultControllerRoute();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulated(app);

app.Run();
