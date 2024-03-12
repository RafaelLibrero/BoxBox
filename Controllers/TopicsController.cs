using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class TopicsController : Controller
    {
        private RepositoryBoxBox repo;

        public async Task<IActionResult> Index()
        {
            List<Topic> topics = await this.repo.GetTopicsAsync();
            return View(topics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(Topic topic)
        {
            await this.repo.CreateTopicAsync(topic);
            return View();
        }
    }
}
