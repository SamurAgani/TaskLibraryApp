namespace TaskLibraryApp.Entities
{
    public class BookingHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
