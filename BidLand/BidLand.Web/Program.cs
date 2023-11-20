using App.Domin.Core._02_Users.Entities;
using App.Infra.Config.IoCConfig;
using App.Infra.Db.sqlServer.Ef.Configurations.CustomIdentityError;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


#region Services
// Add services to the container.
var connectionString = builder.Configuration
.GetConnectionString("AppDbContextConnection") ??
throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
											options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#region Identity
builder.Services.AddDefaultIdentity<User>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.SignIn.RequireConfirmedEmail = false;
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.Password.RequiredLength = 4;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireDigit = false;
})
.AddRoles<Role>()
.AddErrorDescriber<CustomIdentityError>()
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<AppDbContext>();
#endregion

#region Cookies
//builder.Services.ConfigureApplicationCookie(option =>
//{
//	option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
//	option.LoginPath = "/account/login";
//	option.AccessDeniedPath = "/Account/AccessDenied";
//	option.SlidingExpiration = true;
//});
#endregion

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddScopeSqlServerTables(builder.Configuration);
builder.Services.AddHttpContextAccessor();
#endregion


var app = builder.Build();

#region Exception
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
#endregion



#region Middlewares
//app.UseErrorHandling();
//app.UseHangfireServer();
//app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
#endregion



#region EndPoint
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
#endregion


app.Run();



























//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

////builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//#region Services


////builder.Services.AddIdentityDbContextService(builder.Configuration);

//builder.Services.AddDefaultIdentity<User>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//    options.Password.RequiredLength = 4;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireDigit = false;
//})
//    .AddRoles<Role>()
//    .AddDefaultUI()
//    .AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<AppDbContext>();
//builder.Services.AddRazorPages();

//#endregion
//#region IOC
//// Using a custom DI container.
//builder.Services.AddHttpContextAccessor();
////builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//builder.Services.AddScopeSqlServerTables(builder.Configuration);

//builder.Services.ConfigureApplicationCookie(option =>
//{
//    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
//    option.LoginPath = "/Identity/Login";
//    option.AccessDeniedPath = "/Identity/AccessDenied";
//    option.SlidingExpiration = true;
//});
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(1800);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//#endregion

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//////Access wwwroot of External Project
////app.UseStaticFiles(new StaticFileOptions()
////{
////    FileProvider = new PhysicalFileProvider(
////        Path.Combine(Directory.GetCurrentDirectory(), @"./../Administrator_Part/wwwroot/uploadImage/resize")),
////    RequestPath = new PathString("/MyImages")
////});
//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseCookiePolicy();
//app.UseSession();
//ConfigureEndpoints(app);
//void ConfigureEndpoints(IApplicationBuilder app)
//{
//    app.UseEndpoints(endpoints =>
//    {
//        endpoints.MapControllers();

//        endpoints.MapControllerRoute(
//                                     "areaRoute",
//                                     "{area:exists}/{controller=Account}/{action=Index}/{id?}");

//        endpoints.MapControllerRoute(
//                                     "default",
//                                     "{controller=Home}/{action=Index}/{id?}");

//        endpoints.MapRazorPages();
//    });
//}
//app.Run();
