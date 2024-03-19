using BoxBox.Filters;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BoxBox.Helpers;

namespace BoxBox.Controllers
{
    public class UsersController : Controller
    {
        private RepositoryBoxBox repo;
        private HelperUploadFiles helperUploadFiles;

        public UsersController(RepositoryBoxBox repo, HelperUploadFiles helperUploadFiles)
        {
            this.repo = repo;
            this.helperUploadFiles = helperUploadFiles;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Perfil(int userId)
        {
            User user = await this.repo.FindUserAsync(userId);
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["DRIVERS"] = drivers;
            ViewData["TEAMS"] = teams;
            return View(user);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> MiPerfil()
        {
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["DRIVERS"] = drivers;
            ViewData["TEAMS"] = teams;
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> EditPerfil()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = await this.repo.FindUserAsync(userId);
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["DRIVERS"] = drivers;
            ViewData["TEAMS"] = teams;
            return View(user);
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> EditPerfil(User usuario, IFormFile foto)
        {
            if (foto != null)
            {
                await this.helperUploadFiles.UploadFileAsync(foto, Folders.Uploads);
                usuario.ProfilePicture = foto.FileName;
            }

            await this.repo.UpdateUserAsync(usuario);

            User user = await this.repo.FindUserAsync(usuario.UserId);

            ClaimsIdentity identity =
                new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

            Claim claimName =
                    new Claim(ClaimTypes.Name, user.UserName);
            identity.AddClaim(claimName);
            Claim claimEmail =
                new Claim(ClaimTypes.Email, user.Email);
            identity.AddClaim(claimEmail);
            Claim claimId =
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
            identity.AddClaim(claimId);
            Claim claimRol =
                new Claim(ClaimTypes.Role, user.RolId.ToString());
            identity.AddClaim(claimRol);
            Claim claimEquipoFavorito =
                new Claim("EquipoFavorito", user.TeamId.ToString());
            identity.AddClaim(claimEquipoFavorito);
            Claim claimPilotoFavorito =
                new Claim("PilotoFavorito", user.DriverId.ToString());
            identity.AddClaim(claimPilotoFavorito);
            Claim claimFechaRegistro =
                new Claim("FechaRegistro", user.RegistrationDate.ToString());
            identity.AddClaim(claimFechaRegistro);
            Claim claimUltimoAcceso =
                new Claim("UltimoAcceso", user.LastAccess.ToString());
            identity.AddClaim(claimUltimoAcceso);
            Claim claimFotoPerfil =
                new Claim("FotoPerfil", user.ProfilePicture);
            identity.AddClaim(claimFotoPerfil);

            ClaimsPrincipal userPrincipal =
                new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal);

            return RedirectToAction("MiPerfil");
        }
    }
}
