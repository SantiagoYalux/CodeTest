namespace CodeTestTotal.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(string username, string password);
        Task<bool> Login(string username, string password);
    }
}
