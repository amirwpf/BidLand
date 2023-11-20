using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Services;

public interface IBuyerService
{
	Task CreateAsync(BuyerRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(BuyerRepoDto input, CancellationToken cancellationToken);

	Task<List<BuyerRepoDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<BuyerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken);
    Task<BuyerRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task SubtractSalesValueFromBuyer(BuyerRepoDto input, float? commisionValue, CancellationToken cancellationToken);
	Task UpdateAsync(BuyerRepoDto input, CancellationToken cancellationToken);
}
