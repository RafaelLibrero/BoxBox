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

            foreach(Driver driver in drivers)
            {
                driver.Flag = this.helperPathProvider.MapUrlPath(driver.Flag, Folders.Images);
                driver.Imagen = this.helperPathProvider.MapUrlPath(driver.Imagen, Folders.Images);
            }
            return View(drivers);
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Driver driver, IFormFile imagen, IFormFile bandera)
        {
            await this.helperUploadFiles.UploadFileAsync(imagen, Folders.Images);
            await this.helperUploadFiles.UploadFileAsync(bandera, Folders.Images);
            
            driver.Imagen = imagen.FileName;
            driver.Flag = bandera.FileName;

            await this.repo.CreateDriverAsync(driver);

            return View();
        }

        [AuthorizeUsers(Policy = ("ADMIN"))]
        public async Task<IActionResult> Edit(int driverId)
        {
            Driver driver = await this.repo.FindDriverAsync(driverId);

            driver.Flag = this.helperPathProvider.MapUrlPath(driver.Flag, Folders.Images);
            driver.Imagen = this.helperPathProvider.MapUrlPath(driver.Imagen, Folders.Images);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Driver driver, IFormFile imagen, IFormFile bandera)
        {
            await this.helperUploadFiles.UploadFileAsync(imagen, Folders.Images);
            await this.helperUploadFiles.UploadFileAsync(bandera, Folders.Images);

            driver.Imagen = imagen.FileName;
            driver.Flag = bandera.FileName;

            await this.repo.UpdateDriverAsync(driver);

            return View();
        }
    }
}
