﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._01_Purchase;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._02_Users;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Infra.DataAccess.Repos.Ef._03_Extras;
using App.Infra.DataAccess.Repos.Ef._03_Extres;
using App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Services._01_Purchase;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Services._02_Users;
using App.Domin.Core._03_Extras.Contracts.Services;
using App.Domin.Services._03_Extras;

namespace App.Infra.Config.IoCConfig
{
	public static class IocConfig
	{
		public static IServiceCollection AddScopeSqlServerTables(this IServiceCollection services,
			IConfiguration configuration)
		{
			#region Purchase
			// --------------------->Reposritory<-------------------------------------
			services.AddScoped<IAuctionRepository, AuctionRepository>();
			services.AddScoped<IBidRepository, BidRepository>();
			services.AddScoped<IBoothRepository, BoothRepository>();
			services.AddScoped<ICartRepository, CartRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IStockRepository, StockRepository>();
			services.AddScoped<IStocksCartRepository, StocksCartRepository>();

			// --------------------->Service<-------------------------------------
			services.AddScoped<IAuctionService, AuctionService>();
			services.AddScoped<IBidService, BidService>();
			services.AddScoped<IBoothService, BoothService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IStockService, StockService>();
			services.AddScoped<IStocksCartService, StocksCartService>();

			#endregion

			#region Users
			// --------------------->Repository<---------------------------------
			services.AddScoped<ISellerRepository, SellerRepository>();
			services.AddScoped<IBuyerRepository, BuyerRepository>();

			// --------------------->Service<---------------------------------
			services.AddScoped<ISellerService, SellerService>();
			services.AddScoped<IBuyerService, BuyerService>();

			#endregion

			#region Extras
			// --------------------->Repository<--------------------------------------
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<ICommentRepository, CommentRepository>();
			services.AddScoped<IImageRepository, ImageRepository>();
			services.AddScoped<IMedalRepository, MedalRepository>();

			// --------------------->Service<--------------------------------------
			services.AddScoped<IAddressService, AddressService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<IImageService, ImageService>();
			services.AddScoped<IMedalService, MedalService>();

			#endregion


			return services;
		}
	}
}