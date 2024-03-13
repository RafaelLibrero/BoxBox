using BoxBox.Extensions;
using BoxBox.Helpers;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            if (user != null)
            {
                //SEGURIDAD
                ClaimsIdentity identity =
                new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                //CREAMOS EL CLAIM PARA EL NOMBRE (APELLIDO) 
                Claim claimName =
                    new Claim(ClaimTypes.Name, user.Email);
                
                identity.AddClaim(claimName);

                //COMO POR AHORA NO VOY A UTILIZAR NI SE UTILIZAR ROLES 

                //NO LO INCLUIMOS 

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                //HttpContext.Session.SetObject("USUARIO", user);
                return RedirectToAction("Index", "Topics");

            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Topics");
        }
    }
}
