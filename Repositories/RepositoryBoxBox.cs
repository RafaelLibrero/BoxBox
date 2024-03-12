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

        public async Task<Topic> FindTopicAsync(int topicId)
        {
            return await
                this.context.Topics.FirstOrDefaultAsync
                (x => x.TopicId == topicId);
        }

        public async Task CreateTopicAsync(Topic tema)
        {
            Topic topic = new Topic();
            topic.TopicId = await this.context.Topics.MaxAsync(x => x.TopicId) + 1;
            topic.Title = tema.Title;

            this.context.Topics.Add(topic);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateTopicAsync(Topic tema)
        {
            Topic topic = await this.FindTopicAsync(tema.TopicId);
            topic.Title = tema.Title;

            await this.context.SaveChangesAsync();
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

        public async Task CreateConversationAsync(Conversation conversacion)
        {
            Conversation conversation = new Conversation();
            conversation.ConversationId = await this.context.Conversations.MaxAsync(x => x.ConversationId) + 1;
            conversation.TopicId = conversacion.TopicId;
            conversation.UserId = conversacion.UserId;
            conversation.Title = conversacion.Title;
            conversation.CreatedAt = conversation.CreatedAt;

            this.context.Conversations.Add(conversation);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateConversationAsync(Conversation conversacion)
        {
            Conversation conversation = await this.FindConversationAsync(conversacion.ConversationId);
            conversation.TopicId = conversacion.TopicId;
            conversation.UserId = conversacion.UserId;
            conversation.Title = conversacion.Title;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteConversationAsync(int conversationId)
        {
            Conversation conversation = await this.FindConversationAsync(conversationId);

            this.context.Conversations.Remove(conversation);
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

        public async Task<Post> FindPostAsync(int postId)
        {
            return await
                this.context.Posts.FirstOrDefaultAsync
                (x => x.PostId == postId);
        }

        public async Task CreatePostAsync(Post posteo)
        {
            Post post = new Post();
            post.PostId = await this.context.Posts.MaxAsync(x => x.PostId) + 1;
            post.ConversationId = posteo.ConversationId;
            post.UserId = posteo.UserId;
            post.Text = posteo.Text;
            post.CreatedAt = posteo.CreatedAt;

            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post posteo)
        {
            Post post = await this.FindPostAsync(posteo.PostId);
            post.ConversationId = posteo.ConversationId;
            post.UserId = posteo.UserId;
            post.Text = posteo.Text;
            post.CreatedAt = posteo.CreatedAt;

            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            Post post = await this.FindPostAsync(postId);

            this.context.Posts.Remove(post);
        }

        #endregion

        #region Drivers

        public async Task<List<Driver>> GetDriversAsync()
        {
            return await
                this.context.Drivers.ToListAsync();
        }

        public async Task<Driver> FindDriverAsync(int driverId)
        {
            return await
                this.context.Drivers.FirstOrDefaultAsync
                (x => x.DriverID == driverId);
        }

        public async Task CreateDriverAsync(Driver conductor)
        {
            Driver driver = new Driver();
            driver.DriverID = await this.context.Drivers.MaxAsync(x => x.DriverID) + 1;
            driver.DriverName = conductor.DriverName;
            driver.CarNumber = conductor.CarNumber;
            driver.TeamID = conductor.TeamID;
            driver.Flag = conductor.Flag;
            driver.Imagen = conductor.Imagen;

            this.context.Drivers.Add(driver);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDriverAsync(Driver conductor)
        {
            Driver driver = await this.FindDriverAsync(conductor.DriverID);
            driver.DriverName = conductor.DriverName;
            driver.CarNumber = conductor.CarNumber;
            driver.TeamID = conductor.TeamID;
            driver.Flag = conductor.Flag;
            driver.Imagen = conductor.Imagen;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDriverAsync(int driverId)
        {
            Driver driver = await this.FindDriverAsync(driverId);

            this.context.Drivers.Remove(driver);
            await this.context.SaveChangesAsync();
        }

        #endregion

        #region Teams

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await
                this.context.Teams.ToListAsync();
        }

        public async Task<Team> FindTeamAsync(int teamId)
        {
            return await 
                this.context.Teams.FirstOrDefaultAsync
                (x => x.TeamId == teamId);
        }

        public async Task CreateTeamAsync (Team equipo)
        {
            Team team = new Team();
            team.TeamId = await this.context.Teams.MaxAsync(x => x.TeamId) + 1;
            team.TeamName = equipo.TeamName;
            team.Logo = equipo.Logo;

            this.context.Teams.Add(team);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync (Team equipo)
        {
            Team team = await this.FindTeamAsync(equipo.TeamId);
            team.TeamName = equipo.TeamName;
            team.Logo = equipo.Logo;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            Team team = await this.FindTeamAsync(teamId);

            this.context.Teams.Remove(team);
            await this.context.SaveChangesAsync();
        }

        #endregion

        #region Races

        public async Task<List<Race>> GetRacesAsync()
        {
            return await
                this.context.Races.ToListAsync();
        }

        public async Task<Race> FindRaceAsync(int raceId)
        {
            return await
                this.context.Races.FirstOrDefaultAsync
                (x => x.RaceId == raceId);
        }

        public async Task CreateRaceAsync (Race carrera)
        {
            Race race = new Race();
            race.RaceId = await this.context.Races.MaxAsync(x => x.RaceId) + 1;
            race.RaceName = carrera.RaceName;
            race.Location = carrera.Location;
            race.StartDate = carrera.StartDate;
            race.EndDate = carrera.EndDate;

            this.context.Races.Add(race);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateRaceAsync (Race carrera)
        {
            Race race = await this.FindRaceAsync(carrera.RaceId);
            race.RaceName = carrera.RaceName;
            race.Location = carrera.Location;
            race.StartDate = carrera.StartDate;
            race.EndDate = carrera.EndDate;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteRaceAsync (int raceId)
        {
            Race race = await this.FindRaceAsync(raceId);

            this.context.Races.Remove(race);
            await this.context.SaveChangesAsync();
        }

        #endregion
    }
}