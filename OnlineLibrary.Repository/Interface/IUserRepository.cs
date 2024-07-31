using OnlineLibrary.Domain.Identity;

namespace OnlineLibrary.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<LibraryMember> GetAll();
        LibraryMember GetFullUser(string? id);
        LibraryMember GetUserWithOnlyRentedBooks(string? id);
        LibraryMember GetUserWithOnlyShoppingCart(string? id);
        void Insert(LibraryMember entity);
        void Update(LibraryMember entity);
        void Delete(LibraryMember entity);
    }
}
