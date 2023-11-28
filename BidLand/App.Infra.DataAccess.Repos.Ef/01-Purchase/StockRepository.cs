using App.Domin.Core._01_Purchase.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class StockRepository : IStockRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Stock> _dbSet;

	public StockRepository(AppDbContext appDbContext)
	{
		_context = appDbContext;
		_dbSet = _context.Set<Stock>();
	}
	public async Task<List<StockRepoDto>> GetAllStocks(CancellationToken cancellationToken)
	{
		var result = await _context.Stocks.AsNoTracking()
			.Include(x => x.Booth)
			.ThenInclude(x => x.Seller)
			.ThenInclude(x => x.Medal)
			.Include(x => x.Product)
			.ThenInclude(x => x.Images)
			.Include(x => x.StocksCarts)
			.Include(x => x.Comments)
			.Select(stockRepoDto => new StockRepoDto
			{
				AdditionalDescription = stockRepoDto.AdditionalDescription,
				//AuctionId = stockRepoDto.AuctionId,
				AvailableNumber = stockRepoDto.AvailableNumber,
				BoothId = stockRepoDto.BoothId,
				Id = stockRepoDto.Id,
				InsertionDate = stockRepoDto.InsertionDate,
				IsActive = stockRepoDto.IsActive,
				IsAuction = stockRepoDto.IsAuction,
				IsDelete = stockRepoDto.IsDelete,
				Price = stockRepoDto.Price,
				ProductId = stockRepoDto.ProductId,
				//Auction = stockRepoDto.Auction,
				Booth = stockRepoDto.Booth,
				Comments = stockRepoDto.Comments,
				Product = stockRepoDto.Product,
				Auctions = stockRepoDto.Auctions,
				StocksCarts = stockRepoDto.StocksCarts
			}).ToListAsync(cancellationToken);
		return result;
	}

	public async Task<StockRepoDto?> GetStockById(int stockId, CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Include(x => x.Booth)
			.ThenInclude(x=>x.Seller)
			.ThenInclude(x=>x.Medal)
			.Include(x => x.Product)
			.ThenInclude(x=>x.Images)
			.Include(x => x.StocksCarts)
			.Include(x => x.Comments)
			.Select(stockRepoDto => new StockRepoDto
			{
				AdditionalDescription = stockRepoDto.AdditionalDescription,
				//AuctionId = stockRepoDto.AuctionId,
				AvailableNumber = stockRepoDto.AvailableNumber,
				BoothId = stockRepoDto.BoothId,
				Id = stockRepoDto.Id,
				InsertionDate = stockRepoDto.InsertionDate,
				IsActive = stockRepoDto.IsActive,
				IsAuction = stockRepoDto.IsAuction,
				IsDelete = stockRepoDto.IsDelete,
				Price = stockRepoDto.Price,
				ProductId = stockRepoDto.ProductId,
				//Auction = stockRepoDto.Auction,
				Booth = stockRepoDto.Booth,
				Comments = stockRepoDto.Comments,
				Product = stockRepoDto.Product,
				StocksCarts = stockRepoDto.StocksCarts,
				Auctions = stockRepoDto.Auctions,
			})
			.FirstOrDefaultAsync(x => x.Id == stockId, cancellationToken);
		if (result == null)
			return null;
		return result;
	}

	public async Task AddAsync(StockRepoDto stockRepoDto, CancellationToken cancellationToken)
	{
		var productsCart = new Stock()
		{
			AdditionalDescription = stockRepoDto.AdditionalDescription,
			//AuctionId = stockRepoDto.AuctionId,
			AvailableNumber = stockRepoDto.AvailableNumber,
			BoothId = stockRepoDto.BoothId,
			//Id = stockRepoDto.Id,
			InsertionDate = stockRepoDto.InsertionDate,
			IsActive = stockRepoDto.IsActive,
			IsAuction = stockRepoDto.IsAuction,
			IsDelete = stockRepoDto.IsDelete,
			Price = stockRepoDto.Price,
			ProductId = stockRepoDto.ProductId,
			//Auction = stockRepoDto.Auction,
			Booth = stockRepoDto.Booth,
			Comments = stockRepoDto.Comments,
			Product = stockRepoDto.Product,
			StocksCarts = stockRepoDto.StocksCarts,
			Auctions = stockRepoDto.Auctions,
		};
		await _dbSet.AddAsync(productsCart, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == stock.Id, cancellationToken);
		if (res != null)
		{
			UpdateValueEq(stock, ref res);
			//_dbSet.Update(res);
			_context.Entry(res).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			_context.ChangeTracker.Clear();
			return true;
		}
		return false;
	}

	public async Task<bool> SoftDeleteAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == stock.Id, cancellationToken);
		if (res != null)
		{
			res.IsDelete = true;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftRecoverAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == stock.Id, cancellationToken);
		if (res != null)
		{
			res.IsDelete = false;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(StockRepoDto stock, CancellationToken cancellationToken)
	{
		var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == stock.Id, cancellationToken);
		if (res != null)
		{
			_dbSet.Remove(res);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}


	private Stock ConvertToStock(StockRepoDto stockRepoDto)
	{
		var result = new Stock()
		{
			AdditionalDescription = stockRepoDto.AdditionalDescription,
			//AuctionId = stockRepoDto.AuctionId,
			AvailableNumber = stockRepoDto.AvailableNumber,
			BoothId = stockRepoDto.BoothId,
			//Id = stockRepoDto.Id,
			InsertionDate = stockRepoDto.InsertionDate,
			IsActive = stockRepoDto.IsActive,
			IsAuction = stockRepoDto.IsAuction,
			IsDelete = stockRepoDto.IsDelete,
			Price = stockRepoDto.Price,
			ProductId = stockRepoDto.ProductId,
			//Auction = stockRepoDto.Auction,
			Booth = stockRepoDto.Booth,
			Comments = stockRepoDto.Comments,
			Product = stockRepoDto.Product,
			StocksCarts = stockRepoDto.StocksCarts,
			Auctions = stockRepoDto.Auctions,
		};
		return result;
	}

	private StockRepoDto ConvertToStockRepoDto(Stock stock)
	{
		var result = new StockRepoDto()
		{
			AdditionalDescription = stock.AdditionalDescription,
			//AuctionId = stock.AuctionId,
			AvailableNumber = stock.AvailableNumber,
			BoothId = stock.BoothId,
			//Id = stock.Id,
			InsertionDate = stock.InsertionDate,
			IsActive = stock.IsActive,
			IsAuction = stock.IsAuction,
			IsDelete = stock.IsDelete,
			Price = stock.Price,
			ProductId = stock.ProductId,
			//Auction = stock.Auction,
			Booth = stock.Booth,
			Comments = stock.Comments,
			Product = stock.Product,
			StocksCarts = stock.StocksCarts,
			Auctions = stock.Auctions,
		};
		return result;
	}

	private void UpdateValueEq(StockRepoDto stockRepoDto, ref Stock stock)
	{
		stock.AdditionalDescription = stockRepoDto.AdditionalDescription;
		//stock.AuctionId = stock.AuctionId;
		stock.AvailableNumber = stockRepoDto.AvailableNumber;
		stock.BoothId = stockRepoDto.BoothId;
		//stock.Id = stockRepoDto.Id;
		stock.InsertionDate = stockRepoDto.InsertionDate;
		stock.IsActive = stockRepoDto.IsActive;
		stock.IsAuction = stockRepoDto.IsAuction;
		stock.IsDelete = stockRepoDto.IsDelete;
		stock.Price = stockRepoDto.Price;
		stock.ProductId = stockRepoDto.ProductId;
		//stock.Auction = stock.Auction;
		stock.Booth = stockRepoDto.Booth;
		stock.Comments = stockRepoDto.Comments;
		stock.Product = stockRepoDto.Product;
		stock.StocksCarts = stockRepoDto.StocksCarts;
		stock.Auctions = stockRepoDto.Auctions;
	}
}
