using OnlineLibrary.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Service.Interface
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthours();
        Author GetAuthorDetails(Guid? id);
        void CreateNewAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(Guid id);
    }
}
