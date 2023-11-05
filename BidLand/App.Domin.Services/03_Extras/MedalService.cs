using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._03_Extras;

public class MedalService: IMedalService
{
	private readonly IMedalRepository _repo;

	public MedalService(IMedalRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(MedalRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(MedalRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.HardDeleteAsync(input, cancellationToken);
	}

	public async Task<List<MedalRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<MedalRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(MedalRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
