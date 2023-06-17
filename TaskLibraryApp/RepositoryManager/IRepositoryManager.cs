using TaskLibraryApp.Repository;
using TaskLibraryApp.BaseRepository;
using TaskLibraryApp.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace TaskLibraryApp.RepositoryManager
{
    public interface IRepositoryManager
    {
        void Save();
        IDbContextTransaction GetTransaction();
        UserRepository Users { get; }
        BookRepository Books { get; }
        RepositoryBase<BookStatus> BookStatuses { get; }
        RepositoryBase<Category> Categories { get; }
        RepositoryBase<BookingHistory> BookingHistory { get; }
    }
}
