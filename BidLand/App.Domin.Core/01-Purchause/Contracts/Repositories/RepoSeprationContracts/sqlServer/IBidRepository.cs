using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IBidRepository
	{
		Task<bool> HasPlacedBid(int buyerId, int auctionId, CancellationToken cancellationToken);
		Task<List<BidRepoDto>> GetBidsByCustomerId(int buyerId, CancellationToken cancellationToken);
		Task<BidRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<List<BidRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task<int> AddAsync(BidRepoDto dto, CancellationToken cancellationToken);
		Task UpdateAsync(BidRepoDto bid, CancellationToken cancellationToken);
		Task DeleteAsync(BidRepoDto bid, CancellationToken cancellationToken);
	}
}
