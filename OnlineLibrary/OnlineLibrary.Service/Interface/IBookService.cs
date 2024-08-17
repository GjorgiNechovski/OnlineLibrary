using OnlineLibrary.Domain.Domain;

namespace OnlineLibrary.Service.Interface
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetAllBooksFiltered(string? title, Guid? authorId, bool? inStock);
        Book GetBookDetailsById(Guid? id);
        void CreateNewBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Guid id);
        List<RentedBook> GetRentedBooksForUser(string userId);
        void ReturnBook(string userId, Guid bookId);
    }
}
