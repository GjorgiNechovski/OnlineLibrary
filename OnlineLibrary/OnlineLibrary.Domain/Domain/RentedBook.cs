using OnlineLibrary.Domain.Identity;

namespace OnlineLibrary.Domain.Domain
{
    public class RentedBook : BaseEntity
    {
        public Guid RentId { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public string LibraryMemberId { get; set; }
        public LibraryMember? LibraryMember { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        public DateTime DueDate { get; set; }
        public int Quantity { get; set; }
    }
}
