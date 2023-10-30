using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._01_Purchase;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._02_Users;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._03_Extras;
using App.Infra.DataAccess.Repos.Ef._03_Extres;
using App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;

namespace App.Infra.Config.IoCConfig
{
	public static class IocConfig
	{
		public static IServiceCollection AddScopeSqlServerTables(this IServiceCollection services,
			IConfiguration configuration)
		{
			#region Purchase
			// --------------------->Purchase<-------------------------------------
			services.AddScoped<IAuctionRepository, AuctionRepository>();
			services.AddScoped<IBidRepository, BidRepository>();
			services.AddScoped<IBoothRepository, BoothRepository>();
			services.AddScoped<ICartRepository, CartRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IStockRepository, StockRepository>();
			services.AddScoped<IStocksCartRepository, StocksCartRepository>();

			#endregion

			#region Users
			// --------------------->Users<---------------------------------
			services.AddScoped<ISellerRepository, SellerRepository>();
			services.AddScoped<IBuyerRepository, BuyerRepository>();

			#endregion

			#region Extras
			// --------------------->Extras<--------------------------------------
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<IImageRepository, ImageRepository>();
			services.AddScoped<IMedalRepository, MedalRepository>();

			#endregion


			return services;
		}
	}
}
