using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers(bool isTrackingChanges);
        User GetById(int id, bool isTrackingChanges);
        User GetByEmail(string mail, bool isTrackingChanges);
        List<UserHistoryVM> GetUserHistory(string mail);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User CheckUser(VMLogin vMLogin);
    }
}
