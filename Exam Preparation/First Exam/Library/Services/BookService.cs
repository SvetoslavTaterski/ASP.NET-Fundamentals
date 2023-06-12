using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models.Book;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Library.Common.ValidationConstants;
using Book = Library.Data.Models.Book;

namespace Library.Services
{
	public class BookService : IBookService
	{
		private readonly LibraryDbContext _data;

		public BookService(LibraryDbContext data)
		{
			_data = data;
		}

		public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
		{
			var books = await _data
				.Books
				.Select(book => new BookViewModel()
				{
					Id = book.Id,
					Title = book.Title,
					Author = book.Author,
					Category = book.Category.Name,
					ImageUrl = book.ImageUrl,
					Rating = book.Rating,
					Description = book.Description
				})
				.ToArrayAsync();

			return books;
		}

		public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
		{
			return await _data.IdentityUserBooks
				.Where(ub => ub.CollectorId == userId)
				.Select(b => new AllBookViewModel
				{
					Id = b.Book.Id,
					Title = b.Book.Title,
					Author = b.Book.Author,
					ImageUrl = b.Book.ImageUrl,
					Description = b.Book.Description,
					Category = b.Book.Category.Name
				}).ToListAsync();
		}

		public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
		{
			bool alreadyAdded = await _data.IdentityUserBooks
				.AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

			if (alreadyAdded == false)
			{
				var userBook = new IdentityUserBook
				{
					CollectorId = userId,
					BookId = book.Id
				};

				await _data.IdentityUserBooks.AddAsync(userBook);
				await _data.SaveChangesAsync();
			}
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

		public async Task RemoveBookFromCollectionAsync(string userId, IdentityUserBook book)
		{
			bool isBookExisting =
				await _data.IdentityUserBooks
					.AnyAsync(b => b.BookId == book.BookId && b.CollectorId == userId);

			if (isBookExisting)
			{
				_data.IdentityUserBooks.Remove(book);
				await _data.SaveChangesAsync();
			}
		}

		public async Task AddNewBook(AddBookViewModel book)
		{
			Book newBook = new Book()
			{
				Title = book.Title,
				Author = book.Author,
				CategoryId = book.CategoryId,
				Description = book.Description,
				ImageUrl = book.Url,
				Rating = decimal.Parse(book.Rating)
			};

			await _data.Books.AddAsync(newBook);
			await _data.SaveChangesAsync();
		}

		public async Task<AddBookViewModel> GetNewAddBookModelCategories()
		{
			var categories = await _data.Categories.Select(c => new CategoryViewModel
			{
				Id = c.Id,
				Name = c.Name,
			}).ToListAsync();

			var model = new AddBookViewModel()
			{
				Categories = categories
			};

			return model;
		}
	}
}
