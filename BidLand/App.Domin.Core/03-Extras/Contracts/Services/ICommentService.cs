using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Services;

public interface ICommentService
{
    Task<bool> ConfirmCommentByIdAsync(int commentId, bool isConfirm, CancellationToken cancellationToken);
    Task CreateAsync(CommentRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(CommentRepoDto input, CancellationToken cancellationToken);

	Task<List<CommentRepoDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<CommentRepoDto>> GetAllCommentsWithSellerNameConfirmAsync(CancellationToken cancellationToken);
    Task<List<CommentRepoDto>> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(CommentRepoDto input, CancellationToken cancellationToken);
}

