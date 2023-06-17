using TaskLibraryApp.Entities;

namespace TaskLibraryApp.CacheManager
{
    public interface ICacheManager
    {
        List<BookStatus> GetBookStatuses();
        List<Category> GetCategories();
    }
}
