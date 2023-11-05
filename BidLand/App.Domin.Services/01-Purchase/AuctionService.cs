using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class AuctionService : IAuctionService
{
	private readonly IAuctionRepository _repo;

	public AuctionService(IAuctionRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(AuctionRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(AuctionRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.HardDeleteAsync(input, cancellationToken);
	}

	public async Task<List<AuctionRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<AuctionRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(AuctionRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
