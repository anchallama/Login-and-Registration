using Login_and_Registration.Models;

namespace Login_and_Registration.Repository
{
    public interface IUserRepository
    {
        public  Task<int> AddUser(User user);
        public  Task<User> GetUserByUsername(string username);
    }
}
