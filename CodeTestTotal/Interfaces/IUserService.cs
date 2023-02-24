using CodeTestTotal.Models;

namespace CodeTestTotal.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddRoleUser(Usuario user, string roleName);
        Task<int> AddUser(string username, string password);
        Task<int> AddUser(Usuario user);
        Task<Usuario> GetUserById(int userID);
        Task<List<string>> GetUserRoles(Usuario user);
        Task<List<Usuario>> GetUsersInRole(string RoleName);
        Task<bool> Login(string username, string password);
        Task<Usuario> SearchUserByUsername(string username);
    }
}
