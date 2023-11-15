using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._03_Extras.Enums;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._03_Extras;

public class MedalRepository : IMedalRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Medal> _dbSet;

	public MedalRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Medal>();
	}

	public async Task<MedalRepoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(x => x.Id == id)
			.Select(medal => new MedalRepoDto()
			{
				Id = medal.Id,
				InsertionDate = medal.InsertionDate,
				LevelType = medal.LevelType,
				Percentage = medal.Percentage,
				SellerId = medal.SellerId,
				Seller = medal.Seller
			}).FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;
		return result;
	}

	public async Task<MedalRepoDto?> GetMedalByTypeAsync(MedalEnum medalType, CancellationToken cancellationToken)
	{
		var result = await _dbSet.Where(m => m.LevelType == medalType)
			.Select(medal => new MedalRepoDto()
			{
				Id = medal.Id,
				InsertionDate = medal.InsertionDate,
				LevelType = medal.LevelType,
				Percentage = medal.Percentage,
				SellerId = medal.SellerId,
				Seller = medal.Seller
			}).FirstOrDefaultAsync(cancellationToken);
		if (result == null) return null;
		return result;
	}


	public async Task<List<MedalRepoDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet
			.Select(medal => new MedalRepoDto()
			{
				Id = medal.Id,
				InsertionDate = medal.InsertionDate,
				LevelType = medal.LevelType,
				Percentage = medal.Percentage,
				SellerId = medal.SellerId,
				Seller = medal.Seller
			}).ToListAsync(cancellationToken);
		return result;
	}

	public async Task AddAsync(MedalRepoDto medal, CancellationToken cancellationToken)
	{
		var result = new Medal();
		Equaler(medal, ref result);
		await _dbSet.AddAsync(result);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> UpdateAsync(MedalRepoDto medal, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == medal.Id, cancellationToken);
		if (result != null)
		{
			Equaler(medal, ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	public async Task<bool> HardDeleteAsync(MedalRepoDto medal, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == medal.Id, cancellationToken);
		if (result != null)
		{
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private MedalRepoDto ConvertToDto(Medal medal)
	{
		return new MedalRepoDto()
		{
			Id = medal.Id,
			InsertionDate = medal.InsertionDate,
			LevelType = medal.LevelType,
			Percentage= medal.Percentage,
			SellerId = medal.SellerId,
			Seller = medal.Seller
		};
	}

	private void Equaler(MedalRepoDto medalDto, ref Medal medal)
	{
		medal.Id = medalDto.Id;
		medal.InsertionDate = medalDto.InsertionDate;
		medal.LevelType = medalDto.LevelType;
		medal.Percentage = medalDto.Percentage;
		medal.SellerId = medalDto.SellerId;
		medal.Seller = medalDto.Seller;
	}
}
