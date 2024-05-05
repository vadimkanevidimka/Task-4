using Task4DataBase.Models;

namespace Task_4.Interfaces
{
    public interface IUser
    {
        public List<User> GetAll();
        public User GetById(int id);
        public bool DeleteUser(User user);
        public int UpdateUser(User _newUserData);
        public int AddUser(User user);
        public bool IsUserRegistered(User user);
        public int GetUserId(User user);
        public int BlockUsers(List<int> usersid);
        public int UnblockUsers(List<int> usersid);
        public int DeleteUsers(List<int> usersid);
    }
}
