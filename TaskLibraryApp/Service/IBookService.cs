using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp.Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool isTrackingChanges);
        IEnumerable<Book> GetMyAllBooks(int userId);
        Book GetById(int id, bool isTrackingChanges);
        BookDetailsVM GetBookDetails(int id, int userId);
        void CreateOrUpdateBook(CreateUpdateBookVM book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void CheckBookStatuses();
        bool BookTheBook(BookTheBookVM book);
        bool GiveBookBack(int bookId);
    }
}
