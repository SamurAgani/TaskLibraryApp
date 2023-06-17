namespace TaskLibraryApp.Models
{
    public class UserHistoryVM
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
