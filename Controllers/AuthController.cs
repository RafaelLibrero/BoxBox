﻿using BoxBox.Extensions;
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
 
                Claim claimName =
                    new Claim(ClaimTypes.Name, user.Email);
                identity.AddClaim(claimName);
                Claim claimId =
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
                identity.AddClaim(claimId);
                Claim claimRol = 
                    new Claim(ClaimTypes.Role, user.RolId.ToString());
                identity.AddClaim(claimRol);

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                if (TempData["queryString"] != null)
                {
                    var queryString = TempData["queryString"].ToString();

                    var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString);

                    // Crear un objeto anónimo para almacenar los parámetros
                    var routeParams = new Dictionary<string, string>();
                    foreach (var param in queryParams)
                    {
                        // Agregar cada parámetro al objeto anónimo
                        routeParams[param.Key] = param.Value;
                    }

                    return RedirectToAction(action, controller, routeParams);
                }

                return RedirectToAction(action, controller);
                //HttpContext.Session.SetObject("USUARIO", user);

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

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
