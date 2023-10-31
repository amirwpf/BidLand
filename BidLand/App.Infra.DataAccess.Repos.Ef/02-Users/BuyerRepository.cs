using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._02_Users
{
	public class BuyerRepository : IBuyerRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Buyer> _dbSet;

		public BuyerRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Buyer>();
		}

		public async Task<BuyerRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(a=>a.Id==id)
				.Select(a=>new BuyerRepoDto
				{
					Id = a.Id,
					FullName= a.FullName,
					Credit= a.Credit,
					IsBan= a.IsBan,
					TotalPurchaseAmount= a.TotalPurchaseAmount,
					InsertionDate= a.InsertionDate,
					Addresses= a.Addresses,
					Bids= a.Bids,
					Carts= a.Carts,
					Comments = a.Comments
				}).FirstOrDefaultAsync(cancellationToken);
			return result;
		}

		public async Task<List<BuyerRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var result = await _dbSet
				.Select(a => new BuyerRepoDto
				{
					Id = a.Id,
					FullName = a.FullName,
					Credit = a.Credit,
					IsBan = a.IsBan,
					TotalPurchaseAmount = a.TotalPurchaseAmount,
					InsertionDate = a.InsertionDate,
					Addresses = a.Addresses,
					Bids = a.Bids,
					Carts = a.Carts,
					Comments = a.Comments
				}).ToListAsync(cancellationToken);
			return result;
		}

		public async Task AddAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
			await _dbSet.AddAsync(result);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}
		public async Task<int> UpdateWithBidAsync(BuyerRepoDto buyer, BidRepoDto bidDto, CancellationToken cancellationToken)
		{
			var bid = new Bid
			{
				Price = bidDto.Price,
				BidDate = bidDto.BidDate,
				AuctionId = bidDto.AuctionId,
				Buyer = bidDto.Buyer
			};
			buyer.Bids.Add(bid);
			_context.Entry(buyer).State = EntityState.Modified;
			var result = await _context.SaveChangesAsync(cancellationToken);
			return result;
		}
		public async Task DeleteAsync(BuyerRepoDto buyer, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == buyer.Id, cancellationToken);
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

}
