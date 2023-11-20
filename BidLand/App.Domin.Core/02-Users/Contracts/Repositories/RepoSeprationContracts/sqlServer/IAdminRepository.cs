﻿using App.Domin.Core._02_Users.Contracts.Repositories.Dtos;
using App.Domin.Core._02_Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._02_Users.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IAdminRepository
	{
		Task<AdminRepoDto> GetAllAsync(CancellationToken cancellationToken);
		Task<bool> UpdateAsync(AdminRepoDto admin, CancellationToken cancellationToken);
	}
}
