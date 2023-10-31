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

namespace App.Infra.DataAccess.Repos.Ef._03_Extras
{
	public class MedalRepository : IMedalRepository
	{
		private readonly AppDbContext _context;
		private readonly DbSet<Medal> _dbSet;

		public MedalRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<Medal>();
		}

		public async Task<MedalRepoDto> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(x => x.Id == id)
				.Select(x => new MedalRepoDto
				{
					Id = x.Id,
					InsertionDate = x.InsertionDate,
					LevelType = x.LevelType,
					SellerId = x.SellerId,
					Seller = x.Seller
				}).FirstOrDefaultAsync(cancellationToken);
			return result;
		}

		public async Task<MedalRepoDto> GetMedalByTypeAsync(MedalEnum medalType, CancellationToken cancellationToken)
		{
			var result = await _dbSet.Where(m => m.LevelType == medalType)
				.Select(x => new MedalRepoDto
				{
					Id = x.Id,
					InsertionDate = x.InsertionDate,
					LevelType = x.LevelType,
					SellerId = x.SellerId,
					Seller = x.Seller
				}).FirstOrDefaultAsync(cancellationToken);
			return result;
		}


		public async Task<List<MedalRepoDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var result = await _dbSet
				.Select(x => new MedalRepoDto
				{
					Id = x.Id,
					InsertionDate = x.InsertionDate,
					LevelType = x.LevelType,
					SellerId = x.SellerId,
					Seller = x.Seller
				}).ToListAsync(cancellationToken);
			return result;
		}

		public async Task AddAsync(MedalRepoDto medal, CancellationToken cancellationToken)
		{
			var result =await _dbSet.FirstOrDefaultAsync(x=>x.Id== medal.Id,cancellationToken);
			await _dbSet.AddAsync(result);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateAsync(MedalRepoDto medal, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == medal.Id, cancellationToken);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task DeleteAsync(MedalRepoDto medal, CancellationToken cancellationToken)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == medal.Id, cancellationToken);
			_dbSet.Remove(result);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
