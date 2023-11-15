using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.AppServices
{
    public interface IAdminPanelAppServices
    {
        Task<bool> ConfirmProduct(int productId, bool isConfirm, CancellationToken cancellationToken);
        Task<List<ProductRepoDto>> GetAllProducts(CancellationToken cancellationToken);
        Task<bool> ConfirmCommentByIdAsync(int commentId, bool isConfirm, CancellationToken cancellationToken);
        Task<List<CommentRepoDto>> GetPendingCommentsAsync(CancellationToken cancellationToken);
		Task<List<CategoryRepoDto>> GetChildCategories(CancellationToken cancellationToken);
		Task CreateProduct(ProductRepoDto model, CancellationToken cancellationToken);
		Task<bool> UpdateProduct(ProductRepoDto model, CancellationToken cancellationToken);
        Task<ProductRepoDto> GetProductById(int id, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(int id, CancellationToken cancellationToken);
        Task<List<ProductRepoDto>> GetAllConfirmedProductsAsync(CancellationToken cancellationToken);
        Task<List<ProductRepoDto>> GetAllPendingProductsAsync(CancellationToken cancellationToken);
        Task<List<BoothRepoDto>> GetAllBooths(CancellationToken token);
        Task<BoothRepoDto> GetBoothsById(int id, CancellationToken token);
        Task<BoothRepoDto> UpdateBoothAsync(BoothRepoDto model, CancellationToken token);
		Task<bool> RecoverProduct(int id, CancellationToken cancellationToken);
		Task<List<SellerCommissionDto?>> GetSellersCommision(CancellationToken cancellationToken);
		Task<float?> GetSellersSumCommision(CancellationToken cancellationToken);
	}
}
