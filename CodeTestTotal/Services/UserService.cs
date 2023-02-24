using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
                if (user.UsuarioUsernameNormalizado == username)
                {
                    Result = user;
                }
            }

            return Result;
        }


        public async Task<bool> AddRoleUser(Usuario user, string roleName)
        {
            int result = 0;
            UsuarioRol newUserRol = new UsuarioRol();
            newUserRol.UsuarioRolID = await _dbContext.GetLastId(newUserRol) + 1;
            newUserRol.UsuarioRolNombre = roleName;
            newUserRol.UsuarioRolUsuarioId = user.UsuarioId;

            bool resulta = await _dbContext.AddNewRegister(newUserRol);

            return resulta;
        } 
        public async Task<List<string>> GetUserRoles(Usuario user)
        {
            var usersRoles = _dbContext.UsuarioRoles;
            List<string> userRolesNames = new List<string>();

            foreach (var rolUser in usersRoles)
            {
                if (rolUser.UsuarioRolUsuarioId == user.UsuarioId)
                {
                    userRolesNames.Add(rolUser.UsuarioRolNombre);
                }
            }

            return userRolesNames;
        }
        public async Task<List<Usuario>> GetUsersInRole(string RoleName)
        {
            var usersRoles = _dbContext.UsuarioRoles;
            var listUsers = _dbContext.Usuarios;
            List<Usuario> usuarios = new List<Usuario>();

            foreach (var rolUser in usersRoles)
            {
                if (rolUser.UsuarioRolNombre == RoleName)
                {
                    usuarios.Add(listUsers.Where(x => x.UsuarioId == rolUser.UsuarioRolUsuarioId).First());
                }
            }

            return usuarios;
        }

        public async Task<Usuario> GetUserById(int userID)
        {
            return _dbContext.Usuarios.FirstOrDefault(x => x.UsuarioId == userID);
        }
    }
}
