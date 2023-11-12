using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Services._01_Purchase;
using App.Infra.Config.DbConfig;
using App.Infra.Config.IoCConfig;
using App.Infra.Db.sqlServer.Ef.Context;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
#region Services


builder.Services.AddIdentityDbContextService(builder.Configuration);


builder.Services.AddIdentity<User , IdentityRole<int>>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    }).AddRoles<IdentityRole<int>>()
.AddEntityFrameworkStores<AppDbContext>();

#endregion
#region IOC
// Using a custom DI container.
builder.Services.AddHttpContextAccessor();
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddScopeSqlServerTables(builder.Configuration);

builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.LoginPath = "/Identity/Login";
    option.AccessDeniedPath = "/Identity/AccessDenied";
    option.SlidingExpiration = true;
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

#endregion

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
////Access wwwroot of External Project
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), @"./../Administrator_Part/wwwroot/uploadImage/resize")),
//    RequestPath = new PathString("/MyImages")
//});
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();
ConfigureEndpoints(app);
void ConfigureEndpoints(IApplicationBuilder app)
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        endpoints.MapControllerRoute(
                                     "areaRoute",
                                     "{area:exists}/{controller=Account}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
                                     "default",
                                     "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapRazorPages();
    });
}
app.Run();
