using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Exceptions;
using OnlineLibrary.Domain.Helper;
using OnlineLibrary.Repository.Interface;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Service.Implementation
{
    public class OrderService(IUserRepository userRepository, IBookRepository bookRepository, IEmailService emailService) : IOrderService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IBookRepository bookRepository = bookRepository;

        public void AddToCart(Guid bookId, int quantity, string userId)
        {
            var user = userRepository.GetUserWithOnlyShoppingCart(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");
            var cart = user.BookCart;

            if (cart == null)
            {
                cart = new BookCart
                {
                    Books = []
                };
                user.BookCart = cart;
            }

            var book = bookRepository.GetById(bookId) ?? throw new NotFoundException("Book not found!");

            var bookInCart = cart.Books.FirstOrDefault(bic => bic.BookId == bookId);

            if (bookInCart != null)
            {
                bookInCart.Quantity += quantity;
            }
            else
            {
                bookInCart = new BookInCart
                {
                    BookId = book.Id,
                    Book = book,
                    Quantity = quantity,
                };
                cart.Books.Add(bookInCart);
            }

            book.QuantityInInventory-=quantity;

            bookRepository.Update(book);
            userRepository.Update(user);
        }

        public List<BookInCart> GetBookCart(string userId)
        {
            var user = userRepository.GetUserWithOnlyShoppingCart(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");
            var books = user.BookCart?.Books?.ToList() ?? [];

            return books;
        }

        public void DeleteBookFromCart(Guid bookId, string userId)
        {
            var user = userRepository.GetUserWithOnlyShoppingCart(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");
            var cart = user.BookCart;
            var book = bookRepository.GetById(bookId);

            if (cart != null)
            {
                var bookInCart = cart.Books.FirstOrDefault(bic => bic.BookId == bookId);

                if (bookInCart != null)
                {
                    cart.Books.Remove(bookInCart);
                    book.QuantityInInventory += bookInCart.Quantity;

                    bookRepository.Update(book);
                    userRepository.Update(user);
                }
            }
        }

        public void RentBooks(string? userId)
        {
            var user = userRepository.GetUserWithOnlyShoppingCart(userId) ?? throw new NotFoundException("We could not locate the user with the given id!");
            var cart = user.BookCart;
            DateTime timeToReturn = DateTime.Now.AddDays(10);

            var count = 0;

            if(cart != null)
            {
                user.RentedBooks ??= [];
                var booksInCart = cart.Books;


                foreach(var bookInCart in booksInCart)
                {
                    RentedBook rentedBook = new()
                    {
                        BookId = bookInCart.BookId,
                        Book = bookInCart.Book,
                        LibraryMemberId = userId,
                        LibraryMember = user,
                        DateRented = DateTime.Now,
                        DateReturned = null,
                        DueDate = timeToReturn,
                        Quantity = bookInCart.Quantity,
                    };

                    user.RentedBooks.Add(rentedBook);
                    count+=bookInCart.Quantity;
                }

                cart.Books.Clear();
                userRepository.Update(user);
            }

            emailService.SendEmailAsync(new EmailMessage(user.Email, "Rented Books", $"You have succefully rented {count} books and you have until {timeToReturn} to return them"));
        }
    }
}
