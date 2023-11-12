using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._02_Users;

public class BuyerService: IBuyerService
{
	private readonly IBuyerRepository _repo;

	public BuyerService(IBuyerRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(BuyerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(BuyerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.SoftDeleteAsync(input, cancellationToken);
	}
    public async Task<List<BuyerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken)
    {
        return await _repo.GetAllDeletedAsync(cancellationToken);
    }
    public async Task<List<BuyerRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<BuyerRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(BuyerRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
