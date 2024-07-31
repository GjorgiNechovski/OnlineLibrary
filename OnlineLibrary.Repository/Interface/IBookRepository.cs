using OnlineLibrary.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Repository.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetAllFiltered(string? title, Guid? authorId, bool? inStock);
    }
}
