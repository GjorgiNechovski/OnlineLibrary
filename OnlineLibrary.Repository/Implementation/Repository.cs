using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Repository.Data;
using OnlineLibrary.Repository.Interface;
namespace OnlineLibrary.Repository.Implementation
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext context = context;
        protected readonly DbSet<T> entities = context.Set<T>();

        public virtual IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public virtual T GetById(Guid? id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
