using Microsoft.EntityFrameworkCore;
using TaskLibraryApp.Entities;

namespace TaskLibraryApp
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookingHistory> BookingHistory { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
        {

        }
    }
}
