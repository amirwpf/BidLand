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
		Task<bool> HasOwnedAction(int userId, int auctionId);
		Task<List<Auction>> GetCompletedsAsync();
		Task<int> UpdateWithBidAsync(Auction auction, BidRepoDto bidDto);
		Task<List<AuctionRepoDto>> GetAllTrueAsync();
		Task<Auction> GetByIdAsync(int id);
		Task<List<AuctionRepoDto>> GetAllAsync();
		Task AddAsync(Auction auction);
		Task UpdateAsync(Auction auction);
		Task DeleteAsync(Auction auction);
	}
}
