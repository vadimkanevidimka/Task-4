using Task_4.Interfaces;
using Task4DataBase.Context;
using Task4DataBase.Models;

namespace Task_4.Mocks
{
    public class UserMocks : IUser
    {
        private readonly DataBaseContext _context;
        public UserMocks(DataBaseContext contaxt) 
        {
            _context = contaxt;
        }
        public int AddUser(User user)
        {
            this._context.Users.Add(user);
            return this._context.SaveChanges();
        }

        public bool DeleteUser(User user)
        {
            this._context.Users.Remove(user);
            return this._context.SaveChanges() > 0;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User GetById(int id) => _context?.Users?.Where(x => x.Id == id)?.First();

        public int UpdateUser(User _newUserData)
        {
            _context.Users.Update(_newUserData);
            return _context.SaveChanges();
        }

        public bool IsUserRegistered(User user)
        {
            return _context.Users.Any(x => x.Email == user.Email);
        }

        public int GetUserId(User user)
        {
            return _context.Users.Where(c => c.Email == user.Email).First().Id;
        }

        public int BlockUsers(List<int> usersid)
        {
            var userstoblock = new List<User>();
            foreach (var user in usersid) 
            {
                var usertoblock = _context.Users.Where(c => c.Id == user).First();
                usertoblock.IsBlocked = true;
                _context.Users.Update(usertoblock);
            }
            return _context.SaveChanges();
        }

        public int UnblockUsers(List<int> usersid)
        {
            var userstoblock = new List<User>();
            foreach (var user in usersid)
            {
                var usertoblock = _context.Users.Where(c => c.Id == user).First();
                usertoblock.IsBlocked = false;
                _context.Users.Update(usertoblock);
            }
            return _context.SaveChanges();
        }

        public int DeleteUsers(List<int> usersid)
        {
            var userstoblock = new List<User>();
            foreach (var user in usersid)
            {
                var usertoblock = _context.Users.Where(c => c.Id == user).First();
                _context.Users.Remove(usertoblock);
            }
            return _context.SaveChanges();
        }
    }
}
