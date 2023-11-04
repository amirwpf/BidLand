using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IBoothRepository
	{
		Task<List<BoothRepoDto>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
		Task<BoothRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<List<BoothRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken);
		Task<bool> UpdateBoothAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken);
		Task<bool> SoftDeleteAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken);
		Task<bool> SoftRecoverAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken);
		Task<bool> HardDeleteAsync(BoothRepoDto boothRepDto, CancellationToken cancellationToken);
	}
}
