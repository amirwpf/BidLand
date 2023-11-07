using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
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
        
    }
}
