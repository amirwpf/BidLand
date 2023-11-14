using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface ISellerRepository
	{
		//Task<SellerRepoDto> GetFinancialBySellerId(int sellerId);
		//Task<SellerRepoDto> GetByIdWithNavigationAsync(int id);
		Task<SellerRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<List<SellerRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(SellerRepoDto sellerDto, CancellationToken cancellationToken);
		Task<bool> UpdateAsync(SellerRepoDto seller, CancellationToken cancellationToken);
		Task<bool> UpdateProfileAsync(SellerRepoDto updatesellerDto, CancellationToken cancellationToken);
		Task<bool> SoftDeleteAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken);
		Task<bool> SoftRecoverAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken);
		Task<bool> HardDeleteAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken);
        Task<List<SellerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken);
		Task<int?> GetSumSellersCommisionAmount(CancellationToken cancellationToken);
	}
}
