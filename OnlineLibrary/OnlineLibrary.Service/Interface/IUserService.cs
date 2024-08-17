using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Dto;
using OnlineLibrary.Domain.Identity;

namespace OnlineLibrary.Service.Interface
{
    public interface IUserService
    {
        IEnumerable<LibraryMember> GetAll();
        IEnumerable<LibraryMemberDto> GetAllLibraryMembersDto();
        LibraryMember GetFullUser(string? id);
        LibraryMember GetUserWithOnlyRentedBooks(string? id);
        LibraryMember GetUserWithOnlyShoppingCart(string? id);
        void Insert(LibraryMember entity);
        void Update(LibraryMember entity);
        void Delete(LibraryMember entity);
        List<RentedBook> GetRentedBooksFromUserDayFromDueDate(string? id);
    }
}
