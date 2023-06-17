using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;
using TaskLibraryApp.RepositoryManager;

namespace TaskLibraryApp.Service
{
    public class BookService : IBookService
    {
        private IRepositoryManager _repositoryManager;

        public BookService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public bool BookTheBook(BookTheBookVM book)
        {
            using (var transaction = _repositoryManager.GetTransaction())
            {
                try
                {
                    var bookEntity = _repositoryManager.Books.GetById(book.BookId,true);
                    bookEntity.StatusId = (int)BookStatuses.Booked;
                    _repositoryManager.BookingHistory.Add(new BookingHistory() { BookId = book.BookId, UserId = book.UserId, BookDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) });
                    _repositoryManager.Save();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return false;
                }
            }
        }

        public void CreateBook(Book book)
        {
            _repositoryManager.Books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            _repositoryManager.Books.Delete(book);
        }

        public IEnumerable<Book> GetAllBooks(bool isTrackingChanges)
        {
            return _repositoryManager.Books.GetAllBooks(isTrackingChanges);
        }

        public Book GetById(int id, bool isTrackingChanges)
        {
            return _repositoryManager.Books.GetById(id, isTrackingChanges);
        }

        public void UpdateBook(Book Book)
        {
            _repositoryManager.Books.Update(Book);
        }
    }
}
