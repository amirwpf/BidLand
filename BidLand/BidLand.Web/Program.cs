using App.Infra.Config.DbConfig;
using App.Infra.Config.IoCConfig;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScopeSqlServerTables(builder.Configuration);

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

app.MapAreaControllerRoute(
    name: "Admin",
	areaName : "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
