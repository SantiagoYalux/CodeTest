namespace CodeTestTotal.Interfaces
{
    public interface IUserService
    {
        Task<bool> Login(string username, string password);
    }
}
