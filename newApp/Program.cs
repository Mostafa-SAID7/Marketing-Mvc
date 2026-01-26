using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Repositoriers;
using newApp.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Register services
// --------------------
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductServ, ProductServ>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// --------------------
// Build app
// --------------------
var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
