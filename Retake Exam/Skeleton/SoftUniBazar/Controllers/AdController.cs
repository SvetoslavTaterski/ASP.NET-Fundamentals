using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Contracts;
using SoftUniBazar.Models.Ads;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
	[Authorize]
	public class AdController : Controller
	{
		private readonly IAdService _adService;

		public AdController(IAdService adService)
		{
			_adService = adService;
		}

		public async Task<IActionResult> All()
		{
			var model = await _adService.GetAllAdsAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddAdsViewModel viewModel = await _adService.GetNewAddAdViewModelCategories();

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddAdsViewModel model)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _adService.AddNewAd(model, userId);

			return RedirectToAction("All", "Ad");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			AddAdsViewModel model = await _adService.GetNewAddAdViewModelCategories();

			model = await _adService.GetAdForEditAsync(userId, id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, AddAdsViewModel model)
		{
			await _adService.EditAdAsync(id, model);

			return RedirectToAction("All","Ad");
		}

		public async Task<IActionResult> AddToCart(int id)
		{
			var ad = await _adService.GetAdByIdAsync(id);

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _adService.AddAdToCartAsync(userId, ad);

			return RedirectToAction("Cart", "Ad");
		}

		[HttpGet]
		public async Task<IActionResult> Cart()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var ads = await _adService.GetAllAdsCartAsync(userId);

			return View(ads);
		}

		public async Task<IActionResult> RemoveFromCart(int id)
		{
			var ad = await _adService.GetAdBuyerByIdAsync(id);

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _adService.RemoveAdFromCollection(userId, ad);

			return RedirectToAction("All", "Ad");
		}
	}
}
