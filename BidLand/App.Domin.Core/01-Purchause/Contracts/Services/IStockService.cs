﻿using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Services;

public interface IStockService
{
	Task CreateAsync(StockRepoDto input, CancellationToken cancellationToken);

	Task DeleteAsync(StockRepoDto input, CancellationToken cancellationToken);

	Task<List<StockRepoDto>> GetAllAsync(CancellationToken cancellationToken);

	Task<StockRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken);

	Task UpdateAsync(StockRepoDto input, CancellationToken cancellationToken);
}