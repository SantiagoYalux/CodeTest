using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeTestTotal.Controllers
{
    public class UserController : Controller
    {
        private IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        public UserController(IUserService IUserService, UserManager<Usuario> userManager)
        {
            _IUserService = IUserService;
            _UserManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
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


            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Client");
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            /*Validate ViewModel*/
            if (!ModelState.IsValid)
            {
                //If the model is not valid
                return View(loginViewModel);
            }

            var resultado = await _IUserService.Login(loginViewModel.Username, loginViewModel.Password);

            if (resultado)
            {
                //Redirect to Index page
                return RedirectToAction("Index", "Client");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos");
                return View(loginViewModel);
            }
        }
    }
}
