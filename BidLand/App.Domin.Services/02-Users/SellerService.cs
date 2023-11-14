using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._02_Users;

public class SellerService: ISellerService
{
	private readonly ISellerRepository _repo;

	public SellerService(ISellerRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(SellerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(SellerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.SoftDeleteAsync(input, cancellationToken);
	}
public async Task<List<SellerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllDeletedAsync(cancellationToken);
	}

	public async Task<List<SellerRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<int?> GetSumSellerCommisionAmount(CancellationToken cancellationToken)
	{
		return await _repo.GetSumSellersCommisionAmount(cancellationToken);
	}

	public async Task<SellerRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(SellerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
