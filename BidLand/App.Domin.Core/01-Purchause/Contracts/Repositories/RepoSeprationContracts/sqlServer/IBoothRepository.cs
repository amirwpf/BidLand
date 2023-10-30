using App.Domin.Core._01_Purchause.Contracts.Repositories.Dtos;
using App.Domin.Core._01_Purchause.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._01_Purchause.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IBoothRepository
	{
		Task<List<Booth>> GetByCategoryIdAsync(int categoryId);
		Task<Booth> GetByIdAsync(int id);
		Task<List<BoothRepoDto>> GetAllAsync();
		Task AddAsync(BoothRepoDto booth);
		Task<bool> UpdateBoothAsync(BoothRepoDto boothRepDto);
		Task<bool> DeleteAsync(int id);
	}
}
