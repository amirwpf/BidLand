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

public class AdminRepository : IAdminRepository
{
	private readonly AppDbContext _context;
	private readonly DbSet<Admin> _dbSet;

	public AdminRepository(AppDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<Admin>();
	}



	public async Task<AdminRepoDto> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await _dbSet.AsNoTracking()
			 .Include(b => b.User)
			  //.Where(x => !(bool)x.IsDelete)
			  .Select(admin => new AdminRepoDto()
			  {
				 Id=admin.Id,
				 SiteCommissionIncome= admin.SiteCommissionIncome,
				 InsertionDate = admin.InsertionDate
			  }).FirstOrDefaultAsync(cancellationToken);
		return result;
	}

	public async Task<bool> UpdateAsync(AdminRepoDto admin, CancellationToken cancellationToken)
	{
		var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == admin.Id, cancellationToken);
		if (result != null)
		{
			Equaler(admin, ref result);
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync(cancellationToken);
			return true;
		}
		return false;
	}

	private void Equaler(AdminRepoDto admin, ref Admin b)
	{
		//b.Id = buyer.Id;
		//b.FullName = buyer.FullName;
		b.SiteCommissionIncome= admin.SiteCommissionIncome;
		b.InsertionDate= admin.InsertionDate;
	}
}
