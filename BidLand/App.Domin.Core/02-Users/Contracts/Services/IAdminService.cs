using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Services;

public interface IAdminService
{
	Task AddCommisionValueToAdmin(float? commisionValu, CancellationToken cancellationToken);
	Task<AdminRepoDto> GetAllAsync(CancellationToken cancellationToken);
	Task UpdateAsync(AdminRepoDto input, CancellationToken cancellationToken);
}
