using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._03_Extras.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IMedalRepository
	{
		Task<MedalRepoDto> GetByIdAsync(int id,CancellationToken cancellationToken);
		Task<List<MedalRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(MedalRepoDto medal, CancellationToken cancellationToken);
		Task UpdateAsync(MedalRepoDto medal, CancellationToken cancellationToken);
		Task DeleteAsync(MedalRepoDto medal, CancellationToken cancellationToken);
		Task<MedalRepoDto> GetMedalByTypeAsync(MedalEnum medalType, CancellationToken cancellationToken);
	}
}
