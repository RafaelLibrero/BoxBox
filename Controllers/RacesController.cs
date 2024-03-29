﻿using BoxBox.Filters;
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
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
           
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
            ViewData["TEAMS"] = teams;

            foreach (Race race in races)
            {
                race.Image = this.helperPathProvider.MapUrlPath(race.Image, Folders.Images);
            }

            return View(races);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Create()
        {
            List<Driver> drivers = await this.repo.GetDriversAsync();
            ViewData["DRIVERS"] = drivers;
            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(Race race, IFormFile circuit)
        {
            await this.helperUploadFiles.UploadFileAsync(circuit, Folders.Images);

            race.Image = circuit.FileName;

            await this.repo.CreateRaceAsync(race);

            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit(int raceId)
        {
            Race race = await this.repo.FindRaceAsync(raceId);
            List<Driver> drivers = await this.repo.GetDriversAsync();
            ViewData["DRIVERS"] = drivers;

            ViewData["CIRCUIT"] = this.helperPathProvider.MapUrlPath(race.Image, Folders.Images);

            return View(race);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit(Race race, IFormFile circuit)
        {
            if (circuit != null)
            {
                await this.helperUploadFiles.UploadFileAsync(circuit, Folders.Images);
                race.Image = circuit.FileName;
            }
            
            await this.repo.UpdateRaceAsync(race);

            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Delete(int raceId)
        {
            await this.repo.DeleteRaceAsync(raceId);
            return RedirectToAction("Index");
        }
    }
}
