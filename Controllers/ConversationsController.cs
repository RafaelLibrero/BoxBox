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

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(int topicId)
        {
            List<VConversation> conversations = await this.repo.GetVConversationsTopicAsync(topicId);

            List<User> users = new List<User>();
            List<Post> lastMessages = new List<Post>();

            foreach (var conversation in conversations)
            {
                User usuario = await this.repo.FindUserAsync(conversation.UserId);
                users.Add(usuario);
                Post lastMessage = await this.repo.FindPostAsync(conversation.LastMessage);
                lastMessages.Add(lastMessage);
            }

            ViewData["Usuarios"] = users;
            ViewData["LastMessages"] = lastMessages;

            return View(conversations);
        }
    }
}
