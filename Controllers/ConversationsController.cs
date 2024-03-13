using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class ConversationsController : Controller
    {
        private RepositoryBoxBox repo;

        public ConversationsController(RepositoryBoxBox repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index(int topicId)
        {
            List<VConversation> conversations = await this.repo.GetVConversationsTopicAsync(topicId);
            return View(conversations);
        }
    }
}
