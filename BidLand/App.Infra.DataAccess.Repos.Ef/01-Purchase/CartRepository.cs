using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._01_Purchause.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase;

public class CartRepository : ICartRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Cart> _dbSet;

	public CartRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Cart>();
	}
	public async Task<int> GetTotalPrices(int cartId, CancellationToken cancellationToken)
	{
		var products = await _context.StocksCarts
			.Where(pc => pc.CartId == cartId)
			.Include(pc => pc.Stock)
			.ToListAsync();
		if (products != null)
		{
			var amount = (int)products.Sum(pc => pc.Stock.Price * pc.Quantity);
			return amount;
		}
		return 0;
	}

	public async Task<bool> FinalizeCartAsync(int cartId, CancellationToken cancellationToken)
	{
		var cart = await _dbSet.FindAsync(cartId);
		var seller = await _dbSet
			.Where(c => c.Id == cartId)
			.SelectMany(c => c.StocksCarts)
			.Select(pc => pc.Stock)
			.Select(p => p.Booth)
			.Select(b => b.Seller)
			.FirstOrDefaultAsync();

		if (seller == null)
			return false;
		if (cart == null)
			return false;

		seller.CommissionsAmount += Convert.ToInt32(await GetTotalPrices(cartId, cancellationToken) * seller.CommissionPercentage);
		var amount = await GetTotalPrices(cartId, cancellationToken) - seller.CommissionsAmount;
		seller.SalesAmount += amount;

		cart.PurchaseCompeleted = true;
		cart.PurchaseDate = DateTime.Now;
		_context.Entry(seller).State = EntityState.Modified;
		_context.Entry(cart).State = EntityState.Modified;
		var e = await _context.SaveChangesAsync();
		if (e != 0)
			return true;
		return false;
	}
	public async Task<List<BidResponseDto>> GetNonCompeletedCartsByBuyerId(int buyerId, CancellationToken cancellationToken)
	{
		var list = await _dbSet
			   .Where(c => c.BuyerId == buyerId && (c.PurchaseCompeleted == null || c.PurchaseCompeleted == false))
			   .Select( c => new BidResponseDto
			   {
				   Id = c.Id,
				   BuyerId = buyerId,
				   TotalPrices = GetTotalPrices(c.Id, cancellationToken).Result,
				   PurchaseCompeleted = c.PurchaseCompeleted,
				   ProductsNames = c.StocksCarts.Select(p => p.Stock.Product.Name).ToList(),
				   QuantityFromOne = c.StocksCarts.Select(c => c.Quantity.Value).Sum()
			   })
			   .ToListAsync();

		return list;
	}
	public async Task<List<BidResponseDto>> GetCompeletedCartsByCustomerId(int buyerId, CancellationToken cancellationToken)
	{
		var list = await _dbSet
			.Where(c => c.BuyerId == buyerId && c.PurchaseCompeleted == true)
			.Select(c => new BidResponseDto
			{
				Id = c.Id,
				BuyerId = buyerId,
				TotalPrices = GetTotalPrices(c.Id, cancellationToken).Result,
				PurchaseCompeleted = c.PurchaseCompeleted,
				ProductsNames = c.StocksCarts.Select(p => p.Stock.Product.Name).ToList(),
				QuantityFromOne = c.StocksCarts.Select(c => c.Quantity.Value).Sum()
			})
			   .ToListAsync();

		return list;
	}
	public async Task<CartRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Include(x => x.StocksCarts)
			.ThenInclude(x => x.Stock)
			.ThenInclude(x => x.Product)
			.Include(x => x.StocksCarts)
			.ThenInclude(x => x.Stock)
			.ThenInclude(x => x.Booth)
			.Select(x => new CartRepoDto()
			{
				Id = x.Id,
				StocksCarts = x.StocksCarts,
				Buyer = x.Buyer,
				BuyerId = x.BuyerId,
				InsertionDate = x.InsertionDate,
				PurchaseCompeleted = x.PurchaseCompeleted,
				PurchaseDate = x.PurchaseDate,
				Value = x.Value
			}).FirstOrDefaultAsync(x=>x.Id== id, cancellationToken);
		if(result!=null) return result;
		return null;	
	}

	public async Task<List<CartRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			.Include(x=>x.StocksCarts)
			.ThenInclude(x=>x.Stock)
			.ThenInclude(x=>x.Product)
			.Include(x=>x.StocksCarts)
			.ThenInclude(x=>x.Stock)
			.ThenInclude(x=>x.Booth)
			.Select(x => new CartRepoDto()
			{
				Id = x.Id,
				StocksCarts = x.StocksCarts,
				Buyer = x.Buyer,
				BuyerId = x.BuyerId,
				InsertionDate = x.InsertionDate,
				PurchaseCompeleted = x.PurchaseCompeleted,
				PurchaseDate = x.PurchaseDate,
				Value = x.Value
			}).ToListAsync(cancellationToken);
		return result;
	}

	public async Task AddAsync(CartRepoDto dto, CancellationToken cancellationToken)
	{
		var cart = new Cart();
		Equaler(dto, ref cart);
		await _dbSet.AddAsync(cart);
		var result = await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task UpdateAsync(CartRepoDto cart, CancellationToken cancellationToken)
	{
		//var resualt = await _dbSet.Where(x => x.Id == cart.Id).FirstOrDefaultAsync(cancellationToken);
		//Equaler(cart, ref resualt);
		//_context.Entry(resualt).State = EntityState.Modified;
		//await _context.SaveChangesAsync(cancellationToken);
		var existingEntity = _dbSet.Find(cart.Id);

		if (existingEntity != null)
		{
			Equaler(cart, ref existingEntity);
			_context.Entry(existingEntity).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task<bool> DeleteAsync(CartRepoDto cart,CancellationToken cancellationToken)
	{
		var resualt = await _dbSet.FirstOrDefaultAsync(x => x.Id == cart.Id,cancellationToken);
		if(resualt != null)
		{
			_dbSet.Remove(resualt);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
		
	}
	public async Task<string> DeleteOpenCartAsync(int buyerId, int cartId, CancellationToken cancellationToken)
	{
		// دریافت سبد خرید مشتری
		var buyerCart = await _context.Carts
			.Include(c => c.StocksCarts)
			.ThenInclude(pc => pc.Stock)
			//.ThenInclude(p => p.Auction)
			//.ThenInclude(a => a.Bids)
			.SingleOrDefaultAsync(c => c.BuyerId == buyerId && c.Id == cartId && !c.PurchaseCompeleted, cancellationToken);

		if (buyerCart == null)
		{
			return "سبد خرید مشتری یافت نشد";
		}

		// بررسی وجود کالاهای با IsAuction=true در سبد خرید
		bool hasAuctionProducts = buyerCart.StocksCarts.Any(pc => pc.Stock.IsAuction);

		if (hasAuctionProducts)
		{
			return "در سبد خرید مشتری کالاهایی با قابلیت حراجی وجود دارد. لطفاً برای حذف این سبد خرید با پشتیبانی تماس بگیرید.";
		}

		var isAuctionProductAddedByBuyer = false;
			//= buyerCart.StocksCarts
			//.Any(pc => pc.Stock.FirstOrDefault(b => b != null && b.BuyerId == buyerId && b.HasWon == true) != null);


		if (isAuctionProductAddedByBuyer)
		{
			return "شما مجاز به حذف سبد مزایده‌ای که پیشنهاد آن توسط مزایده پذیرفته شده است نمی‌باشید";
		}

		// حذف کلیه محصولات سبد خرید
		_context.StocksCarts.RemoveRange(buyerCart.StocksCarts);

		// حذف سبد خرید
		_context.Carts.Remove(buyerCart);

		await _context.SaveChangesAsync(cancellationToken);

		return "سبد خرید مشتری با موفقیت حذف شد";
	}

	private CartRepoDto ConvertToCardRepoDto(Cart x)
	{
		return new CartRepoDto()
		{
			Id = x.Id,
			StocksCarts = x.StocksCarts,
			Buyer = x.Buyer,
			BuyerId = x.BuyerId,
			InsertionDate = x.InsertionDate,
			PurchaseCompeleted = x.PurchaseCompeleted,
			PurchaseDate = x.PurchaseDate,
			Value = x.Value
		};
	}

	private void Equaler(CartRepoDto cartRepoDto, ref Cart cart)
	{
		//cart.Id = cartRepoDto.Id;
		cart.StocksCarts = cartRepoDto.StocksCarts;
		cart.Buyer = cartRepoDto.Buyer;
		cart.BuyerId = cartRepoDto.BuyerId;
		cart.InsertionDate = cartRepoDto.InsertionDate;
		cart.PurchaseCompeleted = cartRepoDto.PurchaseCompeleted;
		cart.PurchaseDate = cartRepoDto.PurchaseDate;
		cart.Value = cartRepoDto.Value;
	}

}
