using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Services._01_Purchase;
using App.Infra.Config.DbConfig;
using App.Infra.Config.IoCConfig;
using App.Infra.Db.sqlServer.Ef.Context;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Services

builder.Services.AddIdentityDbContextService(builder.Configuration);
builder.Services.AddIdentityCore<IdentityUser>
	(options =>
	{
		options.SignIn.RequireConfirmedAccount = true;
		options.Password.RequireDigit = false;
		options.Password.RequiredLength = 6;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
	})
.AddEntityFrameworkStores<AppDbContext>();
#endregion
#region IOC
// Using a custom DI container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddScopeSqlServerTables(builder.Configuration);
builder.Services.AddTransient<UserManager<IdentityUser>>();
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

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
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
