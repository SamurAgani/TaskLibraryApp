using TaskLibraryApp.Entities;

namespace TaskLibraryApp.Models
{
    public class BookDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string PhotoUrl { get; set; }
        public string BackPhotoUrl { get; set; }
        public Category Category { get; set; }
        public BookStatus Status { get; set; }
        public User User { get; set; }

        public bool LoginUserBooked { get; set; }
    }
}
