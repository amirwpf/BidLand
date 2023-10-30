using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer
{
	public interface IImageRepository
	{
		Task<Image> GetProfileImageBySellerId(int sellerId);
		Task<Image> GetBySellerIdAsync(int sellerId);
		Task<Image> GetByIdAsync(int imageId);
		Task<List<ImageRepoDto>> GetAllImageProductBySellerId(int sellerId);
		Task<IEnumerable<Image>> GetImagesForProductAsync(int productId);
		Task<int> AddAsync(ImageRepoDto image);

		Task<bool> RemoveAsync(int id);
		Task<Image> GetByUrlAsync(string url);
		Task<bool> RemoveAsync(string url);

	}
}
