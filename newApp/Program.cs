using Microsoft.EntityFrameworkCore;
using newApp.Data;
using newApp.Repositoriers;
using newApp.Services;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Register services
// --------------------
builder.Services.AddControllersWithViews();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Existing services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductServ, ProductServ>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();

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
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DataSeeder.SeedAsync(context);
}

app.Run();
