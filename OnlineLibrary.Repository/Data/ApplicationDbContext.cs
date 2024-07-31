using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Identity;

namespace OnlineLibrary.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<LibraryMember>
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookCart> Carts { get; set; }
        public virtual DbSet<RentedBook> RentedBooks { get; set; }
        public virtual DbSet<BookInCart> BooksInCarts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
