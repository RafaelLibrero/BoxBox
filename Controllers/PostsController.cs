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
            return View(posts);
        }
    }
}
