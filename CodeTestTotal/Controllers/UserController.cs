using CodeTestTotal.Interfaces;
using CodeTestTotal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestTotal.Controllers
{
    public class UserController : Controller
    {
        private IUserService _IUserService;
        public UserController(IUserService IUserService)
        {
            _IUserService = IUserService;
        }

        public IActionResult Login()
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

            return RedirectToAction("","");
            return View();
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
