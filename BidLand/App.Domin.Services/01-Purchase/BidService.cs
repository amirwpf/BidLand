using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class BidService : IBidService
{
	private readonly IBidRepository _repo;

	public BidService(IBidRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(BidRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(BidRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.DeleteAsync(input, cancellationToken);
	}

	public async Task<List<BidRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<BidRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(BidRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
