using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
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
		Task<List<StocksCartRepoDto>> GetProductsOpenCartByCartIdAsync(int cartId);
		Task<string> DeleteOpenCartAsync(int customerId, int cartId);
		Task<int> GetTotalPrices(int cartId);
		Task<List<CartRepoDto>> GetfinalizedCartsByCustomerId(int customerId);
		Task<List<CartRepoDto>> GetUnfinalizedCartsByCustomerId(int customerId);
		Task<List<StocksCartRepoDto>> GetProductsByCartIdAsync(int cartId);
		Task<bool> FinalizeCartAsync(int cartId);//
		Task<List<Cart>> GetOpenCartsForCustomerIdByBoothIdAsync(int boothId, int cudtomerId);
		Task<Cart> GetByIdAsync(int id);
		Task<List<Cart>> GetAllAsync();
		Task<CartRepoDto> AddAsync(CartRepoDto dto);
		Task UpdateAsync(Cart cart);
		Task DeleteAsync(Cart cart);
	}
}
