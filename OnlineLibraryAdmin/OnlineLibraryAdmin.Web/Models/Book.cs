namespace OnlineLibraryAdmin.Web.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public string BookCoverUrl { get; set; } = string.Empty;
        public int Pages { get; set; }
        public int QuantityInInventory { get; set; }
    }
}
