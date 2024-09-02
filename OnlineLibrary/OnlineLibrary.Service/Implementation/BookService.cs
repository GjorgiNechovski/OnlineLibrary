using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Exceptions;
using OnlineLibrary.Repository.Interface;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Service.Implementation
{
    public class BookService(IBookRepository bookRepository, IUserRepository userRepository) : IBookService
    {
        public void CreateNewBook(Book p)
        {
            bookRepository.Insert(p);
        }

        public void DeleteBook(Guid id)
        {
            var book = GetBookDetailsById(id);
            bookRepository.Delete(book);

        }

        public void EditBook(Book b)
        {
            bookRepository.Update(b);

        }

        public IEnumerable<Book> GetAllBooks()
        {
            return bookRepository.GetAll();
        }

        public IEnumerable<Book> GetAllBooksFiltered(string? title, Guid? authorId, bool? inStock)
        {
            return bookRepository.GetAllFiltered(title, authorId, inStock);
        }

        public Book GetBookDetailsById(Guid? id)
        {
            if (id != null)
                return bookRepository.GetById(id);

            throw new NotFoundException("Book not found! ");
        }

        public List<RentedBook> GetRentedBooksForUser(string userId)
        {
            var user = userRepository.GetUserWithOnlyRentedBooks(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");

            return user.RentedBooks.ToList();
        }

        public void ReturnBook(string userId, Guid rentedId)
        {
            var user = userRepository.GetUserWithOnlyRentedBooks(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");

            var rentedBook = user.RentedBooks.FirstOrDefault(book => book.RentId.Equals(rentedId))
                             ?? throw new NotFoundException("The rented book could not be located!");

            if (rentedBook.DateReturned != null)
            {
                throw new InvalidOperationException("This book has already been returned.");
            }


            var book = bookRepository.GetById(rentedBook.BookId)
                       ?? throw new NotFoundException("The book could not be located in the repository!");

            rentedBook.DateReturned = DateTime.Now;
            book.QuantityInInventory += rentedBook.Quantity;

            bookRepository.Update(book);
            userRepository.Update(user);
        }
    }
}
