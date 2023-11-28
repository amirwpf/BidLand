using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface ICartService
{
	Task CreateAsync(CartRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(CartRepoDto input, CancellationToken cancellationToken);

	Task<List<CartRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<CartRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(CartRepoDto input, CancellationToken cancellationToken);
}
