using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeTestTotal.Controllers
{
    public class UserController : Controller
    {
        private IUserService _IUserService;
        private UserManager<Usuario> _UserManager;
        private SignInManager<Usuario> _SignInManager;
        public UserController(IUserService IUserService, UserManager<Usuario> userManager, SignInManager<Usuario> SignInManager)
        {
            _IUserService = IUserService;
            _UserManager = userManager;

            /*handle cookie*/
            _SignInManager = SignInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            /*Validate Modelo*/
            if (!ModelState.IsValid)
            {
                //If the model is not valid
                return View(loginViewModel);
            }

            var result2 = await _SignInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure:false);

            if (result2.Succeeded)
            { 
                //Login succcessful
                //Redirect to Index page
                return RedirectToAction("Index", "Client");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos");
                return View(loginViewModel);
            }

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

                await _SignInManager.SignInAsync(usuario, isPersistent: false);
                
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

        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login");
        }
    }
}
