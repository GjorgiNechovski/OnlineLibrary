using Microsoft.VisualBasic;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Dto;
using OnlineLibrary.Domain.Exceptions;
using OnlineLibrary.Domain.Identity;
using OnlineLibrary.Repository.Interface;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Service.Implementation
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public void Delete(LibraryMember entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<LibraryMember> GetAll()
        {
            return repository.GetAll();
        }

        public LibraryMember GetFullUser(string? id)
        {
            var user = repository.GetFullUser(id);

            return user ?? throw new NotFoundException("We could not find the user!");
        }

        public IEnumerable<LibraryMemberDto> GetAllLibraryMembersDto()
        {
            return repository.GetAll().Select(user => new LibraryMemberDto { Id = user.Id, Name = user.Name, Email = user.Email!, LastName= user.LastName });
        }

        public List<RentedBook> GetRentedBooksFromUserDayFromDueDate(string? id)
        {
            var user = repository.GetUserWithOnlyRentedBooks(id);
            var tomorrow = DateTime.Today.AddDays(1);

            return user.RentedBooks
                .Where(book => book.DateReturned != null && book.DueDate == tomorrow)
                .ToList();
        }

        public LibraryMember GetUserWithOnlyRentedBooks(string? id)
        {
            var user = repository.GetUserWithOnlyRentedBooks(id);

            return user ?? throw new NotFoundException("We could not find the user!");
        }

        public LibraryMember GetUserWithOnlyShoppingCart(string? id)
        {
            var user = repository.GetUserWithOnlyShoppingCart(id);

            return user ?? throw new NotFoundException("We could not find the user!");
        }

        public void Insert(LibraryMember entity)
        {
            repository.Insert(entity);
        }

        public void Update(LibraryMember entity)
        {
            repository.Update(entity);
        }
    }
}
