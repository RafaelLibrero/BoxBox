using BoxBox.Filters;
using BoxBox.Helpers;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class DriversController : Controller
    {
        private RepositoryBoxBox repo;
        private HelperPathProvider helperPathProvider;
        private HelperUploadFiles helperUploadFiles;

        public DriversController(RepositoryBoxBox repo, HelperPathProvider helperPathProvider, HelperUploadFiles helperUploadFiles)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
            this.helperUploadFiles = helperUploadFiles;
        }
        public async Task<IActionResult> Index()
        {
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            foreach(Driver driver in drivers)
            {
                driver.Flag = this.helperPathProvider.MapUrlPath(driver.Flag, Folders.Images);
                driver.Imagen = this.helperPathProvider.MapUrlPath(driver.Imagen, Folders.Images);
            }
            foreach(Team team in teams)
            {
                team.Logo = this.helperPathProvider.MapUrlPath(team.Logo, Folders.Images);
            }
            ViewData["TEAMS"] = teams;
            return View(drivers);
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public async Task<IActionResult> Create()
        {
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["TEAMS"] = teams;
            return View();
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        [HttpPost]
        public async Task<IActionResult> Create(Driver driver, IFormFile foto, IFormFile bandera)
        {
            await this.helperUploadFiles.UploadFileAsync(foto, Folders.Images);
            await this.helperUploadFiles.UploadFileAsync(bandera, Folders.Images);
            
            driver.Imagen = foto.FileName;
            driver.Flag = bandera.FileName;

            await this.repo.CreateDriverAsync(driver);

            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public async Task<IActionResult> Edit(int driverId)
        {
            Driver driver = await this.repo.FindDriverAsync(driverId);
            List<Team> teams = await this.repo.GetTeamsAsync();
            ViewData["TEAMS"] = teams;
            ViewData["BANDERA"] = this.helperPathProvider.MapUrlPath(driver.Flag, Folders.Images);
            ViewData["FOTO"] = this.helperPathProvider.MapUrlPath(driver.Imagen, Folders.Images);

            return View(driver);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit(Driver driver, IFormFile foto, IFormFile bandera)
        {
            if (foto != null)
            {
                await this.helperUploadFiles.UploadFileAsync(foto, Folders.Images);
                driver.Imagen = foto.FileName;
            }

            if(bandera != null)
            {
                await this.helperUploadFiles.UploadFileAsync(bandera, Folders.Images);
                driver.Flag = bandera.FileName;
            }

            await this.repo.UpdateDriverAsync(driver);

            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Delete(int driverId)
        {
            await this.repo.DeleteDriverAsync(driverId);
            return RedirectToAction("Index");
        }
    }
}
