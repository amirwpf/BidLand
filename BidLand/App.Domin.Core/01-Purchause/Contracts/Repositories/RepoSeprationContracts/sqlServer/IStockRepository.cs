﻿using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;

public interface IStockRepository
{
	Task<List<StockRepoDto>> GetAllStocks(CancellationToken cancellationToken);
	Task<StockRepoDto> GetStockById(int stockId, CancellationToken cancellationToken);
	Task AddAsync(StockRepoDto dto, CancellationToken cancellationToken);
	Task UpdateAsync(StockRepoDto stock, CancellationToken cancellationToken);
	Task DeleteAsync(StockRepoDto stock, CancellationToken cancellationToken);

}
