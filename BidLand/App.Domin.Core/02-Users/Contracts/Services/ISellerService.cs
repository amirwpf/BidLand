using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Services;

public interface ISellerService
{
	Task CreateAsync(SellerRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(SellerRepoDto input, CancellationToken cancellationToken);

	Task<List<SellerRepoDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<SellerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken);
    Task<SellerRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task<int?> GetSumSellerCommisionAmount(CancellationToken cancellationToken);
	Task UpdateAsync(SellerRepoDto input, CancellationToken cancellationToken);
}
