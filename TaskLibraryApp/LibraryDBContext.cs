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
            if (!BookStatus.Any())
            {
                BookStatus.Add(new BookStatus() { Name = "Available" });
                BookStatus.Add(new BookStatus() { Name = "Booked" });
                SaveChanges();
            }
            if (!Categories.Any())
            {
                Categories.Add(new Category() { Name = "Action" });
                Categories.Add(new Category() { Name = "Dram" });
                SaveChanges();
            }
        }
    }
}
