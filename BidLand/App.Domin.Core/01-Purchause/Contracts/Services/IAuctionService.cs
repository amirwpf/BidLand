using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IAuctionService
{
	Task<AuctionRepoDto?> CreateAsync(AuctionRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(AuctionRepoDto input, CancellationToken cancellationToken);

	Task<List<AuctionRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<AuctionRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(AuctionRepoDto input, CancellationToken cancellationToken);
}
