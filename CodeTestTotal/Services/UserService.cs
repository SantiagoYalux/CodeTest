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
                if(user.UsuarioUsername == username && user.UsuarioPasswordHash == password)
                {
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public async Task<int> AddUser(string username, string password)
        {
            int result = 0;
            Usuario newUser = new Usuario();

            newUser.UsuarioId = await _dbContext.GetLastId(newUser) + 1;
            newUser.UsuarioUsername = username;
            newUser.UsuarioPasswordHash = password;

            bool resulta = await _dbContext.AddNewRegister(newUser);

            return newUser.UsuarioId;
        }
        public async Task<int> AddUser(Usuario user)
        {
            int result = 0;
            Usuario newUser = new Usuario();
            newUser.UsuarioId = await _dbContext.GetLastId(newUser) + 1;
            newUser.UsuarioUsername = user.UsuarioUsername;
            newUser.UsuarioUsernameNormalizado = user.UsuarioUsernameNormalizado;
            newUser.UsuarioPasswordHash = user.UsuarioPasswordHash;

            bool resulta = await _dbContext.AddNewRegister(newUser);

            return newUser.UsuarioId;
        }

        public async Task<Usuario> SearchUserByUsername(string username)
        {
            var users = _dbContext.Usuarios;
            Usuario Result = new Usuario();

            foreach (var user in users)
            {
                if (user.UsuarioUsername == username)
                {
                    Result = user;
                }
            }

            return Result;
        }
    }
}
