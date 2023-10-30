using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IStocksCartRepository
	{
		Task AddProductToOldCartAsync(int cartId, int stockId);
		Task<StocksCart> GetByIdAsync(int id);
		Task<List<StocksCart>> GetAllAsync();
		Task AddAsync(StocksCartRepoDto dto);
		Task UpdateAsync(StocksCart stocksCart);
		Task DeleteAsync(StocksCart stocksCart);
	}
}
