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
        public async Task<IActionResult> Register(User user)
        {
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncriptarContenido(user.Password, user.Salt);
            user.RegistrationDate = DateTime.Today;
            user.LastAccess = DateTime.UtcNow;
            user.RolId = 2;
            user.Estado = 0;
            await this.repo.Register(user);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
