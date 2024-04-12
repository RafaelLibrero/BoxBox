using BoxBox.Models;

namespace BoxBox.Repositories
{
    public interface IRepositoryAuth
    {
        Task<User> LoginUserAsync(string email, string password);
        Task<User> Register(string userName, string email, string password);
    }
}