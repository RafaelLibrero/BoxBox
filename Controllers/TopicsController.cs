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
            List<User> usuarios = new List<User>();

            foreach (var topic in topics)
            {
                Post lastMessage = await this.repo.FindPostAsync(topic.LastMessage);
                if (lastMessage != null)
                {
                    lastMessages.Add(lastMessage);
                }
            }

            foreach (var lastMessage in lastMessages)
            {
                User usuario = await this.repo.FindUserAsync(lastMessage.UserId);
                if (usuario != null)
                {
                    usuarios.Add(usuario);
                }  
            }
            
            ViewData["LastMessages"] = lastMessages;
            ViewData["Usuarios"] = usuarios;
            return View(topics);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult>Create(Topic topic)
        {
            await this.repo.CreateTopicAsync(topic);
            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit(int topicId)
        {
            Topic topic = await this.repo.FindTopicAsync(topicId);
            return View(topic);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit(Topic topic)
        {
            await this.repo.UpdateTopicAsync(topic);
            return RedirectToAction("Index");
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Delete(int topicId)
        {
            await this.repo.DeleteTopicAsync(topicId);
            return RedirectToAction("Index");
        }

    }
}
