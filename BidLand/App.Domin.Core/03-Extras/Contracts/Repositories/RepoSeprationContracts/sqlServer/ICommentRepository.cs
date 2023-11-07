using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface ICommentRepository
	{
		Task<List<CommentRepoDto>> GetByIdAsync(int id,CancellationToken cancellationToken);
		Task<List<CommentRepoDto>> GetByStockIdAsync(int stockId, CancellationToken cancellationToken);
		Task<List<CommentRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task<int> AddAsync(CommentRepoDto comment, CancellationToken cancellationToken);
		Task UpdateAsync(CommentRepoDto comment, CancellationToken cancellationToken);
		Task<bool> HardDeleteAsync(CommentRepoDto comment, CancellationToken cancellationToken);
		Task<bool> ConfirmCommentByIdAsync(int commentId , bool isConfirm, CancellationToken cancellationToken);
		Task<List<CommentRepoDto>> GetAllCommentsWithSellerNameConfirmAsync(CancellationToken cancellationToken);
		
	}
}
