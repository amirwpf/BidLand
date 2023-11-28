using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface ICartRepository
	{
		Task<string> DeleteOpenCartAsync(int buyerId, int cartId, CancellationToken cancellationToken);
		Task<int> GetTotalPrices(int cartId, CancellationToken cancellationToken);
		Task<List<BidResponseDto>> GetCompeletedCartsByCustomerId(int buyerId, CancellationToken cancellationToken);
		Task<List<BidResponseDto>> GetNonCompeletedCartsByBuyerId(int buyerId, CancellationToken cancellationToken);
		Task<bool> FinalizeCartAsync(int cartId, CancellationToken cancellationToken);
		Task<CartRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
		Task<List<CartRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(CartRepoDto dto, CancellationToken cancellationToken);
		Task UpdateAsync(CartRepoDto cart, CancellationToken cancellationToken);
		Task<bool> DeleteAsync(CartRepoDto cart, CancellationToken cancellationToken);
	}
}
