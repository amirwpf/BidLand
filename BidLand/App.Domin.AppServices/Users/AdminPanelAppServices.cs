using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._02_Users.Contracts.AppServices;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.AppServices.Users
{
    public class AdminPanelAppServices : IAdminPanelAppServices
    {
        private readonly IProductService _productServices;
        private readonly ICommentService _commentServices;
        public AdminPanelAppServices(IProductService productService, ICommentService commentService)
        {
            _productServices = productService;
            _commentServices = commentService;
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

    }
}
