using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IProductService
{
	Task CreateAsync(ProductRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(int id, CancellationToken cancellationToken);

	Task<List<ProductRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<ProductRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(ProductRepoDto input, int id, CancellationToken cancellationToken);
}
