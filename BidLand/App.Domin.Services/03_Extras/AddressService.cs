using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._03_Extras;

public class AddressService: IAddressService
{
	private readonly IAddressRepository _repo;

	public AddressService(IAddressRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(AddressRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task DeleteAsync(AddressRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.DeleteAsync(input, cancellationToken);
	}

	public async Task<List<AddressRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<AddressRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(id, cancellationToken);
	}

	public async Task UpdateAsync(AddressRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
