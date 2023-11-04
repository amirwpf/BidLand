﻿using App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Services._01_Purchase;

public class StockService: IStockService
{
	private readonly IStockRepository _repo;

	public StockService(IStockRepository repo)
	{
		_repo = repo;
	}
	public async Task CreateAsync(StockRepoDto input, CancellationToken cancellationToken)
	{
		await _repo.AddAsync(input, cancellationToken);
	}

	public async Task<bool> HardDeleteAsync(StockRepoDto input, CancellationToken cancellationToken)
	{
		return await _repo.HardDeleteAsync(input, cancellationToken);
	}
	public async Task<bool> SoftDeleteAsync(StockRepoDto input, CancellationToken cancellationToken)
	{
		return await _repo.SoftDeleteAsync(input, cancellationToken);
	}
	public async Task<bool> SoftRecoverAsync(StockRepoDto input, CancellationToken cancellationToken)
	{
		return await _repo.SoftRecoverAsync(input, cancellationToken);
	}

	public async Task<List<StockRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		return await _repo.GetAllStocks(cancellationToken);
	}

	public async Task<StockRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		return await _repo.GetStockById(id, cancellationToken);
	}

	public async Task<bool> UpdateAsync(StockRepoDto input, CancellationToken cancellationToken)
	{
		return await _repo.UpdateAsync(input, cancellationToken);
	}
}
