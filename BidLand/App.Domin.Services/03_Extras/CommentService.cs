using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._03_Extras;

public class CommentService: ICommentService
{
	private readonly ICommentRepository _repo;

	public CommentService(ICommentRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(CommentRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(CommentRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.HardDeleteAsync(input, cancellationToken);
	}

	public async Task<List<CommentRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<List<CommentRepoDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(CommentRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
