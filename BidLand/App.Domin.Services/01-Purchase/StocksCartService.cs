using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using App.Domin.Core._01_Purchause.Dtos;
using App.Domin.Core._02_Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class StocksCartService: IStocksCartService
{
	private readonly IStocksCartRepository _repo;

	public StocksCartService(IStocksCartRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(StocksCartRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input,cancellationToken);
	}

	public async Task DeleteAsync(StocksCartRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.HardDeleteAsync(input, cancellationToken);
	}

	public async Task<List<StocksCartRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllAsync(cancellationToken);
	}

	public async Task<StocksCartRepoDto> GetByIdAsync(int cartId, CancellationToken cancellationToken)
	{
		return await _repo.GetByCartIdAsync(cartId, cancellationToken);
	}
	
	public async Task<StocksCartRepoDto> GetById(int cartId, CancellationToken cancellationToken)
	{
		return await _repo.GetByIdAsync(cartId, cancellationToken);
	}

	public async Task UpdateAsync(StocksCartRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.UpdateAsync(input, cancellationToken);
	}

	public async Task<List<SellerCommissionDto?>> GetSellersCommision(CancellationToken cancellationToken)
	{
		return await _repo.GetCommision(cancellationToken);
	}
	
	public async Task<float?> GetSTockCommisionValue(StockRepoDto stockRepoDto,CancellationToken cancellationToken)
	{
		return await _repo.GetCommisionValue(stockRepoDto,cancellationToken);
	}
	
	public async Task<float?> GetSellersSumCommision(CancellationToken cancellationToken)
	{
		var result =  await GetSellersCommision(cancellationToken);
		return result.Select(x => x.Commision).Sum();
	}
}
