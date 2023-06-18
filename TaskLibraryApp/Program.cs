using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskLibraryApp;
using TaskLibraryApp.CacheManager;
using TaskLibraryApp.RepositoryManager;
using TaskLibraryApp.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICacheManager, CacheManager>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
builder.Services.AddDbContext<LibraryDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("LibConnection")));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
