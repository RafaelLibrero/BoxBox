using BoxBox.Filters;
using BoxBox.Helpers;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class TeamsController : Controller
    {
        private RepositoryBoxBox repo;
        private HelperPathProvider helperPathProvider;
        private HelperUploadFiles helperUploadFiles;

        public TeamsController(RepositoryBoxBox repo, HelperPathProvider helperPathProvider, HelperUploadFiles helperUploadFiles)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
            this.helperUploadFiles = helperUploadFiles;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> teams = await this.repo.GetTeamsAsync();
            List<Driver> drivers = await this.repo.GetDriversAsync();
            foreach (Team team in teams)
            {
                team.Logo = this.helperPathProvider.MapUrlPath(team.Logo, Folders.Images);
            }
            foreach (Driver driver in drivers)
            {
                driver.Flag = this.helperPathProvider.MapUrlPath(driver.Flag, Folders.Images);
                driver.Imagen = this.helperPathProvider.MapUrlPath(driver.Imagen, Folders.Images);
            }
            ViewData["DRIVERS"] = drivers;
            return View(teams);
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(Team team, IFormFile imagen)
        {
            await this.helperUploadFiles.UploadFileAsync(imagen, Folders.Images);

            team.Logo = imagen.FileName;

            await this.repo.CreateTeamAsync(team);

            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public async Task<IActionResult> Edit (int teamId)
        {
            Team team = await this.repo.FindTeamAsync(teamId);

            ViewData["LOGO"] = this.helperPathProvider.MapUrlPath(team.Logo, Folders.Images);

            return View(team);
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        [HttpPost]
        public async Task<IActionResult> Edit(Team team, IFormFile imagen)
        {
            if (imagen != null)
            {
                await this.helperUploadFiles.UploadFileAsync(imagen, Folders.Images);
                team.Logo = imagen.FileName;
            }
            await this.repo.UpdateTeamAsync(team);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int teamId)
        {
            await this.repo.DeleteTeamAsync(teamId);
            return RedirectToAction("Index");
        }
    }
}
