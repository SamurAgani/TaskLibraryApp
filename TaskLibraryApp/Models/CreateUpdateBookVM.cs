namespace TaskLibraryApp.Models
{
    public class CreateUpdateBookVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public int UserId { get; set; }
        public IFormFile PhotoUrlFile { get; set; }
        public IFormFile BackPhotoUrlFile { get; set; }

        public string PhotoUrl { get; set; }
        public string BackPhotoUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
