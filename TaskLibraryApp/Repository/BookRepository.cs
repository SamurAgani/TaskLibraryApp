using TaskLibraryApp.Entities;
using TaskLibraryApp.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace TaskLibraryApp.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryDBContext dBContext) : base(dBContext)
        {}
        public void CreateBook(Book book)
        {
            Add(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }

        public IEnumerable<Book> GetAllBooks(bool isTrackingChanges)
        {
            return GetAll(isTrackingChanges).Include(x=>x.Status);
        }

        public Book GetById(int id, bool isTrackingChanges)
        {
            return GetWithCondition(c => c.Id == id, isTrackingChanges).Include(x=>x.Category)
                                                                       .Include(x=>x.Status)
                                                                       .FirstOrDefault();
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }
    }
}
