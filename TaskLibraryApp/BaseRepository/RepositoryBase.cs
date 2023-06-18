using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskLibraryApp.BaseRepository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private LibraryDBContext _libraryDBContext { get; set; }
        public RepositoryBase(LibraryDBContext libraryDBContext)
        {
            _libraryDBContext = libraryDBContext;
        }

        public void Add(T entity)
        {
            _libraryDBContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _libraryDBContext.Set<T>().Remove(entity);
        }
        //The AsNoTracking() extension method returns a new query and the returned entities will not be cached by the context (DbContext or _libraryDBContext)
        public IQueryable<T> GetAll()
        {
            return _libraryDBContext.Set<T>();
        }

        public IQueryable<T> GetWithCondition(Expression<Func<T, bool>> expression)
        {
           return _libraryDBContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _libraryDBContext.Set<T>().Update(entity);
        }
    }
}
