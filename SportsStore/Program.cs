using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// dependency injection feacher, that set app the shared objects
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opt =>
	opt.UseSqlServer(
		builder.Configuration["ConnectionStrings:SportsStoreConnection"]));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.MapControllerRoute("catpage", "{category}/Page{productPage:int}",
	new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{productPage:int}",
	new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}",
	new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination", "Products/Page{productPage}",
	new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("question", "/Questions");


app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();