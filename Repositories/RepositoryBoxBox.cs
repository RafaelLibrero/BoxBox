using BoxBox.Data;
using BoxBox.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxBox.Repositories
{
    public class RepositoryBoxBox
    {
        private BoxBoxContext context { get; set; }

        public RepositoryBoxBox(BoxBoxContext context)
        {
            this.context = context;
        }

        #region Topics

        public async Task<List<Topic>> GetTopicsAsync()
        {
            return await
                this.context.Topics.ToListAsync();
        }

        public async Task<Topic> FindForumAsync(int topicId)
        {
            return await
                this.context.Topics.FirstOrDefaultAsync
                (x => x.TopicId == topicId);
        }

        #endregion

        #region Conversations

        public async Task<List<Conversation>> GetConversationsForumAsync(int topicId)
        {
            return await
                this.context.Conversations.Where
                (x => x.TopicId == topicId).ToListAsync();
        }

        public async Task<Conversation> FindConversationAsync(int conversationId)
        {
            return await
                this.context.Conversations.FirstOrDefaultAsync
                (x => x.ConversationId == conversationId);
        }

        #endregion

        #region Posts

        public async Task<List<Post>> GetPostsConversationAsync(int conversationId)
        {
            return await
                this.context.Posts.Where
                (x => x.ConversationId == conversationId)
                .ToListAsync();
        }
    }
}
