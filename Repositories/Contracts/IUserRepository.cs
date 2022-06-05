using LWAApi.Models;
using LWAApi.Views;

namespace LWAApi.Repositories.Contracts
{
    public interface  IUserRepository
    {
        public List<User> GetUsers();
        public void CreateUser(User User);
        public void UpdateUser(User User);
        public User DeleteUser(int userId);

        public User GetUser(int userId);

        bool DoesUserEixsts(int UserId);









    }
}
