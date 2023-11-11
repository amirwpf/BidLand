using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._02_Users;

public class BuyerRepository : IBuyerRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Buyer> _dbSet;

	public BuyerRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Buyer>();
	}

	public async Task<BuyerRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(a => a.Id == id)
			.Select(a => ConvertToBuyerRepoDto(a)).FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;
		return result;
	}

	public async Task<List<BuyerRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Select(a => ConvertToBuyerRepoDto(a)).ToListAsync(cancellationToken);
		return result;
	}

	public async Task AddAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
	{
		var result = new Buyer();
		Equaler(buyer, ref result);
		await _dbSet.AddAsync(result);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
		if (result != null)
		{
			Equaler(buyer, ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;		
	}
	public async Task<bool> HardDeleteAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
		if(result != null)
		{
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftDeleteAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
		if (result != null)
		{
			result.IsDelete= true;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftRecoverAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
		if (result != null)
		{
			result.IsDelete = false;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	//public async Task<int> UpdateWithBidAsync(BuyerRepoDto buyer, BidRepoDto bidDto, CancellationToken cancellationToken)
	//{
	//	var bid = new Bid
	//	{
	//		Price = bidDto.Price,
	//		BidDate = bidDto.BidDate,
	//		AuctionId = bidDto.AuctionId,
	//		Buyer = bidDto.Buyer
	//	};
	//	buyer.Bids.Add(bid);
	//	_context.Entry(buyer).State = EntityState.Modified;
	//	var result = await _context.SaveChangesAsync(cancellationToken);
	//	return result;
	//}

	private BuyerRepoDto ConvertToBuyerRepoDto(Buyer buyer)
	{
		return new BuyerRepoDto()
		{
			Id = buyer.Id,
			//FullName = buyer.FullName,
			Credit = buyer.Credit,
			IsBan = buyer.IsBan,
			TotalPurchaseAmount = buyer.TotalPurchaseAmount,
			InsertionDate = buyer.InsertionDate,
			Addresses = buyer.Addresses,
			Bids = buyer.Bids,
			Carts = buyer.Carts,
			Comments = buyer.Comments
		};
	}

	private void Equaler(BuyerRepoDto buyer, ref Buyer b)
	{
		b.Id = buyer.Id;
		//b.FullName = buyer.FullName;
		b.Credit = buyer.Credit;
		b.IsBan = buyer.IsBan;
		b.TotalPurchaseAmount = buyer.TotalPurchaseAmount;
		b.InsertionDate = buyer.InsertionDate;
		b.Addresses = buyer.Addresses;
		b.Bids = buyer.Bids;
		b.Carts = buyer.Carts;
		b.Comments = buyer.Comments;
	}
}
