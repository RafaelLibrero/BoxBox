using BoxBox.Filters;
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
            
            List<Post> lastMessages = new List<Post>();

            foreach (var topic in topics)
            {
                Post lastMessage = await this.repo.FindPostAsync(topic.LastMessage);
                lastMessages.Add(lastMessage);
            }
            
            ViewData["LastMessages"] = lastMessages;
            return View(topics);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult>Create(VTopic topic)
        {
            await this.repo.CreateVTopicAsync(topic);
            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit(int topicId)
        {
            VTopic topic = await this.repo.FindVTopicAsync(topicId);
            return View(topic);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit(VTopic topic)
        {
            await this.repo.UpdateVTopicAsync(topic);
            return RedirectToAction("Index");
        }
    }
}
