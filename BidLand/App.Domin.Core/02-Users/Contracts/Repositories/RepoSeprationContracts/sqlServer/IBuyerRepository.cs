using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
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
		Task<BuyerRepoDto> GetByIdAsync(int id,CancellationToken cancellationToken);
		Task<List<BuyerRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(BuyerRepoDto buyer, CancellationToken cancellationToken);
		Task<bool> UpdateAsync(BuyerRepoDto buyer, CancellationToken cancellationToken);
		Task<bool> SoftDeleteAsync(BuyerRepoDto buyer, CancellationToken cancellationToken);
		Task<bool> SoftRecoverAsync(BuyerRepoDto buyer, CancellationToken cancellationToken);
		Task<bool> HardDeleteAsync(BuyerRepoDto buyer, CancellationToken cancellationToken);
        Task<List<BuyerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken);
        //Task<int> UpdateWithBidAsync(BuyerRepoDto buyer, BidRepoDto bidDto, CancellationToken cancellationToken);
    }
}
