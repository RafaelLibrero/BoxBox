using BoxBox.Filters;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class UsersController : Controller
    {
        private RepositoryBoxBox repo;

        public UsersController(RepositoryBoxBox repo)
        {
            this.repo = repo;
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
        public async Task<IActionResult> EditPerfil(int userId)
        {
            User user = await this.repo.FindUserAsync(userId);
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["DRIVERS"] = drivers;
            ViewData["TEAMS"] = teams;
            return View(user);
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> EditPerfil(User user)
        {
            await this.repo.UpdateUserAsync(user);
            return RedirectToAction("Perfil", user.UserId);
        }
    }
}
