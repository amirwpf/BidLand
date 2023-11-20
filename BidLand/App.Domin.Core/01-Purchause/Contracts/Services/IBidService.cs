using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IBidService
{
	Task BidWon(BidRepoDto input, CancellationToken cancellationToken);
	Task CreateAsync(BidRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(BidRepoDto input, CancellationToken cancellationToken);

	Task<List<BidRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<BidRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
	Task SubtractSalesValueFromBuyer(BidRepoDto input, float? commisionValue, CancellationToken cancellationToken);
	Task UpdateAsync(BidRepoDto input, CancellationToken cancellationToken);
}
