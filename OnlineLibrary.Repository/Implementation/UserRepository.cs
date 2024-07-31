using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Identity;
using OnlineLibrary.Repository.Data;
using OnlineLibrary.Repository.Interface;

namespace OnlineLibrary.Repository.Implementation
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext context = context;
        private readonly DbSet<LibraryMember> entities = context.Set<LibraryMember>();

        public IEnumerable<LibraryMember> GetAll()
        {
            return entities.AsEnumerable();
        }

        public LibraryMember? GetFullUser(string id)
        {
            return entities
                .Include(user => user.BookCart)
                .ThenInclude(bookCart => bookCart.Books)
                .ThenInclude(bookInCart => bookInCart.Book)
                .ThenInclude(book => book.Author)
                .Include(user=>user.RentedBooks)
                .ThenInclude(rentedBook => rentedBook.Book)
                .ThenInclude(book => book.Author)
                .SingleOrDefault(user => user.Id == id);
        }

        public LibraryMember GetUserWithOnlyRentedBooks(string? id)
        {
            return entities.Include(user=> user.RentedBooks)
                .ThenInclude(rentedBook => rentedBook.Book)
                .ThenInclude(book => book.Author)
                .SingleOrDefault(user => user.Id == id);
        }

        public LibraryMember GetUserWithOnlyShoppingCart(string? id)
        {
            return entities
                .Include(user => user.BookCart)
                .ThenInclude(bookCart => bookCart.Books)
                .ThenInclude(bookInCart => bookInCart.Book)
                .ThenInclude(book => book.Author)
                .SingleOrDefault(user => user.Id == id);
            ;
        }
        public void Insert(LibraryMember entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(LibraryMember entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(LibraryMember entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entities.Remove(entity);
            context.SaveChanges();
        }

      
    }
}

