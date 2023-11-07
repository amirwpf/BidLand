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
    Task<bool> ConfirmProductAsync(int productId, bool confirm, CancellationToken cancellationToken);
    Task CreateAsync(ProductRepoDto input, CancellationToken cancellationToken);

	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

	Task<List<ProductRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<ProductRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task<bool> UpdateAsync(ProductRepoDto input,  CancellationToken cancellationToken);
}
