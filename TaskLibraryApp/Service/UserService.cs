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
            var user = _repositoryManager.Users.GetWithCondition(x=>x.Email == vMLogin.Email && x.Password == vMLogin.PassWord).FirstOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            _repositoryManager.Users.Add(user);
            _repositoryManager.Save();
        }

        public void DeleteUser(User user)
        {
            _repositoryManager.Users.Delete(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repositoryManager.Users.GetAllUsers();
        }

        public User GetByEmail(string mail)
        {
            var user = _repositoryManager.Users.GetWithCondition(x => x.Email == mail).FirstOrDefault();
            return user;
        }

        public User GetById(int id)
        {
            return _repositoryManager.Users.GetById(id);
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
