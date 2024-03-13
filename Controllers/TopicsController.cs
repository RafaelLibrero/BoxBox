using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class TopicsController : Controller
    {
        private RepositoryBoxBox repo;

        public TopicsController(RepositoryBoxBox repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<VTopic> topics = await this.repo.GetVTopicsAsync();
            return View(topics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(VTopic topic)
        {
            await this.repo.CreateVTopicAsync(topic);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int topicId)
        {
            VTopic topic = await this.repo.FindVTopicAsync(topicId);
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VTopic topic)
        {
            await this.repo.UpdateVTopicAsync(topic);
            return RedirectToAction("Index");
        }
    }
}
