using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User GetByEmail(string mail);
        List<UserHistoryVM> GetUserHistory(string mail);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User CheckUser(VMLogin vMLogin);
    }
}
