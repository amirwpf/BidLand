using App.Domin.Core._03_Extras.Contracts.Repositories.Dtos;
using App.Domin.Core._03_Extras.Contracts.Repositories.RepoSeprationContracts.sqlServer;
using App.Domin.Core._03_Extras.Entities;
using App.Infra.Db.sqlServer.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.Repos.Ef._03_Extras
{
	public class ImageRepository : IImageRepository
	{
		private readonly AppDbContext _dbContext;
		private readonly DbSet<Image> _imageSet;

		public ImageRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			_imageSet = _dbContext.Set<Image>();
		}

		public async Task<ImageRepoDto> GetByIdAsync(int imageId, CancellationToken cancellationToken)
		{
			var result = await _imageSet.Where(i=>i.Id == imageId)
				.Select(i => new ImageRepoDto
				{
					Id = i.Id,
					Url = i.Url,
					ProductId = i.ProductId,
					Product = i.Product,
					InsertionDate = i.InsertionDate
				}).FirstOrDefaultAsync(cancellationToken);

			return result;
		}
		public async Task<List<ImageRepoDto>> GetAllImageForProductByIdAsync(int productId, CancellationToken cancellationToken)
		{

			var images = await _imageSet
				.Where(i => i.Product.Id == productId)
				.Select(i => new ImageRepoDto
				{
					Id = i.Id,
					Url = i.Url,
					ProductId = i.ProductId,
					Product = i.Product,
					InsertionDate = i.InsertionDate
				}).ToListAsync(cancellationToken);

			return images;
		}

		public async Task<ImageRepoDto> GetByUrlAsync(string url, CancellationToken cancellationToken)
		{
			var result = await _imageSet.Where(i => i.Url== url)
				.Select(i => new ImageRepoDto
				{
					Id = i.Id,
					Url = i.Url,
					ProductId = i.ProductId,
					Product = i.Product,
					InsertionDate = i.InsertionDate
				}).FirstOrDefaultAsync(cancellationToken);

			return result;
		}

		public async Task<int> AddAsync(ImageRepoDto imageDto, CancellationToken cancellationToken)
		{
			var image = new Image
			{
				Url = imageDto.Url,
				ProductId = imageDto.ProductId,
				Product = imageDto.Product,
				Id= imageDto.Id,
				InsertionDate= imageDto.InsertionDate
			};

			await _imageSet.AddAsync(image, cancellationToken);
			var result = await _dbContext.SaveChangesAsync(cancellationToken);
			return result;
		}



		public async Task DeleteAsync(int id, CancellationToken cancellationToken)
		{
			var image = await _imageSet.FindAsync(id, cancellationToken);
			if (image != null)
			{
				_imageSet.Remove(image);
				await _dbContext.SaveChangesAsync(cancellationToken);

			}
		}

		public async Task DeleteAsync(string url, CancellationToken cancellationToken)
		{
			var image = await _imageSet.FirstOrDefaultAsync(image => image.Url == url);
			if (image != null)
			{
				_imageSet.Remove(image);
				await _dbContext.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task UpdateAsync(ImageRepoDto imageRepoDto, CancellationToken cancellationToken)
		{
			var result = await _imageSet.Where(x => x.Id == imageRepoDto.Id).FirstOrDefaultAsync(cancellationToken);
			if (result != null)
			{
				var image = _imageSet.Remove(result);
				await _dbContext.SaveChangesAsync(cancellationToken);
			}
		}

	}

}
