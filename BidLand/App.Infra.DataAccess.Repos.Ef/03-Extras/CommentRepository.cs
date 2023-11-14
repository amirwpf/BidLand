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

namespace App.Infra.DataAccess.Repos.Ef._03_Extras;

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
			.Select(c => ConvertToDto(c))
			.ToListAsync(cancellationToken);
		return comments;
	}

	public async Task<List<CommentRepoDto>> GetByStockIdAsync(int stockId, CancellationToken cancellationToken)
	{
		var comments = await _dbSet.AsNoTracking()
			.Where(c => c.StockId == stockId /*&& c.IsConfirm==true*/)
			.Include(p => p.Stock)
			.Include(c => c.Buyer)
			.Select(c => ConvertToDto(c))
			.ToListAsync(cancellationToken);
		return comments;
	}

	public async Task<List<CommentRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var comments = await _dbSet.AsNoTracking()
			.Include(p => p.Stock)
			.Include(c => c.Buyer)
			.Select(c => ConvertToDto(c))
			.ToListAsync(cancellationToken);
		return comments;
	}
	public async Task<List<CommentRepoDto>> GetAllCommentsWithSellerNameConfirmAsync(CancellationToken cancellationToken)
	{
		var comments = await _dbSet
			.Include(p => p.Stock)
			.Include(p => p.Stock.Product)
			.Include(c=>c.Stock.Booth)
			.Include(c => c.Buyer)
			.ThenInclude(c=>c.User)
			.Select(comment => new CommentRepoDto() {
                Id = comment.Id,
                Title = comment.Title,
                Description = comment.Description,
                Advantages = comment.Advantages,
                Buyer = comment.Buyer,
                BuyerId = comment.BuyerId,
                ConfirmDate = comment.ConfirmDate,
                DisAdvantages = comment.DisAdvantages,
                IsConfirm = comment.IsConfirm,
                IsPositive = comment.IsPositive,
                StockId = comment.StockId,
                Stock = comment.Stock
            })
			.ToListAsync(cancellationToken);
		return comments;
	}
	public async Task<int> AddAsync(CommentRepoDto dto, CancellationToken cancellationToken)
	{
		var comment = new Comment();
		Equaler(dto, ref comment);
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

	public async Task<bool> HardDeleteAsync(CommentRepoDto comment, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == comment.Id,cancellationToken);
		if(result != null)
		{
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private CommentRepoDto ConvertToDto(Comment comment)
	{
		return new CommentRepoDto()
		{
			Id = comment.Id,
			Title = comment.Title,
			Description = comment.Description,
			Advantages = comment.Advantages,
			Buyer = comment.Buyer,
			BuyerId = comment.BuyerId,
			ConfirmDate = comment.ConfirmDate,
			DisAdvantages = comment.DisAdvantages,
			IsConfirm = comment.IsConfirm,
			IsPositive = comment.IsPositive,
			StockId = comment.StockId,
			Stock = comment.Stock
		};
	}

	private void Equaler(CommentRepoDto commentDto, ref Comment comment)
	{
		comment.Id = comment.Id;
		comment.Title = comment.Title;
		comment.Description = comment.Description;
		comment.Advantages = comment.Advantages;
		comment.Buyer = comment.Buyer;
		comment.BuyerId = comment.BuyerId;
		comment.ConfirmDate = comment.ConfirmDate;
		comment.DisAdvantages = comment.DisAdvantages;
		comment.IsConfirm = comment.IsConfirm;
		comment.IsPositive = comment.IsPositive;
		comment.StockId = comment.StockId;
		comment.Stock = comment.Stock;
	}

    public async Task<bool> ConfirmCommentByIdAsync(int commentId, bool isConfirm, CancellationToken cancellationToken)
    {
        var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken);
        if (result != null)
        {
			result.IsConfirm = isConfirm;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}
