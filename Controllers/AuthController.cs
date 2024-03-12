using BoxBox.Extensions;
using BoxBox.Helpers;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class AuthController : Controller
    {
        private RepositoryAuth repo;
        
        public AuthController(RepositoryAuth repo)
        {
            this.repo = repo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string email, string password)
        {
            User user = await this.repo.Register(userName, email, password);
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await this.repo.LoginUserAsync(email, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                HttpContext.Session.SetObject("USUARIO", user);
                ViewData["MENSAJE"] = "Bienvenido";
                return View();
            }
        }
    }
}
