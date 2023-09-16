using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Entities;
using SoftUniBazar.Models.Ads;
using SoftUniBazar.Models.Categories;

namespace SoftUniBazar.Services
{
	public class AdService : IAdService
	{
		private readonly BazarDbContext _dbContext;

		public AdService(BazarDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAdToCartAsync(string userId, AllAdsViewModel ad)
		{
			bool isAdAlreadyAdded = await _dbContext.AdBuyers.AnyAsync(a => a.AdId == ad.Id && a.BuyerId == userId);

			if (!isAdAlreadyAdded)
			{
				var userAds = new AdBuyer
				{
					BuyerId = userId,
					AdId = ad.Id,
				};

				await _dbContext.AdBuyers.AddAsync(userAds);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task AddNewAd(AddAdsViewModel model,string userId)
		{
			Ad ad = new Ad
			{
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				OwnerId = userId,
				CreatedOn = DateTime.Now,
				ImageUrl = model.ImageUrl,
				CategoryId = model.CategoryId
			};

			await _dbContext.Ads.AddAsync(ad);
			await _dbContext.SaveChangesAsync();
		}

		public async Task EditAdAsync(int id, AddAdsViewModel model)
		{
			var ad = _dbContext.Ads.FirstOrDefault(a => a.Id == id);


			ad.CategoryId = model.CategoryId;
			ad.Description = model.Description;
			ad.Price = model.Price;
			ad.ImageUrl = model.ImageUrl;
			ad.Name = model.Name;

			await _dbContext.SaveChangesAsync();
		}

		public async Task<AdBuyer?> GetAdBuyerByIdAsync(int id)
		{
			return await _dbContext
				.AdBuyers
				.Where(a => a.AdId == id)
				.Select(x => new AdBuyer
				{
					BuyerId = x.BuyerId,
					Buyer = x.Buyer,
					AdId = x.AdId,
					Ad = x.Ad
				}).FirstOrDefaultAsync();
		}

		public async Task<AllAdsViewModel?> GetAdByIdAsync(int id)
		{
			return await _dbContext
				.Ads
				.Where(b => b.Id == id)
				.Select(b => new AllAdsViewModel
				{
					Id = b.Id,
					Name = b.Name,
					Description = b.Description,
					Owner = b.Owner.UserName,
					ImageUrl = b.ImageUrl,
					Price = b.Price,
					Category = b.Category.Name,
					CreatedOn = b.Category.ToString()
				})
				.FirstOrDefaultAsync();
		}

		public async Task<AddAdsViewModel> GetAdForEditAsync(string userId, int id)
		{
			var ad = await _dbContext.Ads.FindAsync(id);

			var categories = await _dbContext
				.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name,
				}).ToListAsync();

			return new AddAdsViewModel
			{
				Name = ad.Name,
				Description = ad.Description,
				ImageUrl = ad.ImageUrl,
				CreatedOn = ad.CreatedOn.ToString(),
				OwnerId = ad.OwnerId,
				Price = ad.Price,
				CategoryId = ad.CategoryId,
				Categories = categories
			};
		}

		public async Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync()
		{
			return await _dbContext
				.Ads
				.Select(x => new AllAdsViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					ImageUrl = x.ImageUrl,
					Owner = x.Owner.UserName,
					Price = x.Price,
					Category = x.Category.Name,
					CreatedOn = x.CreatedOn.ToString("yyyy-MM-dd H:mm")
				})
				.ToArrayAsync();
		}

		public async Task<IEnumerable<AllAdsViewModel>> GetAllAdsCartAsync(string userId)
		{
			return await _dbContext
				.AdBuyers
				.Where(x => x.BuyerId == userId)
				.Select(x => new AllAdsViewModel
				{
					Id = x.Ad.Id,
					Name = x.Ad.Name,
					Description = x.Ad.Description,
					Owner = x.Ad.Owner.UserName,
					ImageUrl = x.Ad.ImageUrl,
					Price = x.Ad.Price,
					Category = x.Ad.Category.Name,
					CreatedOn = x.Ad.CreatedOn.ToString("yyyy-MM-dd H:mm")
				})
				.ToListAsync();
		}

		public async Task<AddAdsViewModel> GetNewAddAdViewModelCategories()
		{
			var categories = await _dbContext
				.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name,
				}).ToListAsync();

			var model = new AddAdsViewModel()
			{
				Categories = categories
			};

			return model;
		}

		public async Task RemoveAdFromCollection(string userId, AdBuyer ad)
		{
			bool isAdExisting = await _dbContext.AdBuyers.AnyAsync(a => a.AdId == ad.AdId && a.BuyerId == userId);

			if (isAdExisting)
			{
				_dbContext.AdBuyers.Remove(ad);
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
