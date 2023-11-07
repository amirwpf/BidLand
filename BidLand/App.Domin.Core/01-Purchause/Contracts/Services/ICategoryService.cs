using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface ICategoryService
{
	Task CreateAsync(CategoryRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(CategoryRepoDto input, CancellationToken cancellationToken);

	Task<List<CategoryRepoDto>> GetAllAsync(CancellationToken cancellationToken);
	Task<List<CategoryRepoDto>> GetAllChildAsync(CancellationToken cancellationToken);
	Task<CategoryRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(CategoryRepoDto input, CancellationToken cancellationToken);
}
