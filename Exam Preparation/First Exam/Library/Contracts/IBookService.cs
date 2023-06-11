using Library.Models.Book;

namespace Library.Contracts
{
	public interface IBookService
	{
		Task<IEnumerable<BookViewModel>> GetAllBooksAsync();

		Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);

		Task AddBookToCollectionAsync(string userId, BookViewModel book);

		Task<BookViewModel?> GetBookByIdAsync(int id);
	}
}
