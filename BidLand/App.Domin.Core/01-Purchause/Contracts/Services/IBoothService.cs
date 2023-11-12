using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IBoothService
{
    Task CreateAsync(BoothRepoDto input, CancellationToken cancellationToken);
    Task DeleteAsync(BoothRepoDto input, CancellationToken cancellationToken);
    Task<List<BoothRepoDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<BoothRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(BoothRepoDto input, CancellationToken cancellationToken);
}
