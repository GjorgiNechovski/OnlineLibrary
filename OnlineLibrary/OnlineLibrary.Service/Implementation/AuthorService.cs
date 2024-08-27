using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.Exceptions;
using OnlineLibrary.Repository.Interface;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Service.Implementation
{
    public class AuthorService(IRepository<Author> authorRepository) : IAuthorService
    {
        public void CreateNewAuthor(Author author)
        {
            authorRepository.Insert(author);
        }

        public void DeleteAuthor(Guid id)
        {
            var author = GetAuthorDetails(id);
            authorRepository.Delete(author);
        }

        public void EditAuthor(Author a)
        {
            authorRepository.Update(a);
        }

        public IEnumerable<Author> GetAllAuthours()
        {
            return authorRepository.GetAll();
        }

        public Author GetAuthorDetails(Guid? id)
        {
            if (id != null)
                return authorRepository.GetById(id);

            throw new NotFoundException("Author not found! ");
        }
    }
}
