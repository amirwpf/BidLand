using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Contracts.Services;
using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._02_Users;

public class AdminService: IAdminService
{
	private readonly IAdminRepository _repo;

	public AdminService(IAdminRepository repo)
	{
		_repo = repo;
	}
    public async Task<AdminRepoDto> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task UpdateAsync(AdminRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}
	public async Task AddCommisionValueToAdmin(float? commisionValu, CancellationToken cancellationToken)
	{
		var input = await _repo.GetAllAsync(cancellationToken);
		input.SiteCommissionIncome += (int)commisionValu;
		await _repo.UpdateAsync(input, cancellationToken);
	}
}
