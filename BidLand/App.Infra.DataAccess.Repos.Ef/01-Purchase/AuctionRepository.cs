using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class AuctionRepository : IAuctionRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Auction> _dbSet;
	public AuctionRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Auction>();
	}

	public async Task<List<AuctionRepoDto>> GetCompletedsAsync(CancellationToken cancellationToken)
	{
		var completedAuctions = await _dbSet
			.AsNoTracking()
			.Include(a => a.Stock)
			.ThenInclude(a => a.Product)
			.ThenInclude(a => a.Images)
			.Include(a => a.Stock)
			.ThenInclude(a => a.Booth)
			.ThenInclude(a => a.Seller)
			.ThenInclude(a => a.User)
			.Include(a => a.Bids)
			.ThenInclude(a => a.Buyer)
			.ThenInclude(a => a.User)
			.Where(a => a.EndDate <= DateTime.Now)
			.Select(auction => new AuctionRepoDto()
			{
				Id = auction.Id,
				StartDate = auction.StartDate,
				EndDate = auction.EndDate,
				IsActive = auction.IsActive,
				IsDelete = auction.IsDelete,
				CurrentHighestPrice = auction.CurrentHighestPrice,
				MinimumFinalPrice = auction.MinimumFinalPrice,
				StockId = auction.StockId,
				InsertionDate = auction.InsertionDate,
				Bids = auction.Bids,
				Stock = auction.Stock,
				JobId = auction.JobId,
			})
			.ToListAsync(cancellationToken);
		return completedAuctions;
	}

	public async Task<AuctionRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var action = await _dbSet.AsNoTracking()
				.Include(a => a.Stock)
				.ThenInclude(a => a.Product)
				.ThenInclude(a => a.Images)
				.Include(a => a.Stock)
				.ThenInclude(a => a.Booth)
				.ThenInclude(a => a.Seller)
				.ThenInclude(a => a.User)
				.Include(a => a.Bids)
				.ThenInclude(a => a.Buyer)
				.ThenInclude(a => a.User)
				.Select(auction => new AuctionRepoDto()
				{
					Id = auction.Id,
					StartDate = auction.StartDate,
					EndDate = auction.EndDate,
					IsActive = auction.IsActive,
					IsDelete = auction.IsDelete,
					CurrentHighestPrice = auction.CurrentHighestPrice,
					MinimumFinalPrice = auction.MinimumFinalPrice,
					StockId = auction.StockId,
					Bids = auction.Bids,
					Stock = auction.Stock,
					JobId = auction.JobId,
					InsertionDate = auction.InsertionDate,
				})
			   .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		if (action == null) return null;
		return action;
	}
	public async Task<List<AuctionRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Include(a => a.Stock)
			.ThenInclude(a => a.Product)
			.ThenInclude(a => a.Images)
			.Include(a => a.Stock)
			.ThenInclude(a => a.Booth)
			.ThenInclude(a => a.Seller)
			.ThenInclude(a => a.User)
			.Include(a => a.Bids)
			.ThenInclude(a => a.Buyer)
			.ThenInclude(a => a.User)
			.Select(auction => new AuctionRepoDto()
			{
				Id = auction.Id,
				StartDate = auction.StartDate,
				EndDate = auction.EndDate,
				IsActive = auction.IsActive,
				IsDelete = auction.IsDelete,
				CurrentHighestPrice = auction.CurrentHighestPrice,
				MinimumFinalPrice = auction.MinimumFinalPrice,
				StockId = auction.StockId,
				InsertionDate = auction.InsertionDate,
				Bids = auction.Bids,
				Stock = auction.Stock,
				JobId = auction.JobId,
			}).ToListAsync(cancellationToken);
		return result;
	}
	public async Task<List<AuctionRepoDto>> GetAllTrueAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.AsNoTracking()
			.Include(a => a.Stock)
			.ThenInclude(a => a.Product)
			.ThenInclude(a => a.Images)
			.Include(a => a.Stock)
			.ThenInclude(a => a.Booth)
			.ThenInclude(a => a.Seller)
			.ThenInclude(a => a.User)
			.Include(a => a.Bids)
			.ThenInclude(a => a.Buyer)
			.ThenInclude(a => a.User)
			.Select(auction => new AuctionRepoDto()
			{
				Id = auction.Id,
				StartDate = auction.StartDate,
				EndDate = auction.EndDate,
				IsActive = auction.IsActive,
				IsDelete = auction.IsDelete,
				CurrentHighestPrice = auction.CurrentHighestPrice,
				MinimumFinalPrice = auction.MinimumFinalPrice,
				StockId = auction.StockId,
				InsertionDate = auction.InsertionDate,
				Bids = auction.Bids,
				Stock = auction.Stock,
				JobId= auction.JobId,
			}).ToListAsync(cancellationToken);
		return result;
	}
	public async Task<AuctionRepoDto?> AddAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = new Auction();
		Equaler(auction, ref result);
		//await _dbSet.AddAsync(result,cancellationToken);
		_context.Entry(result).State = EntityState.Added;
		await _context.SaveChangesAsync(cancellationToken);

		var allAuctions = await GetAllTrueAsync(cancellationToken);
		var Auction=allAuctions.FirstOrDefault(a=>a.InsertionDate==auction.InsertionDate &&
												  a.StartDate ==auction.StartDate &&
												  a.EndDate == auction.EndDate &&
												  a.StockId == auction.StockId &&
												  a.IsActive == auction.IsActive &&
												  a.IsDelete == auction.IsDelete);
		return Auction;
	}
	public async Task<bool> UpdateAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FindAsync(auction.Id,cancellationToken);
		if (result != null)
		{
			Equaler(auction, ref result);
			_dbSet.Update(result);
			//_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}
	public async Task<bool> SoftDeleteAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id, cancellationToken);
		if (result != null)
		{
			result.IsDelete = true;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftRecoverAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id, cancellationToken);
		if (result != null)
		{
			result.IsDelete = false;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(AuctionRepoDto auction, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == auction.Id, cancellationToken);
		if (result != null)
		{
			_dbSet.Remove(result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	//public async Task<int> UpdateWithBidAsync(Auction auction, BidRepoDto bidDto, CancellationToken cancellationToken)
	//{
	//	var bid = new Bid
	//	{
	//		Price = bidDto.Price,
	//		BidDate = bidDto.BidDate,
	//		AuctionId = bidDto.AuctionId,
	//		BuyerId = bidDto.BuyerId
	//	};
	//	auction.Bids.Add(bid);
	//	_context.Entry(auction).State = EntityState.Modified;
	//	var result = await _context.SaveChangesAsync(cancellationToken);
	//	return result;
	//}

	//public async Task<bool> HasOwnedAction(int userId, int auctionId)
	//{
	//	var action = await _dbSet.Where(s => s.Stock.Booth.SellerId == userId)
	//		.FirstOrDefaultAsync(a => a.Id == auctionId);
	//	if (action == null)
	//		return false;
	//	return true;
	//}

	private AuctionRepoDto ConvertToAuctionRepoDto(Auction auction)
	{
		return new AuctionRepoDto()
		{
			//Id = auction.Id,
			StartDate = auction.StartDate,
			EndDate = auction.EndDate,
			IsActive = auction.IsActive,
			IsDelete = auction.IsDelete,
			CurrentHighestPrice = auction.CurrentHighestPrice,
			MinimumFinalPrice = auction.MinimumFinalPrice,
			StockId = auction.StockId,
			InsertionDate = auction.InsertionDate,
			Bids = auction.Bids,
			Stock = auction.Stock,
			JobId = auction.JobId,
		};
	}

	private void Equaler(AuctionRepoDto auctionRepoDto, ref Auction auction)
	{
		//auction.Id = auctionRepoDto.Id;
		auction.StartDate = auctionRepoDto.StartDate;
		auction.EndDate = auctionRepoDto.EndDate;
		auction.IsActive = auctionRepoDto.IsActive;
		auction.IsDelete = auctionRepoDto.IsDelete;
		auction.CurrentHighestPrice = auctionRepoDto.CurrentHighestPrice;
		auction.MinimumFinalPrice = auctionRepoDto.MinimumFinalPrice;
		auction.StockId = auctionRepoDto.StockId;
		auction.InsertionDate = auctionRepoDto.InsertionDate;
		auction.Bids = auctionRepoDto.Bids;
		auction.Stock = auctionRepoDto.Stock;
		auction.JobId = auctionRepoDto.JobId;
	}
}
