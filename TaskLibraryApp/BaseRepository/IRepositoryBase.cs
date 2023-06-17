using System.Linq.Expressions;

namespace TaskLibraryApp.BaseRepository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll(bool isTrackChanges);
        IQueryable<T> GetWithCondition(Expression<Func<T, bool>> expression, bool isTrackChanges);

        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);
    }
}
