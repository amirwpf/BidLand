using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IAuctionRepository
	{
		//Task<bool> HasOwnedAction(int userId, int auctionId);
		Task<List<AuctionRepoDto>> GetCompletedsAsync(CancellationToken cancellationToken);
		Task<int> UpdateWithBidAsync(Auction auction, BidRepoDto bidDto, CancellationToken cancellationToken);
		Task<List<AuctionRepoDto>> GetAllTrueAsync(CancellationToken cancellationToken);
		Task<AuctionRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<List<AuctionRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(AuctionRepoDto auction, CancellationToken cancellationToken);
		Task UpdateAsync(AuctionRepoDto auction, CancellationToken cancellationToken);
		Task DeleteAsync(AuctionRepoDto auction, CancellationToken cancellationToken);
	}
}
