using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._01_Purchause.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class BoothService : IBoothService
{
	private readonly IBoothRepository _repo;

	public BoothService(IBoothRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(BoothRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(BoothRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.DeleteAsync(input, cancellationToken);
	}

	public async Task<List<BoothRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<BoothRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(BoothRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateBoothAsync(input, cancellationToken);
	}
}
