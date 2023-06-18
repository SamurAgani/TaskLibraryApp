using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        List<UserHistoryVM> GetUserHistory(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
