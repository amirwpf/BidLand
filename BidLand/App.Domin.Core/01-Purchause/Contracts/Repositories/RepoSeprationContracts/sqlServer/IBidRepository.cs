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
		Task<bool> HasPlacedBid(int customerId, int auctionId);
		Task<List<BidRepoDto>> GetBidsByCustomerId(int customerId);
		Task<Bid> GetByIdAsync(int id);
		Task<List<Bid>> GetAllAsync();
		Task<int> AddAsync(BidRepoDto dto);
		Task UpdateAsync(Bid bid);
		Task DeleteAsync(Bid bid);
	}
}
