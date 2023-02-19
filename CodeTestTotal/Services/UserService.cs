using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;

namespace CodeTestTotal.Services
{
    public class UserService : IUserService
    {
        private DBContext _dbContext;
        public UserService(DBContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public Task<bool> Login(string username, string password)
        {
            var users = _dbContext.Usuarios;

            foreach (var user in users)
            {
                if(user.UsuarioUsername == username && user.UsuarioPassword == password)
                {
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }
    }
}
