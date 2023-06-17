using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp.Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool isTrackingChanges);
        Book GetById(int id, bool isTrackingChanges);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        bool BookTheBook(BookTheBookVM book);
    }
}
