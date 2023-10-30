using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Infra.Config.DbConfig;

public static class DbConfig
{
	public static IServiceCollection AddIdentityDbContextService(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(option =>
			option.UseSqlServer(configuration.GetConnectionString("sqlserver")));

		return services;
	}
}
