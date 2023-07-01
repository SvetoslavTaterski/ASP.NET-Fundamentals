using Library.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Library.Data.Entities;
using Library.Models;

namespace Library.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		public async Task<IActionResult> All()
		{
			var models = await _bookService.GetBooksAsync();

			return View(models);
		}

		public async Task<IActionResult> AddToCollection(int bookId)
		{
			var book = await _bookService.GetBookByIdAsync(bookId);

			if (book == null)
			{
				return RedirectToAction("All");
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _bookService.AddBookToCollectionAsync(userId, book);

			return RedirectToAction("All");
		}

		public async Task<IActionResult> Mine()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var models = await _bookService.GetAllMyBooksAsync(userId);

			return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			BookViewModel book = await _bookService.GetNewAddBookModelCategories();

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> Add(BookViewModel model)
		{
			await _bookService.AddNewBook(model);

			return RedirectToAction("All");
		}

		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var book = await _bookService.GetIdentityUserBookByIdAsync(id);

			if (book == null)
			{
				return RedirectToAction(nameof(All));
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _bookService.RemoveBookAsync(userId, book);

			return RedirectToAction("Mine", "Book");
		}

	}
}
