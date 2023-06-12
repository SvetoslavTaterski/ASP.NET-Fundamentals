using Library.Data.Models;
using Library.Models.Book;

namespace Library.Contracts
{
	public interface IBookService
	{
		Task<IEnumerable<BookViewModel>> GetAllBooksAsync();

		Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);

		Task AddBookToCollectionAsync(string userId, BookViewModel book);

		Task<BookViewModel?> GetBookByIdAsync(int id);

		Task RemoveBookFromCollectionAsync(string userId, IdentityUserBook book);

		Task<IdentityUserBook?> GetIdentityUserBookByIdAsync(int id);

		Task AddNewBook(AddBookViewModel book);

		Task<AddBookViewModel> GetNewAddBookModelCategories();
	}
}
