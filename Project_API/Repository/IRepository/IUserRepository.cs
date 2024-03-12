using Project_API.Models;

namespace Project_API.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string email);
        Task<User> Authenticate(string email, string password);
        Task<User> Register(User user);
        bool IsValidPassword(string password);
        ICollection<User> GetUsers();
    }
}
