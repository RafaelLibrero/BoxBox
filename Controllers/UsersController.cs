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
            return View(user);
        }
    }
}
