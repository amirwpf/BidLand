using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IBuyerRepository
	{
		Task<int> UpdateWithBidAsync(Buyer buyer, BidRepoDto bidDto);
		Task<Buyer> GetByIdAsync(int id);
		Task<List<Buyer>> GetAllAsync();
		Task AddAsync(Buyer buyer);
		Task UpdateAsync(Buyer buyer);
		Task DeleteAsync(Buyer buyer);
	}
}
