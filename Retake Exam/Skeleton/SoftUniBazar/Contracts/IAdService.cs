using SoftUniBazar.Data.Entities;
using SoftUniBazar.Models.Ads;

namespace SoftUniBazar.Contracts
{
	public interface IAdService
	{
		public Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync();

		public Task<IEnumerable<AllAdsViewModel>> GetAllAdsCartAsync(string userId);

		public Task AddNewAd(AddAdsViewModel model,string userId);

		public Task<AddAdsViewModel> GetNewAddAdViewModelCategories();

		public Task<AddAdsViewModel> GetAdForEditAsync(string userId,int id);

		public Task EditAdAsync(int id,AddAdsViewModel model);

		public Task AddAdToCartAsync(string userId, AllAdsViewModel ad);

		public Task<AllAdsViewModel?> GetAdByIdAsync(int id);

		public Task RemoveAdFromCollection(string userId, AdBuyer ad);

		public Task<AdBuyer?> GetAdBuyerByIdAsync(int id);
	}
}
