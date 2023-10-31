using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._03_Extras
{
	public class CommentRepository : ICommentRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Comment> _dbSet;

		public CommentRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Comment>();
		}

		public async Task<List<CommentRepoDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var comments = await _dbSet.AsNoTracking()
				.Where(c => c.Id == id /*&& c.IsConfirm==true*/)
				.Include(p => p.Stock)
				.Include(c => c.Buyer)
				.ToListAsync(cancellationToken);
			var result = comments.Select(c => new CommentRepoDto
			{
				Id = c.Id,
				Title = c.Title,
				Description = c.Description,
				Advantages = c.Advantages,
				Buyer = c.Buyer,
				BuyerId = c.BuyerId,
				ConfirmDate = c.ConfirmDate,
				DisAdvantages = c.DisAdvantages,
				IsConfirm = c.IsConfirm,
				IsPositive = c.IsPositive,
				StockId = c.StockId,
				Stock = c.Stock
			}).ToList();
			return result;
		}

		public async Task<List<CommentRepoDto>> GetByStockIdAsync(int stockId, CancellationToken cancellationToken)
		{
			var comments = await _dbSet.AsNoTracking()
				.Where(c => c.StockId == stockId /*&& c.IsConfirm==true*/)
				.Include(p => p.Stock)
				.Include(c => c.Buyer)
				.ToListAsync(cancellationToken);
			var result = comments.Select(c => new CommentRepoDto
			{
				Id = c.Id,
				Title = c.Title,
				Description = c.Description,
				Advantages= c.Advantages,
				Buyer= c.Buyer,
				BuyerId= c.BuyerId,
				ConfirmDate= c.ConfirmDate,
				DisAdvantages = c.DisAdvantages,
				IsConfirm = c.IsConfirm,
				IsPositive= c.IsPositive,
				StockId= c.StockId,
				Stock = c.Stock
			}).ToList();
			return result;
		}

		public async Task<List<CommentRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var comments = await _dbSet.AsNoTracking()
				.Include(p => p.Stock)
				.Include(c => c.Buyer)
				.ToListAsync(cancellationToken);
			var result = comments.Select(c => new CommentRepoDto
			{
				Id = c.Id,
				Title = c.Title,
				Description = c.Description,
				Advantages = c.Advantages,
				Buyer = c.Buyer,
				BuyerId = c.BuyerId,
				ConfirmDate = c.ConfirmDate,
				DisAdvantages = c.DisAdvantages,
				IsConfirm = c.IsConfirm,
				IsPositive = c.IsPositive,
				StockId = c.StockId,
				Stock = c.Stock
			}).ToList();
			return result;
		}
		public async Task<List<CommentRepoDto>> GetAllCommentsWithSellerNameConfirmAsync(CancellationToken cancellationToken)
		{
			var comments = await _dbSet.Where(x => x.IsConfirm == null)
				.Include(c => c.Stock)
				.ThenInclude(p => p.Booth)
				.ThenInclude(b => b.Seller)
				.ToListAsync(cancellationToken);
			var result = comments.Select(c => new CommentRepoDto
			{
				Id = c.Id,
				Title = c.Title,
				Description = c.Description,
				Advantages = c.Advantages,
				Buyer = c.Buyer,
				BuyerId = c.BuyerId,
				ConfirmDate = c.ConfirmDate,
				DisAdvantages = c.DisAdvantages,
				IsConfirm = c.IsConfirm,
				IsPositive = c.IsPositive,
				StockId = c.StockId,
				Stock = c.Stock
			}).ToList();

			return result;
		}
		public async Task<int> AddAsync(CommentRepoDto dto, CancellationToken cancellationToken)
		{
			var comment = new Comment
			{
				Id = dto.Id,
				Title = dto.Title,
				Description = dto.Description,
				Advantages = dto.Advantages,
				Buyer = dto.Buyer,
				BuyerId = dto.BuyerId,
				ConfirmDate = dto.ConfirmDate,
				DisAdvantages = dto.DisAdvantages,
				IsConfirm = dto.IsConfirm,
				IsPositive = dto.IsPositive,
				StockId = dto.StockId,
				Stock = dto.Stock
			};
			await _dbSet.AddAsync(comment, cancellationToken);
			var result = await _context.SaveChangesAsync(cancellationToken);
			return result;
		}

		public async Task UpdateAsync(CommentRepoDto comment, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(x => x.Id == comment.Id).FirstOrDefaultAsync(cancellationToken);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(CommentRepoDto comment, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(x => x.Id == comment.Id).FirstOrDefaultAsync(cancellationToken);
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

}
