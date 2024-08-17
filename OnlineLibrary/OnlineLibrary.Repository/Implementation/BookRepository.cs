using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Repository.Data;
using OnlineLibrary.Repository.Interface;

namespace OnlineLibrary.Repository.Implementation
{
    public class BookRepository(ApplicationDbContext context) : Repository<Book>(context), IBookRepository
    {
        public override IEnumerable<Book> GetAll()
        {
            return entities.Include(b => b.Author).AsEnumerable();
        }

        public List<Book> GetAllFiltered(string? title, Guid? authorId, bool? inStock)
        {
            var query = context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            if (inStock.HasValue && inStock.Value == true)
            {
                query = query.Where(b => b.QuantityInInventory > 0);
            }
            else if(inStock.HasValue && inStock.Value == false)
            {
                query = query.Where(b => b.QuantityInInventory == 0);
            }

            return query.ToList();
        }

        public override Book GetById(Guid? id)
        {
            return entities.Include(b => b.Author)
                           .SingleOrDefault(b => b.Id == id);
        }
    }
}
