using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._02_Users;

public class SellerRepository : ISellerRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Seller> _dbSet;

	public SellerRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Seller>();
	}

	public async Task<SellerRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Include(x=>x.User)
			  .Include(b => b.Booth)
              .Select(seller => new SellerRepoDto()
              {
                  Id = seller.Id,
                  User = seller.User,
                  CommissionPercentage = seller.CommissionPercentage,
                  CommissionsAmount = seller.CommissionsAmount,
                  InsertionDate = seller.InsertionDate,
                  IsActive = seller.IsActive,
                  IsBan = seller.IsBan,
                  IsDelete = seller.IsDelete,
                  SalesAmount = seller.SalesAmount,
                  UserId = seller.UserId
              })
			  .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		
		return result;
	}
public async Task<List<SellerRepoDto>> GetAllDeletedAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
            .Where(x => x.IsDelete)
              .Include(b => b.User)
			  .Include(b => b.Booth)
			  .Select(seller => new SellerRepoDto() {
                  Id = seller.Id,
                  User = seller.User,
                  CommissionPercentage = seller.CommissionPercentage,
                  CommissionsAmount = seller.CommissionsAmount,
                  InsertionDate = seller.InsertionDate,
                  IsActive = seller.IsActive,
                  IsBan = seller.IsBan,
                  IsDelete = seller.IsDelete,
                  SalesAmount = seller.SalesAmount,
                  UserId = seller.UserId
              })
			  .ToListAsync(cancellationToken);
		return result;
	}
	public async Task<List<SellerRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
            //.Where(x => !x.IsDelete)
              .Include(b => b.User)
			  .Include(b => b.Booth)
			  .Select(seller => new SellerRepoDto() {
                  Id = seller.Id,
                  User = seller.User,
                  CommissionPercentage = seller.CommissionPercentage,
                  CommissionsAmount = seller.CommissionsAmount,
                  InsertionDate = seller.InsertionDate,
                  IsActive = seller.IsActive,
                  IsBan = seller.IsBan,
                  IsDelete = seller.IsDelete,
                  SalesAmount = seller.SalesAmount,
                  UserId = seller.UserId
              })
			  .ToListAsync(cancellationToken);
		return result;
	}
	public async Task<int?> GetSumSellersCommisionAmount(CancellationToken cancellationToken)
	{
		var result = await _dbSet.Select(x => new
		{
			x.CommissionsAmount
		}).ToListAsync(cancellationToken);
		return result.Sum(x => x.CommissionsAmount);
	}
	public async Task AddAsync(SellerRepoDto sellerDto, CancellationToken cancellationToken)
	{
		var seller = new Seller();
		Equaler(sellerDto, ref seller);
		await _dbSet.AddAsync(seller, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(SellerRepoDto sellerDto, CancellationToken cancellationToken)
	{
		var seller = await _dbSet.FirstOrDefaultAsync(x => x.Id == sellerDto.Id, cancellationToken);
		if (seller != null)
		{
			Equaler(sellerDto, ref seller);
			_context.Entry(seller).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}
	public async Task<bool> UpdateProfileAsync(SellerRepoDto updatesellerDto, CancellationToken cancellationToken)
	{
		var seller = await _dbSet.Include(x => x.Addresses)
			.Include(b => b.Booth)
			.FirstOrDefaultAsync(x => x.Id == updatesellerDto.Id, cancellationToken);

		if (seller == null)
		{
			return false;
		}

		//seller.FullName = updatesellerDto.FullName;

		if (seller.Addresses == null)
		{
			var address = new Address();

			seller.Addresses.Add(address);
		}
		else
		{
			// Update the existing address
			seller.Addresses = updatesellerDto.Addresses;

		}
		if (seller.Booth == null)
		{
			var booth = new Booth()
			{
				Name = updatesellerDto.User.GetFullName(),
				SellerId = updatesellerDto.Id
			};

			seller.Booth = booth;
		}
		else
		{
			seller.Booth.Name = updatesellerDto.User.GetFullName();
		}


		_context.Entry(seller).State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> SoftDeleteAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken)
	{
		var seller = await _dbSet.FirstOrDefaultAsync(s => s.Id == sellerRepo.Id);
		if (seller != null)
		{
			seller.IsDelete = true;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> SoftRecoverAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken)
	{
		var seller = await _dbSet.FirstOrDefaultAsync(s => s.Id == sellerRepo.Id);
		if (seller != null)
		{
			seller.IsDelete = false;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(SellerRepoDto sellerRepo, CancellationToken cancellationToken)
	{
		var seller = await _dbSet.FirstOrDefaultAsync(s => s.Id == sellerRepo.Id);
		if (seller != null)
		{
			_dbSet.Remove(seller);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	//public async Task<FinancialRepSeller> GetFinancialBySellerId(int sellerId)
	//{
	//	var seller = await _dbSet.FirstOrDefaultAsync(x => x.Id == sellerId);
	//	if (seller == null)
	//		return null;

	//	var result = new FinancialRepSeller()
	//	{
	//		Id = sellerId,
	//		SalesAmount = seller.SalesAmount,
	//		CommissionPercentage = seller.CommissionPercentage,
	//		CommissionsAmount = seller.CommissionsAmount,
	//		Medals = seller.Medals
	//	};
	//	return result;
	//}
	//public async Task<AddSellerDto> GetByIdWithNavigationAsync(int id)
	//{
	//	var result = await _dbSet.AsNoTracking().Select(s => new AddSellerDto
	//	{
	//		SellerId = s.Id,
	//		CompanyName = s.CompanyName,
	//		City = s.Address.City,
	//		Street = s.Address.Street,
	//		AddressDescription = s.Address.Description,
	//		BoothName = s.Booth.Name,
	//		BoothDescription = s.Booth.Description,
	//		BoothId = s.Booth.Id,
	//		AddressId = s.Address.Id,
	//	})
	//		   .FirstOrDefaultAsync(x => x.SellerId == id);
	//	return result;
	//}

	private SellerRepoDto ConvertToSellerRepoDto(Seller seller)
	{
		return new SellerRepoDto()
		{
			Id = seller.Id,
			//FullName = seller.FullName,
			CommissionPercentage = seller.CommissionPercentage,
			CommissionsAmount = seller.CommissionsAmount,
			InsertionDate = seller.InsertionDate,
			IsActive = seller.IsActive,
			IsBan = seller.IsBan,
			IsDelete = seller.IsDelete,
			SalesAmount = seller.SalesAmount,
			UserId = seller.UserId
		};
	}

	private void Equaler(SellerRepoDto sellerRepoDto, ref Seller seller)
	{
		seller.Id = sellerRepoDto.Id;
		seller.CommissionPercentage = sellerRepoDto.CommissionPercentage;
		seller.CommissionsAmount = sellerRepoDto.CommissionsAmount;
		seller.InsertionDate = sellerRepoDto.InsertionDate;
		seller.IsActive = sellerRepoDto.IsActive;
		seller.IsBan = sellerRepoDto.IsBan;
		seller.IsDelete = sellerRepoDto.IsDelete;
		seller.SalesAmount = sellerRepoDto.SalesAmount;
		seller.UserId = sellerRepoDto.UserId;
	}
}
