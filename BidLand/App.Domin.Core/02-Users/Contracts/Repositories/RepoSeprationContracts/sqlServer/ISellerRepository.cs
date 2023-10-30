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
		Task<SellerRepoDto> GetFinancialBySellerId(int sellerId);
		Task<SellerRepoDto> GetByIdWithNavigationAsync(int id);
		Task<Seller> GetByIdAsync(int id);
		Task<List<Seller>> GetAllAsync();
		Task AddAsync(SellerRepoDto seller);
		Task<bool> UpdateAsync(SellerRepoDto seller);
		Task<bool> UpdateProfileAsync(SellerRepoDto updatesellerDto);
		Task DeleteAsync(int seller);
	}
}
