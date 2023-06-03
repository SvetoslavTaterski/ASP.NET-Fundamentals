using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Data;
using ShoppingListApp.Models.Product;

namespace ShoppingListApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly ShoppingListAppDbContext _data;

		public ProductController(ShoppingListAppDbContext data)
			=> _data = data;

		public IActionResult All()
		{
			var products = _data
				
			return View(products);
		}
	}
}
