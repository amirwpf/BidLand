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
		Task AddProductToOldCartAsync(int cartId, int stockId, CancellationToken cancellationToken);
		Task<StocksCartRepoDto> GetByCartIdAsync(int cartId, CancellationToken cancellationToken);
		Task<List<StocksCartRepoDto>> GetAllAsync(CancellationToken cancellationToken);
		Task AddAsync(StocksCartRepoDto dto, CancellationToken cancellationToken);
		Task UpdateAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken);
		Task DeleteAsync(StocksCartRepoDto stocksCart, CancellationToken cancellationToken);
	}
}
