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

namespace App.Infra.DataAccess.Repos.Ef._01_Purchase
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Product> _dbSet;

		public ProductRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Product>();
		}

		public async Task<List<ProductRepoDto>> GetAllConfirmProductsWithNavAsync(bool IsConfirm , CancellationToken cancellationToken)
		{
			var products = await _dbSet
				.Include(p=>p.Stocks)
				.Include(p=>p.Images)
				.Include(p=>p.Category)
				.Where(p => p.IsConfirm == IsConfirm)
                .Select(product => new ProductRepoDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    BasePrice = product.BasePrice,
                    Images = product.Images,
                    Stocks = product.Stocks,
                    IsConfirm = product.IsConfirm,
                    IsActive = product.IsActive,
                    IsDelete = product.IsDelete,
                    Description = product.Description,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    InsertionDate = product.InsertionDate
                })
                .ToListAsync(cancellationToken);
            return products;
		}
		public async Task<List<ProductRepoDto>> GetAllProductsWithNavAsync(CancellationToken cancellationToken)
		{
			var products =  _dbSet
				.Include(p => p.Stocks)
				.Include(p => p.Images)
				.Include(p => p.Category)
				.Select(product => new ProductRepoDto()
				{
					Id = product.Id,
					Name = product.Name,
					BasePrice = product.BasePrice,
					Images = product.Images,
					Stocks = product.Stocks,
					IsConfirm = product.IsConfirm,
					IsActive = product.IsActive,
					IsDelete = product.IsDelete,
					Description = product.Description,
					Category = product.Category,
					CategoryId = product.CategoryId,
					InsertionDate = product.InsertionDate
				})
				.ToList();
			//var p = ConvertToProductRepoDto(products1.First());
			//var products = _dbSet
			//	.Include(p => p.Stocks)
			//	.Include(p => p.Images)
			//	.Include(p => p.Category)
			//	.Select(p => ConvertToProductRepoDto(p)).ToList();
			return products;
		}

		public async Task AddAsync(ProductRepoDto productDto, CancellationToken cancellationToken)
		{
			productDto.Category = null;
			var product = ConvertToProduct(productDto);
			await _dbSet.AddAsync(product);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<bool> UpdateAsync(ProductRepoDto productDto, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x=>x.Id == productDto.Id,cancellationToken);

			if (result != null)
			{
				UpdateValueEqualer(productDto, ref result);
				_context.Entry(result).State = EntityState.Modified;
				await _context.SaveChangesAsync(cancellationToken);
				return true;
			}
			return false;
		}



		public async Task<bool> SoftDeleteAsync(int id, CancellationToken cancellationToken)
		{
			var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			if (product != null)
			{
				product.IsDelete = true;
				await _context.SaveChangesAsync(cancellationToken);
				return true;
			}
			return false;
		}
		public async Task<bool> SoftRecoverAsync(int id, CancellationToken cancellationToken)
		{
			var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			if (product != null)
			{
				product.IsDelete = false;
				await _context.SaveChangesAsync(cancellationToken);
				return true;
			}
			return false;
		}
		public async Task<bool> HardDeleteAsync(int id, CancellationToken cancellationToken)
		{
			var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			if (product != null)
			{
				_dbSet.Remove(product);
				await _context.SaveChangesAsync(cancellationToken);
				return true;
			}
			return false;
		}

		public async Task<ProductRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var result = await _dbSet
				.Include(c => c.Category)
				.Select(product => new ProductRepoDto() {
                    Id = product.Id,
                    Name = product.Name,
                    BasePrice = product.BasePrice,
                    Images = product.Images,
                    Stocks = product.Stocks,
                    IsConfirm = product.IsConfirm,
                    IsActive = product.IsActive,
                    IsDelete = product.IsDelete,
                    Description = product.Description,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    InsertionDate = product.InsertionDate
                } )
				.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
			if (result == null) return null;
			return  result;
		}




		#region other
		//public async Task UpdateAsync(ProductRepoDto productDto, List<int> categoryIds, CancellationToken cancellationToken)
		//{
		//	var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == productDto.Id, cancellationToken);

		//	if (result != null)
		//	{
		//		result.Name = productDto.Name;
		//		result.BasePrice = productDto.BasePrice;
		//		result.Description = productDto.Description;
		//		result.IsActive = productDto.IsActive;
		//		result.IsDelete = productDto.IsDelete;
		//		result.IsConfirm = productDto.IsConfirm;

		//		// به روزرسانی دسته‌بندی‌ها
		//		result.Categories = await _context.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync();

		//		_context.Entry(result).State = EntityState.Modified;
		//		await _context.SaveChangesAsync(cancellationToken);
		//	}
		//}
		//public async Task<List<ProductCustomerDto>> GetProductByBoothIdAsync(int boothId)//IsConfirm==true
		//{
		//	var dto = await _dbSet
		//		.Where(x => x.Booth.Id == boothId && x.IsAuction == false && x.IsConfirm == true)
		//		.Select(p => new ProductCustomerDto
		//		{
		//			BoothId = p.Booth.Id,
		//			Name = p.Name,
		//			Description = p.Description,
		//			BasePrice = p.BasePrice,
		//			Availability = p.Availability,
		//			Id = p.Id,
		//			ImagesUrls = p.Images.Select(u => u.Url).ToList(),
		//			Categories = p.Categories.Select(n => n.Name).ToList(),
		//			BoothName = p.Booth.Name,
		//			IsActive = p.IsActive
		//		}).ToListAsync();
		//	return dto;
		//}

		//public async Task<List<AuctionProductDto>> GetProductsWithTrueAuctions(int sellerId)
		//{
		//	var result = await _dbSet.
		//		Where(a =>
		//			a.IsAuction == true &&
		//			a.IsActive == true &&
		//			a.Auction.EndDeadTime >= DateTime.Now &&
		//			a.Booth.Seller.Id == sellerId)
		//		.Select(p => new AuctionProductDto
		//		{
		//			ProductId = p.Id,
		//			ProductName = p.Name,
		//			BasePrice = p.BasePrice ?? 0,
		//			AuctionId = p.Auction.Id,
		//			StartDeadTime = p.Auction.StartDeadTime,
		//			EndDeadTime = p.Auction.EndDeadTime,
		//			HighestPrice = p.Auction.HighestPrice
		//		}).ToListAsync();

		//	return result;
		//}


		//public async Task<ProductDto> GetWithAllNavigationsByIdSellerAsync(int id)
		//{
		//	var product = await _dbSet.AsNoTracking().Where(x => x.Id == id)
		//			.Include(b => b.Auction)
		//			.Include(i => i.Images)
		//			.Include(c => c.Categories)
		//			.FirstOrDefaultAsync();
		//	return new ProductDto
		//	{
		//		Id = product.Id,
		//		Name = product.Name,
		//		BasePrice = product.BasePrice,
		//		IsAuction = product.IsAuction,
		//		Availability = product.Availability,
		//		Description = product.Description,
		//		Auction = product.Auction,
		//		Categories = product.Categories,
		//		Image = product.Images
		//	};
		//}
		//public async Task<List<Product>> GetProductsWithSellerNameConfirmAsync()
		//{
		//	var products = await _dbSet.AsNoTracking().Where(x => x.IsConfirm == null)
		//		.Include(p => p.Booth)
		//		.ThenInclude(b => b.Seller).ToListAsync();
		//	return products;
		//}
		//public async Task<List<Product>> GetAllAsync()
		//{
		//	return await _dbSet.AsNoTracking().ToListAsync();
		//}

		//public async Task<List<ProductDto>> GetAllWithNavigationsAsync(int? boothId)
		//{
		//	var products = await _dbSet.AsNoTracking().Where(x => x.BoothId == boothId)
		//		.Include(b => b.Auction)
		//		.Include(i => i.Images)
		//		.Include(c => c.Categories).ToListAsync();

		//	var result = products.Select(p => new ProductDto
		//	{

		//		Id = p.Id,
		//		Name = p.Name,
		//		BasePrice = p.BasePrice,
		//		IsAuction = p.IsAuction,
		//		IsConfirm = p.IsConfirm,
		//		Availability = p.Availability,
		//		Description = p.Description,
		//		IsActive = p.IsActive,
		//		Auction = p.Auction,
		//		Image = p.Images,
		//		Categories = p.Categories

		//	}).ToList();
		//	return result;
		//}


		//public async Task<string> RemoveFromCartByProductId(int productId, int customerId)
		//{

		//	// دریافت سبد خرید مشتری
		//	var customerCart = await _context.Carts
		//		.Include(c => c.ProductsCarts)
		//		.FirstOrDefaultAsync(c => c.CustomerId == customerId && !c.IsRegistrationFinalized);

		//	if (customerCart == null)
		//		return "سبد خرید مشتری یافت نشد";


		//	// یافتن کالای مورد نظر
		//	var product = await _context.Products
		//		.FirstOrDefaultAsync(p => p.Id == productId && !p.IsRemove);

		//	if (product == null)
		//		return "کالای مورد نظر یافت نشد";


		//	if (product.IsAuction)
		//		return " کالا در مزایده است . برای حذف این کالا با پشتیبانی تماس بگیرید";

		//	// یافتن مورد کالا در سبد خرید مشتری
		//	var cartItem = customerCart.ProductsCarts
		//		.FirstOrDefault(pc => pc.ProductId == productId);

		//	if (cartItem == null)
		//		return "کالای مورد نظر در سبد خرید یافت نشد";

		//	var isAuctionProductAddedByCustomer = customerCart.ProductsCarts
		//		.Any(pc => pc.Product?.Auction?.Bids?.FirstOrDefault(b => b != null && b.CustomerId == customerId && b.IsAccepted == true) != null);


		//	if (isAuctionProductAddedByCustomer)
		//	{
		//		return "شما مجاز به حذف سبد مزایده‌ای که پیشنهاد آن توسط مزایده پذیرفته شده است نمی‌باشید";
		//	}



		//	if (cartItem.Quantity > 1)
		//	{
		//		cartItem.Quantity--;
		//		product.Availability++;
		//	}


		//	else
		//	{
		//		_context.ProductsCarts.Remove(cartItem);
		//		product.Availability++;
		//	}


		//	await _context.SaveChangesAsync();
		//	return "عملیات با موفقیت انجام شد";
		//}

		#endregion



		private ProductRepoDto ConvertToProductRepoDto(Product product)
		{
			var result = new ProductRepoDto()
			{
				Id = product.Id,
				Name = product.Name,
				BasePrice = product.BasePrice,
				Images = product.Images,
				Stocks = product.Stocks,
				IsConfirm = product.IsConfirm,
				IsActive = product.IsActive,
				IsDelete = product.IsDelete,
				Description = product.Description,
				Category = product.Category,
				CategoryId = product.CategoryId,
				InsertionDate = product.InsertionDate
			};

			return result;
		}

		private Product ConvertToProduct(ProductRepoDto productRepoDto)
		{
			var result = new Product()
			{
				Id = productRepoDto.Id,
				Name = productRepoDto.Name,
				BasePrice = productRepoDto.BasePrice,
				Images = productRepoDto.Images,
				Stocks = productRepoDto.Stocks,
				IsConfirm = productRepoDto.IsConfirm,
				IsActive = productRepoDto.IsActive,
				IsDelete = productRepoDto.IsDelete,
				Description = productRepoDto.Description,
				Category = productRepoDto.Category,
				CategoryId = productRepoDto.CategoryId,
				InsertionDate = productRepoDto.InsertionDate
			};

			return result;
		}

		private void UpdateValueEqualer(ProductRepoDto productDto, ref Product result)
		{
			result.Name = productDto.Name;
			result.BasePrice = productDto.BasePrice;
			result.Description = productDto.Description;
			result.IsActive = productDto.IsActive;
			result.IsDelete = productDto.IsDelete;
			result.IsConfirm = productDto.IsConfirm;
			result.Stocks = productDto.Stocks;
			result.Images = productDto.Images;
			result.Category = productDto.Category;
			result.CategoryId = productDto.CategoryId;
			result.InsertionDate = productDto.InsertionDate;
		}

        public async Task<bool> ConfirmProductAsync(int productId, bool confirm, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

            if (result != null)
            {
				result.IsConfirm = confirm;
                _context.Entry(result).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }
}
