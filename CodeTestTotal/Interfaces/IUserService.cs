using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(string username, string password);
        Task<int> AddUser(Usuario user);
        int GetCurrentUser();
        Task<bool> Login(string username, string password);
        Task<Usuario> SearchUserByUsername(string username);
    }
}
