namespace OnlineLibraryAdmin.Web.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<Book>? WrittenBooks { get; set; }

        public string GetFullName()
        {
            return Name + " " + LastName;
        }
    }
}
