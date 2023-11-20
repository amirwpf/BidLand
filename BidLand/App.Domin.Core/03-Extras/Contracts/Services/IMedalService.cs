using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Services;

public interface IMedalService
{
	Task CreateAsync(MedalRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(MedalRepoDto input, CancellationToken cancellationToken);

	Task<List<MedalRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<MedalRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<MedalRepoDto> GetMedalByNameAsync(MedalEnum? medal, CancellationToken cancellationToken);
	Task UpdateAsync(MedalRepoDto input, CancellationToken cancellationToken);
}
