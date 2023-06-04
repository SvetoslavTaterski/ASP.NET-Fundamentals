using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models.Products;

namespace ShoppingListApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly ShoppingListAppDbContext _data;

		public ProductController(ShoppingListAppDbContext data)
			=> _data = data;

		public async Task<IActionResult> All()
		{
			var products = await _data.Products
				.Select(p => new ProductViewModel()
				{
					Id = p.Id,
					Name = p.Name,
				})
				.ToListAsync();

			return View(products);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
			=> View();

		[HttpPost]
		public async Task<IActionResult> Add(ProductViewModel model)
		{
			var product = new Product()
			{
				Id = model.Id,
				Name = model.Name
			};

			_data.Products.AddAsync(product);
			_data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var product = await _data.Products.FindAsync(id);

			return View(new ProductViewModel()
			{
				Name = product.Name
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Product model)
		{
			var product= await _data.Products.FindAsync(id);

			product.Name=model.Name;

			_data.SaveChangesAsync();

			return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _data.Products.FindAsync(id);

			_data.Products.Remove(product);

			await _data.SaveChangesAsync();

			return RedirectToAction("All");
		}
	}
}
