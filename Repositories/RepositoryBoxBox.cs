using BoxBox.Data;
using BoxBox.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

#region Views y Stored Procedures

#region Topics

//CREATE VIEW V_Topics AS
//SELECT
//    t.TopicID,
//    t.Title,
//    t.Description,
//    COUNT(DISTINCT c.ConversationID) AS Conversations,
//    COUNT(p.PostID) AS Posts,
//    COALESCE(MAX(p.PostID), 0) AS LastMessage
//FROM
//    topics t
//LEFT JOIN
//    conversations c ON t.TopicID = c.TopicID
//LEFT JOIN
//    posts p ON c.ConversationID = p.ConversationID
//GROUP BY
//    t.TopicID, t.Title, t.Description;

#endregion

#region Conversations

//CREATE VIEW V_Conversations AS
//SELECT
//    c.ConversationID,
//    c.TopicID,
//    c.UserID,
//    c.Title,
//    c.EntryCount,
//    c.CreatedAt,
//    COUNT(p.PostID) AS PostCount,
//    COALESCE(MAX(p.PostID), 0) AS LastMessage
//FROM
//    Conversations c
//LEFT JOIN
//    Posts p ON c.ConversationID = p.ConversationID
//GROUP BY
//    c.ConversationID, c.UserID, c.TopicID, c.Title, c.EntryCount, c.CreatedAt;

//CREATE PROCEDURE SP_UPDATE_ENTRYCOUNT
//    @ConversationID INT
//AS
//BEGIN
//    UPDATE Conversations
//    SET EntryCount = EntryCount + 1
//    WHERE ConversationID = @ConversationID;
//END;

#endregion

#endregion

namespace BoxBox.Repositories
{
    public class RepositoryBoxBox
    {
        private BoxBoxContext context { get; set; }

        public RepositoryBoxBox(BoxBoxContext context)
        {
            this.context = context;
        }
        #region Users

        public async Task<User> FindUserAsync(int userId)
        {
            return await
                this.context.Users.FirstOrDefaultAsync
                (x => x.UserId == userId);
        }

        #endregion

        #region Topics

        public async Task<List<VTopic>> GetVTopicsAsync()
        {
            return await
                this.context.VTopics.ToListAsync();
        }

        public async Task<VTopic> FindVTopicAsync(int topicId)
        {
            return await
                this.context.VTopics.FirstOrDefaultAsync
                (x => x.TopicId == topicId);
        }

        public async Task CreateVTopicAsync(VTopic tema)
        {
            VTopic topic = new VTopic();
            topic.TopicId = await this.context.VTopics.MaxAsync(x => x.TopicId) + 1;
            topic.Title = tema.Title;
            topic.Description = tema.Description;

            this.context.VTopics.Add(topic);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateVTopicAsync(VTopic tema)
        {
            VTopic topic = await this.FindVTopicAsync(tema.TopicId);
            topic.Title = tema.Title;
            topic.Description = tema.Description;

            await this.context.SaveChangesAsync();
        }

        #endregion

        #region Conversations

        public async Task<List<VConversation>> GetVConversationsTopicAsync(int topicId)
        {
            return await
                this.context.VConversations.Where
                (x => x.TopicId == topicId).ToListAsync();
        }

        public async Task<VConversation> FindVConversationAsync(int conversationId)
        {
            return await
                this.context.VConversations.FirstOrDefaultAsync
                (x => x.ConversationId == conversationId);
        }

        public async Task CreateVConversationAsync(VConversation conversacion)
        {
            VConversation conversation = new VConversation();
            conversation.ConversationId = await this.context.Conversations.MaxAsync(x => x.ConversationId) + 1;
            conversation.TopicId = conversacion.TopicId;
            conversation.UserId = conversacion.UserId;
            conversation.Title = conversacion.Title;
            conversation.CreatedAt = conversation.CreatedAt;

            this.context.VConversations.Add(conversation);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateVConversationAsync(VConversation conversacion)
        {
            VConversation conversation = await this.FindVConversationAsync(conversacion.ConversationId);
            conversation.TopicId = conversacion.TopicId;
            conversation.UserId = conversacion.UserId;
            conversation.Title = conversacion.Title;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteVConversationAsync(int conversationId)
        {
            VConversation conversation = await this.FindVConversationAsync(conversationId);

            this.context.VConversations.Remove(conversation);
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
            post.Title = posteo.Title;
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
            post.Title = posteo.Title;
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