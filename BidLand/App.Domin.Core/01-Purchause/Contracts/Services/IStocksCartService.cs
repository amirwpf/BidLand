using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IStocksCartService
{
	Task CreateAsync(StocksCartRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(StocksCartRepoDto input, CancellationToken cancellationToken);

	Task<List<StocksCartRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<StocksCartRepoDto> GetByIdAsync(int cartId, CancellationToken cancellationToken);
	Task<List<SellerCommissionDto?>> GetSellersCommision(CancellationToken cancellationToken);
	Task<float?> GetSellersSumCommision(CancellationToken cancellationToken);
	Task UpdateAsync(StocksCartRepoDto input, CancellationToken cancellationToken);
}