﻿using BoxBox.Filters;
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
        public async Task<IActionResult> Index(int? posicion, int topicId)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            
            ConversationsPaginado conversations = await this.repo.GetVConversationsTopicAsync(posicion.Value, topicId);
            Topic topic = await this.repo.FindTopicAsync(topicId);
            List<User> users = new List<User>();
            List<User> usuarios = new List<User>();
            List<Post> lastMessages = new List<Post>();
            ViewData["REGISTROS"] = conversations.Registros;
            int siguiente = posicion.Value + 1;
            if (siguiente > conversations.Registros)
            {

                siguiente = conversations.Registros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = conversations.Registros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            ViewData["TOPICID"] = topicId;
            foreach (var conversation in conversations.Conversations)
            {
                User usuario = await this.repo.FindUserAsync(conversation.UserId);
                users.Add(usuario);
                Post lastMessage = await this.repo.FindPostAsync(conversation.LastMessage);
                lastMessages.Add(lastMessage);
            }

            foreach (var lastMessage in lastMessages)
            {
                if (lastMessage != null)
                {
                    User usuario = await this.repo.FindUserAsync(lastMessage.UserId);
                    if (usuario != null)
                    {
                        usuarios.Add(usuario);
                    }
                }
                
            }
            ViewData["Title"] = topic.Title;
            ViewData["UsuariosConversation"] = users;
            ViewData["Usuarios"] = usuarios;
            ViewData["LastMessages"] = lastMessages;

            HttpContext.Session.SetString("fromConversations", "true");

            return View(conversations.Conversations);
        }

        [AuthorizeUsers]
        public IActionResult Create(int topicId)
        {
            ViewData["TopicId"] = topicId;
            return View();
        }

        [AuthorizeUsers]
        [HttpPost]
        public async Task<IActionResult> Create(Conversation conversation)
        { 
            Conversation conversacion = await this.repo.CreateConversationAsync(conversation);

            return RedirectToAction("Create", "Posts" , new { conversationId = conversacion.ConversationId });
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        public async Task<IActionResult> Edit (int conversationId)
        {
            Conversation conversation = await this.repo.FindConversationAsync(conversationId);
            return View(conversation);
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit (Conversation conversation)
        {
            await this.repo.UpdateConversationAsync(conversation);
            return RedirectToAction("Index", new { topicId = conversation.TopicId });
        }

        [AuthorizeUsers(Policy = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Delete (int conversationId)
        {
            Conversation conversation = await this.repo.FindConversationAsync(conversationId);
            await this.repo.DeleteConversationAsync(conversationId);
            return RedirectToAction("Index", new { topicId = conversation.TopicId });
        }
    }
}
