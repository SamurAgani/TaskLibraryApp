namespace TaskLibraryApp.Entities
{
    public class Book
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
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
