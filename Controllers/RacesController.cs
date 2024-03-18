using BoxBox.Filters;
using BoxBox.Helpers;
using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class RacesController : Controller
    {
        private RepositoryBoxBox repo;
        private HelperPathProvider helperPathProvider;
        private HelperUploadFiles helperUploadFiles;

        public RacesController(RepositoryBoxBox repo, HelperPathProvider helperPathProvider, HelperUploadFiles helperUploadFiles)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
            this.helperUploadFiles = helperUploadFiles;
        }

        public async Task<IActionResult> Index()
        {
            List<Race> races = await this.repo.GetRacesAsync();

            foreach(Race race in races)
            {
                race.Image = this.helperPathProvider.MapUrlPath(race.Image, Folders.Images);
            }

            return View(races);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(Race race, IFormFile image)
        {
            await this.helperUploadFiles.UploadFileAsync(image, Folders.Images);

            race.Image = image.FileName;

            await this.repo.CreateRaceAsync(race);

            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit(int raceId)
        {
            Race race = await this.repo.FindRaceAsync(raceId);

            race.Image = this.helperPathProvider.MapUrlPath(race.Image, Folders.Images);

            return View(race);
        }

        [HttpPost]
        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit(Race race, IFormFile image)
        {
            await this.helperUploadFiles.UploadFileAsync(image, Folders.Images);

            race.Image = image.FileName;

            await this.repo.UpdateRaceAsync(race);

            return View();
        }
    }
}
