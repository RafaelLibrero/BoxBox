using BoxBox.Models;
using BoxBox.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoxBox.Controllers
{
    public class PostsController : Controller
    {
        private RepositoryBoxBox repo;

        public PostsController(RepositoryBoxBox repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult>Index(int conversationId)
        {
            List<Post> posts = await this.repo.GetPostsConversationAsync(conversationId);

            List<User> users = new List<User>();

            foreach (var post in posts)
            {
                User usuario = await this.repo.FindUserAsync(post.UserId);
                users.Add(usuario);
            }

            ViewData["Usuarios"] = users;

            await this.repo.UpdateEntryCount(conversationId);

            return View(posts);
        }
    }
}
