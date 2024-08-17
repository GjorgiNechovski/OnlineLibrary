using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Domain.Domain;

namespace OnlineLibrary.Domain.Identity
{
    public class LibraryMember : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid? BookCartId { get; set; }
        public BookCart? BookCart { get; set; }
        public bool IsStripedConnected { get; set; }
        public virtual ICollection<RentedBook>? RentedBooks { get; set; }
    }
}
