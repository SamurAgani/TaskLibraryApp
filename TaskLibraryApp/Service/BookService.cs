using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;
using TaskLibraryApp.RepositoryManager;

namespace TaskLibraryApp.Service
{
    public class BookService : IBookService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public BookService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public bool BookTheBook(BookTheBookVM book)
        {
            using (var transaction = _repositoryManager.GetTransaction())
            {
                try
                {
                    var bookEntity = _repositoryManager.Books.GetById(book.BookId);
                    bookEntity.StatusId = (int)BookStatuses.Booked;
                    _repositoryManager.BookingHistory.Add(new BookingHistory() { BookId = book.BookId, UserId = book.UserId, BookDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) });
                    _repositoryManager.Books.Update(bookEntity);
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

        public void CheckBookStatuses()
        {
            #region check enddates
            var allBookedBooks = _repositoryManager.Books.GetWithCondition(x => x.Status.Id == (int)BookStatuses.Booked).ToList();

            foreach (var book in allBookedBooks)
            {
                var shouldGiveBack = _repositoryManager.BookingHistory.GetWithCondition(x => x.BookId == book.Id && x.EndDate > DateTime.Now).Any();
                if (!shouldGiveBack)
                    GiveBookBack(book.Id);
            }
            #endregion
        }

        public void CreateOrUpdateBook(CreateUpdateBookVM bookCreate)
        {
            string frontGouid = Guid.NewGuid().ToString();
            string backGouid = Guid.NewGuid().ToString();
            bookCreate.PhotoUrl = "/Photos/" + frontGouid + bookCreate.PhotoUrlFile.FileName;
            bookCreate.BackPhotoUrl = "/Photos/" + backGouid + bookCreate.BackPhotoUrlFile.FileName;
            string frontPhotoUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", frontGouid + bookCreate.PhotoUrlFile.FileName);
            string backPhotoUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", backGouid + bookCreate.BackPhotoUrlFile.FileName);
            using (var stream = new FileStream(frontPhotoUrl, FileMode.Create))
            {
                bookCreate.PhotoUrlFile.CopyTo(stream);
            }
            using (var stream = new FileStream(backPhotoUrl, FileMode.Create))
            {
                bookCreate.BackPhotoUrlFile.CopyTo(stream);
            }
          
            var book = _mapper.Map<Book>(bookCreate);
            book.StatusId = (int)BookStatuses.Available;
            if(bookCreate.Id == null)
                _repositoryManager.Books.Add(book);
            else
                _repositoryManager.Books.UpdateBook(book);
            _repositoryManager.Save();
        }

        public void DeleteBook(Book book)
        {
            _repositoryManager.Books.Delete(book);
        }

        public IEnumerable<Book> GetAllBooks(bool isTrackingChanges)
        {
            var allBooks = _repositoryManager.Books.GetAllBooks();
            
            return allBooks;
        }

        public BookDetailsVM GetBookDetails(int id, int userId)
        {
            var book = _repositoryManager.Books.GetById(id);
            var isThisUserBooked = _repositoryManager.BookingHistory.GetWithCondition(x => x.UserId == userId && x.BookId == id && x.EndDate > DateTime.Now).Any();
            var bookDetails = _mapper.Map<BookDetailsVM>(book);
            bookDetails.LoginUserBooked = isThisUserBooked;
            return bookDetails;
        }

        public Book GetById(int id, bool isTrackingChanges)
        {
            return _repositoryManager.Books.GetById(id);
        }

        public IEnumerable<Book> GetMyAllBooks(int userId)
        {
            return _repositoryManager.Books.GetWithCondition(x=>x.UserId == userId).Include(x=>x.Status);
        }

        public bool GiveBookBack(int bookId)
        {
            using (var transaction = _repositoryManager.GetTransaction())
            {
                try
                {
                    var bookEntity = _repositoryManager.Books.GetById(bookId);
                    bookEntity.StatusId = (int)BookStatuses.Available;
                    var bookHistory = _repositoryManager.BookingHistory.GetWithCondition(x=>x.BookId == bookId).OrderByDescending(x=>x.Id).FirstOrDefault();
                    bookHistory.EndDate = DateTime.Now;
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

        public void UpdateBook(Book Book)
        {
            _repositoryManager.Books.Update(Book);
        }
    }
}
