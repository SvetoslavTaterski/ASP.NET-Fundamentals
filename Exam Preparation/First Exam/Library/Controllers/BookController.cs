using System.Security.Claims;
using Library.Contracts;
using Library.Models.Book;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly IBookService _bookService;

		public BookController(IBookService service)
		{
			_bookService = service;
		}

		public async Task<IActionResult> All()
		{
			var books = await _bookService.GetAllBooksAsync();

			return View(books);
		}

		public async Task<IActionResult> Mine()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var books = await _bookService.GetMyBooksAsync(userId);

			return View(books);
		}

		public async Task<IActionResult> AddToCollection(int id)
		{
			var book = await _bookService.GetBookByIdAsync(id);

			if (book == null)
			{
				return RedirectToAction(nameof(All));
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _bookService.AddBookToCollectionAsync(userId, book);

			return RedirectToAction(nameof(All));
		}

		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var book = await _bookService.GetIdentityUserBookByIdAsync(id);

			if (book == null)
			{
				return RedirectToAction(nameof(All));
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			await _bookService.RemoveBookFromCollectionAsync(userId, book);

			return RedirectToAction("Mine", "Book");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddBookViewModel bookViewModel = await _bookService.GetNewAddBookModelCategories();

			return View(bookViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBookViewModel model)
		{
			await _bookService.AddNewBook(model);

			return RedirectToAction("All");
		}
	}
}
