using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
	public interface IBookService
	{
		Task<IEnumerable<BookViewModel>> GetBooksAsync();

		Task AddBookToCollectionAsync(string userId,BookViewModel model);

		Task<BookViewModel?> GetBookByIdAsync(int id);

		Task<IEnumerable<BookViewModel>> GetAllMyBooksAsync(string userId);

		Task<BookViewModel> GetNewAddBookModelCategories();

		Task AddNewBook(BookViewModel model);

		Task RemoveBookAsync(string userId,IdentityUserBook book);

		Task<IdentityUserBook?> GetIdentityUserBookByIdAsync(int id);
	}
}
