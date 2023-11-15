using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.AppServices.Users
{
    public class AdminPanelAppServices : IAdminPanelAppServices
	{
		private readonly IProductService _productServices;
		private readonly ICommentService _commentServices;
		private readonly ICategoryService _categoryService;
		private readonly IBoothService _boothService;
		private readonly IStocksCartService _stocksCartService;


		public AdminPanelAppServices(IProductService productService,
									ICommentService commentService,
									ICategoryService categoryService,
									IBoothService boothService,
									IStocksCartService stocksCartService)
		{
			_productServices = productService;
			_commentServices = commentService;
			_categoryService = categoryService;
			_boothService = boothService;
			_stocksCartService = stocksCartService;
		}

		#region ProductsManagement
		public Task<List<ProductRepoDto>> GetAllProducts(CancellationToken cancellationToken)
		{
			return _productServices.GetAllAsync(cancellationToken);
		}

		public async Task<bool> ConfirmProduct(int productId, bool isConfirm, CancellationToken cancellationToken)
		{
			return await _productServices.ConfirmProductAsync(productId, isConfirm, cancellationToken);
		}

        public Task<List<ProductRepoDto>> GetAllConfirmedProductsAsync(CancellationToken cancellationToken)
        {
            return _productServices.GetAllConfirmedProductsAsync(cancellationToken);
        }
        public Task<List<ProductRepoDto>> GetAllPendingProductsAsync(CancellationToken cancellationToken)
        {
            return _productServices.GetAllPendingProductsAsync(cancellationToken);
        }

        #endregion

        #region CommentsManagement




        public async Task<List<CommentRepoDto>> GetPendingCommentsAsync(CancellationToken cancellationToken)
		{
			return await _commentServices.GetAllCommentsWithSellerNameConfirmAsync(cancellationToken);
		}
		public async Task<bool> ConfirmCommentByIdAsync(int commentId, bool isConfirm, CancellationToken cancellationToken)
		{
			return await _commentServices.ConfirmCommentByIdAsync(commentId, isConfirm, cancellationToken);

		}
		#endregion


		#region Categories
		public async Task<List<CategoryRepoDto>> GetChildCategories(CancellationToken cancellationToken)
		{
			return await _categoryService.GetAllChildAsync(cancellationToken);
		}

		public async Task CreateProduct(ProductRepoDto model, CancellationToken cancellationToken)
		{
           
            model.InsertionDate = DateTime.Now;
			await _productServices.CreateAsync(model, cancellationToken);
		}

        public Task<ProductRepoDto> GetProductById(int id, CancellationToken cancellationToken)
        {
			return _productServices.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateProduct(ProductRepoDto model, CancellationToken cancellationToken)
        {
			model.InsertionDate = DateTime.Now;
			return await _productServices.UpdateAsync(model, cancellationToken);
        }

        public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
        {
			return await _productServices.DeleteAsync(id, cancellationToken);
        }
		
		public async Task<bool> RecoverProduct(int id, CancellationToken cancellationToken)
        {
			return await _productServices.RecoverAsync(id, cancellationToken);
        }

        public async Task<List<BoothRepoDto>> GetAllBooths(CancellationToken token)
        {
            return await _boothService.GetAllAsync(token);
        }

        public async Task<BoothRepoDto> GetBoothsById(int id, CancellationToken token)
        {
			return await _boothService.GetByIdAsync(id, token);
        }

        public async Task<BoothRepoDto> UpdateBoothAsync(BoothRepoDto model, CancellationToken token)
        {
			model.InsertionDate = DateTime.Now;
             await _boothService.UpdateAsync(model, token);
			return model;
        }

		#endregion


		#region Seller

		public async Task<List<SellerCommissionDto?>> GetSellersCommision(CancellationToken cancellationToken)
		{
			return await _stocksCartService.GetSellersCommision(cancellationToken);
		}
		
		public async Task<float?> GetSellersSumCommision(CancellationToken cancellationToken)
		{
			return await _stocksCartService.GetSellersSumCommision(cancellationToken);
		}

		#endregion
	}
}
