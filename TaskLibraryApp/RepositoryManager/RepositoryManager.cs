using Microsoft.EntityFrameworkCore.Storage;
using TaskLibraryApp.BaseRepository;
using TaskLibraryApp.Entities;
using TaskLibraryApp.Repository;

namespace TaskLibraryApp.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LibraryDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public RepositoryManager(LibraryDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        private UserRepository _userRepository;
        public UserRepository Users
        {
            get 
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext, _configuration);
                return _userRepository;
            }
        }
        private BookRepository _bookRepository;

        public BookRepository Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_dbContext);
                return _bookRepository;
            }
        }
        private RepositoryBase<BookStatus> _bookStatuses;

        public RepositoryBase<BookStatus> BookStatuses
        {
            get
            {
                if (_bookStatuses == null)
                    _bookStatuses = new RepositoryBase<BookStatus>(_dbContext);
                return _bookStatuses;
            }
        }

        private RepositoryBase<Category> _category;
        public RepositoryBase<Category> Categories
        {
            get
            {
                if (_category == null)
                    _category = new RepositoryBase<Category>(_dbContext);
                return _category;
            }
        }


        private RepositoryBase<BookingHistory> _bookingHistory;
        public RepositoryBase<BookingHistory> BookingHistory
        {
            get
            {
                if (_bookingHistory == null)
                    _bookingHistory = new RepositoryBase<BookingHistory>(_dbContext);
                return _bookingHistory;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IDbContextTransaction GetTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
    }
}
