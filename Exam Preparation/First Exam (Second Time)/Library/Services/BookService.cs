using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Library.Data.Entities;
using Book = Library.Data.Entities.Book;

namespace Library.Services
{
	[Authorize]
	public class BookService : IBookService
	{
		private readonly LibraryDbContext _data;

		public BookService(LibraryDbContext data)
		{
			_data = data;
		}

		public async Task AddBookToCollectionAsync(string userId, BookViewModel model)
		{
			bool alreadyAdded = await _data.IdentityUserBooks
				.AnyAsync(ub => ub.CollectorId == userId && ub.BookId == model.Id);

			if (alreadyAdded == false)
			{
				IdentityUserBook book = new IdentityUserBook()
				{
					CollectorId = userId,
					BookId = model.Id
				};

				await _data.IdentityUserBooks.AddAsync(book);
				await _data.SaveChangesAsync();
			}

		}

		public async Task AddNewBook(BookViewModel model)
		{
			Book book = new Book()
			{
				Author = model.Author,
				CategoryId = model.CategoryId,
				Title = model.Title,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Rating = model.Rating,
			};

			await _data.Books.AddAsync(book);
			await _data.SaveChangesAsync();
		}

		public async Task<IEnumerable<BookViewModel>> GetAllMyBooksAsync(string userId)
		{
			var entities = await _data.IdentityUserBooks
				.Where(u => u.CollectorId == userId)
				.Select(b => new BookViewModel()
				{
					Author = b.Book.Author,
					Category = b.Book.Category.Name,
					Title = b.Book.Title,
					Id = b.Book.Id,
					ImageUrl = b.Book.ImageUrl,
					Description = b.Book.Description
				}).ToListAsync();

			return entities;
		}

		public async Task<BookViewModel?> GetBookByIdAsync(int id)
		{

			return await _data.Books
				.Where(b => b.Id == id)
				.Select(b => new BookViewModel
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ImageUrl = b.ImageUrl,
					Description = b.Description,
					Rating = b.Rating,
					CategoryId = b.CategoryId
				}).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<BookViewModel>> GetBooksAsync()
		{
			var entities = await _data.Books.Select(b => new BookViewModel()
			{
				Title = b.Title,
				Author = b.Author,
				Rating = b.Rating,
				CategoryId = b.CategoryId,
				ImageUrl = b.ImageUrl,
				Category = b.Category.Name
			}).ToArrayAsync();

			return entities;
		}

		public async Task<BookViewModel> GetNewAddBookModelCategories()
		{
			var categories = await _data.Categories.Select(c => new CategoryViewModel()
			{
				Id = c.Id,
				Name = c.Name,
			}).ToListAsync();

			var model = new BookViewModel()
			{
				Categories = categories
			};

			return model;
		}

		public async Task RemoveBookAsync(string userId,IdentityUserBook book)
		{
			_data.IdentityUserBooks.Remove(book);

			await _data.SaveChangesAsync();
		}

		public async Task<IdentityUserBook?> GetIdentityUserBookByIdAsync(int id)
		{
			return await _data.IdentityUserBooks
				.Where(b => b.BookId == id)
				.Select(b => new IdentityUserBook()
				{
					BookId = b.BookId,
					Book = b.Book,
					Collector = b.Collector,
					CollectorId = b.CollectorId
				}).FirstOrDefaultAsync();
		}
	}
}
