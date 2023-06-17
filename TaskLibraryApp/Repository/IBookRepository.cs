using TaskLibraryApp.Entities;

namespace TaskLibraryApp.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks(bool isTrackingChanges);
        Book GetById(int id, bool isTrackingChanges);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
