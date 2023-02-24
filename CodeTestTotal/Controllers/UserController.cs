using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.Services;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace CodeTestTotal.Controllers
{
    public class UserController : Controller
    {
        private IClientService _IClientService;
        private IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        private SignInManager<Usuario> _SignInManager;
        public UserController(UserManager<Usuario> userManager, SignInManager<Usuario> SignInManager, IClientService IClientService, IUserService IUserService)
        {
            _IUserService = IUserService;
            _IClientService = IClientService;
            _UserManager = userManager;
            /*handle cookie*/
            _SignInManager = SignInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            /*Validate Modelo*/
            if (!ModelState.IsValid)
            {
                //If the model is not valid
                return View(loginViewModel);
            }

            var result = await _SignInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            
            var user = await _IUserService.SearchUserByUsername(loginViewModel.Username.ToUpper());

            var roles = await _UserManager.GetRolesAsync(user);

            var asd = User.IsInRole("Cliente");
            if (result.Succeeded)
            {
                //Login succcessful
                //Redirect to Index page

                if (User.Identity.IsAuthenticated)
                {
                    var claims = User.Claims.ToList();
                    var userID = int.Parse(claims[0].Value);
                    

                    //if (type == "C")
                    //{
                    //    ViewBag.TypeUser = "C";
                    return RedirectToAction("Index", "Client");
                    //}
                    //else
                    //{
                    //    ViewBag.TypeUser = "V";
                    //    return RedirectToAction("Order", "ListOrders");
                    //}

                }

                ModelState.AddModelError(string.Empty, "El usuario no tiene un rol");
                return View(loginViewModel);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos");
                return View(loginViewModel);
            }

        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel oRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(oRegisterViewModel);
            }

            var usuario = new Usuario()
            {
                UsuarioUsername = oRegisterViewModel.Username
            };

            var result = await _UserManager.CreateAsync(usuario, password: oRegisterViewModel.Password);

            //await _roleManager.CreateAsync(new IdentityRole("Cliente"));

            if (result.Succeeded)
            {
                /*Add client*/
                var resultClient = await _IClientService.AddClient(usuario.UsuarioId, oRegisterViewModel.UsuarioNombre);

                if (resultClient)
                {
                    await _SignInManager.SignInAsync(usuario, isPersistent: false);

                    var user = await _IUserService.SearchUserByUsername(usuario.UsuarioUsername.ToUpper());
                    await _UserManager.AddToRoleAsync(user, "Cliente");

                    return RedirectToAction("Index", "Client");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo generar el usuario");
                    return View(oRegisterViewModel);
                }

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(oRegisterViewModel);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login");
        }

    }
}
