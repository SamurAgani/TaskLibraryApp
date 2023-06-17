using Microsoft.EntityFrameworkCore;
using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;
using TaskLibraryApp.RepositoryManager;

namespace TaskLibraryApp.Service
{
    public class UserService : IUserService
    {
        private IRepositoryManager _repositoryManager;

        public UserService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public User CheckUser(VMLogin vMLogin)
        {
            var user = _repositoryManager.Users.GetWithCondition(x=>x.Email == vMLogin.Email && x.Password == vMLogin.PassWord, true).FirstOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            _repositoryManager.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _repositoryManager.Users.Delete(user);
        }

        public IEnumerable<User> GetAllUsers(bool isTrackingChanges)
        {
            return _repositoryManager.Users.GetAllUsers(isTrackingChanges);
        }

        public User GetByEmail(string mail, bool isTrackingChanges)
        {
            var user = _repositoryManager.Users.GetWithCondition(x => x.Email == mail, isTrackingChanges).FirstOrDefault();
            return user;
        }

        public User GetById(int id, bool isTrackingChanges)
        {
            return _repositoryManager.Users.GetById(id, isTrackingChanges);
        }

        public List<UserHistoryVM> GetUserHistory(string mail)
        {
            var result = _repositoryManager.Users.GetUserHistory(mail);
            return result;
        }

        public void UpdateUser(User user)
        {
            _repositoryManager.Users.Update(user);
        }
    }
}
