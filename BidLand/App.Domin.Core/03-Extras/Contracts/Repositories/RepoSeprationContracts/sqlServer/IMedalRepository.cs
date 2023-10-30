using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._03_Extras.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IMedalRepository
	{
		Task<Medal> GetByIdAsync(int id);
		Task<List<Medal>> GetAllAsync();
		Task AddAsync(Medal medal);
		Task UpdateAsync(Medal medal);
		Task DeleteAsync(Medal medal);
		Task<Medal> GetMedalByTypeAsync(MedalEnum medalType);
	}
}
