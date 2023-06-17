using Microsoft.Extensions.Caching.Memory;
using TaskLibraryApp.Entities;
using TaskLibraryApp.RepositoryManager;

namespace TaskLibraryApp.CacheManager
{
    public class CacheManager: ICacheManager
    {
        private IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        public IRepositoryManager repositoryManager;

        public CacheManager(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public List<BookStatus> GetBookStatuses()
        {
            List<BookStatus> Statuses;
            Statuses = memoryCache.Get("BookStatuses") as List<BookStatus>;
            if(Statuses == null)
            {
                Statuses = repositoryManager.BookStatuses.GetAll(true).ToList();
                var cacheOption = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };
                memoryCache.Set("BookStatuses", Statuses, cacheOption);
            }
            return Statuses;
        }

        public List<Category> GetCategories()
        {
            List<Category> Categories;
            Categories = memoryCache.Get("BookCategories") as List<Category>;
            if (Categories == null)
            {
                Categories = repositoryManager.Categories.GetAll(true).ToList();
                var cacheOption = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };
                memoryCache.Set("BookCategories", Categories, cacheOption);
            }
            return Categories;
        }
    }
}
