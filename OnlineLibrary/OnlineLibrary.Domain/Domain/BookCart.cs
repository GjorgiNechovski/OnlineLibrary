namespace OnlineLibrary.Domain.Domain
{
    public class BookCart : BaseEntity
    {
        public ICollection<BookInCart>? Books { get; set; }
    }
}
