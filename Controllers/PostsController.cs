using BoxBox.Filters;
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

        public async Task<IActionResult>Index(int? posicion, int conversationId)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            PostsPaginado posts = await this.repo.GetPostsConversationAsync(posicion.Value, conversationId);
            ViewData["REGISTROS"] = posts.Registros;
            int siguiente = posicion.Value + 1;
            if (siguiente > posts.Registros)
            {
                
                siguiente = posts.Registros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = posts.Registros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            ViewData["CONVERSATIONID"] = conversationId;
            List<Driver> drivers = await this.repo.GetDriversAsync();
            List<Team> teams = await this.repo.GetTeamsAsync();
            Conversation conversation = await this.repo.FindConversationAsync(conversationId);
            ViewData["DRIVERS"] = drivers;
            ViewData["TEAMS"] = teams;
            ViewData["Title"] = conversation.Title;
            List<User> users = new List<User>();

            foreach (var post in posts.Posts)
            {
                User usuario = await this.repo.FindUserAsync(post.UserId);
                users.Add(usuario);
            }
            ViewData["Usuarios"] = users;

            if (HttpContext.Session.GetString("fromConversations") == "true")
            {
                await this.repo.UpdateEntryCount(conversationId);
                HttpContext.Session.Remove("fromConversations"); 
            }

            return View(posts.Posts);
        }

        [AuthorizeUsers]
        public IActionResult Create(int conversationId)
        {
            ViewData["ConversationId"] = conversationId;
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            await this.repo.CreatePostAsync(post);

            return RedirectToAction("Index", new { conversationId = post.ConversationId });
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Delete(int postId)
        {
            await this.repo.DeleteConversationAsync(postId);
            Post post = await this.repo.FindPostAsync(postId);
            return RedirectToAction("Index", new { conversationId = post.ConversationId });
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> ReportedPosts()
        {
            List<Post> posts = await this.repo.GetReportedPosts();
            return View(posts);
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> ReportPost(int postId)
        {
            await this.repo.ReportPostAsync(postId);
            Post post = await this.repo.FindPostAsync(postId);
            return RedirectToAction("Index", new { conversationId = post.ConversationId });
        }
    }
}
