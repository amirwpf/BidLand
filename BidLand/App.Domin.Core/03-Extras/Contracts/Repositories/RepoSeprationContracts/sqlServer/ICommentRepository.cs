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
		Task<Comment> GetByIdAsync(int id);
		Task<List<CommentRepoDto>> GetByProductIdAsync(int productId);
		Task<List<Comment>> GetAllAsync();
		Task<int> AddAsync(CommentRepoDto comment);
		Task UpdateAsync(Comment comment);
		Task DeleteAsync(Comment comment);
		Task<List<Comment>> GetAllCommentsWithSellerNameConfirmAsync();
	}
}
