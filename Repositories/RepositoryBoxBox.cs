using BoxBox.Data;
using BoxBox.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
//    @conversationID INT
//AS
//BEGIN
//    UPDATE Conversations
//    SET EntryCount = EntryCount + 1
//    WHERE ConversationID = @conversationID;
//END;

/*
CREATE OR ALTER PROCEDURE SP_PAGINACION_CONVERSATIONS
(@POSICION INT, @IDTOPIC INT, @REGISTROS INT OUT)
AS
	SELECT @REGISTROS = COUNT(ConversationID)
	FROM V_Conversations
	WHERE TopicID = @IDTOPIC
	SELECT ConversationID, TopicID, UserID, Title, EntryCount, CreatedAt, PostCount, LastMessage
	FROM
	(
		SELECT CAST(ROW_NUMBER() OVER (ORDER BY ConversationID) AS int)
		AS POSICION, ConversationID, TopicID, UserID, Title, EntryCount, CreatedAt, PostCount, LastMessage
		FROM V_Conversations
		WHERE TopicID = @IDTOPIC
	)
	AS QUERY
	where posicion >= @posicion and posicion < (@posicion + 10)
GO
*/

#endregion

#region Posts

/*
CREATE OR ALTER PROCEDURE SP_PAGINACION_POSTS
(@POSICION INT, @IDCONVERSACION INT, @REGISTROS INT OUT)
AS
	SELECT @REGISTROS = COUNT(PostID)
	FROM Posts
	WHERE ConversationID = @IDCONVERSACION
	SELECT PostID, ConversationID, UserID, Text, CreatedAt, Estado
	FROM
	(
		SELECT CAST(ROW_NUMBER() OVER (ORDER BY PostID) AS int)
		AS POSICION, PostID, ConversationID, UserID, Text, CreatedAt, Estado
		FROM Posts
		WHERE ConversationID = @IDCONVERSACION
	)
	AS QUERY
	where posicion >= @posicion and posicion < (@posicion + 10)
GO
*/

#endregion

#endregion

namespace BoxBox.Repositories
{
    public class RepositoryBoxBox : IRepositoryBoxBox
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

        public async Task UpdateUserAsync(User user)
        {
            User usuario = await this.context.Users.FirstOrDefaultAsync
                (x => x.UserId == user.UserId);
            usuario.UserName = user.UserName;
            usuario.ProfilePicture = user.ProfilePicture;
            usuario.TeamId = user.TeamId;
            usuario.DriverId = user.DriverId;

            await this.context.SaveChangesAsync();
        }

        #endregion

        #region Topics

        public async Task<List<VTopic>> GetVTopicsAsync()
        {
            return await
                this.context.VTopics.ToListAsync();
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
            topic.TopicId = await this.context.VTopics.MaxAsync(x => x.TopicId) + 1;
            topic.Title = tema.Title;
            topic.Description = tema.Description;

            this.context.Topics.Add(topic);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateTopicAsync(Topic tema)
        {
            Topic topic = await this.FindTopicAsync(tema.TopicId);
            topic.Title = tema.Title;
            topic.Description = tema.Description;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(int topicId)
        {
            Topic topic = await this.FindTopicAsync(topicId);
            this.context.Topics.Remove(topic);
            await this.context.SaveChangesAsync();
        }

        #endregion

        #region Conversations

        public async Task<ConversationsPaginado> GetVConversationsTopicAsync(int posicion, int topicId)
        {
            string sql = "SP_PAGINACION_CONVERSATIONS @posicion, @idtopic, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamTopicId = new SqlParameter("@idtopic", topicId);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = ParameterDirection.Output;

            var consulta = this.context.VConversations.FromSqlRaw
                (sql, pamPosicion, pamTopicId, pamRegistros);
            var datos = await consulta.ToListAsync();

            return new ConversationsPaginado
            {
                Registros = (int)pamRegistros.Value,
                Conversations = datos
            };
        }

        public async Task<Conversation> FindConversationAsync(int conversationId)
        {
            return await
                this.context.Conversations.FirstOrDefaultAsync
                (x => x.ConversationId == conversationId);
        }

        public async Task<Conversation> CreateConversationAsync(Conversation conversacion)
        {
            Conversation conversation = new Conversation();
            conversation.ConversationId = await this.context.Conversations.MaxAsync(x => x.ConversationId) + 1;
            conversation.TopicId = conversacion.TopicId;
            conversation.UserId = conversacion.UserId;
            conversation.Title = conversacion.Title;
            conversation.CreatedAt = DateTime.UtcNow;

            this.context.Conversations.Add(conversation);
            await this.context.SaveChangesAsync();

            return conversation;
        }

        public async Task UpdateConversationAsync(Conversation conversacion)
        {
            Conversation conversation = await this.FindConversationAsync(conversacion.ConversationId);

            conversation.Title = conversacion.Title;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteConversationAsync(int conversationId)
        {
            Conversation conversation = await this.FindConversationAsync(conversationId);

            this.context.Conversations.Remove(conversation);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEntryCount(int conversationId)
        {
            string sql = "SP_UPDATE_ENTRYCOUNT @conversationID";
            SqlParameter pamId = new SqlParameter("@conversationID", conversationId);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }

        #endregion

        #region Posts

        public async Task<PostsPaginado> GetPostsConversationAsync(int posicion, int conversationId)
        {
            string sql = "SP_PAGINACION_POSTS @posicion, @idconversacion, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamConversationId = new SqlParameter("@idconversacion", conversationId);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = ParameterDirection.Output;

            var consulta = this.context.Posts.FromSqlRaw
                (sql, pamPosicion, pamConversationId, pamRegistros);
            var datos = await consulta.ToListAsync();

            return new PostsPaginado
            {
                Registros = (int)pamRegistros.Value,
                Posts = datos
            };
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
            post.CreatedAt = DateTime.UtcNow;
            post.Estado = 0;

            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post posteo)
        {
            Post post = await this.FindPostAsync(posteo.PostId);
            post.ConversationId = posteo.ConversationId;
            post.UserId = posteo.UserId;
            post.Text = posteo.Text;

            this.context.Posts.Add(post);
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            Post post = await this.FindPostAsync(postId);

            this.context.Posts.Remove(post);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetReportedPosts()
        {
            return await
                this.context.Posts
                .Where(x => x.Estado == 1).ToListAsync();
        }

        public async Task ReportPostAsync(int postId)
        {
            Post post = await this.FindPostAsync(postId);
            post.Estado = 1;

            await this.context.SaveChangesAsync();
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

        public async Task CreateTeamAsync(Team equipo)
        {
            Team team = new Team();
            team.TeamId = await this.context.Teams.MaxAsync(x => x.TeamId) + 1;
            team.TeamName = equipo.TeamName;
            team.Logo = equipo.Logo;

            this.context.Teams.Add(team);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync(Team equipo)
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

        public async Task CreateRaceAsync(Race carrera)
        {
            Race race = new Race();
            race.RaceId = await this.context.Races.MaxAsync(x => x.RaceId) + 1;
            race.RaceName = carrera.RaceName;
            race.Image = carrera.Image;
            race.Location = carrera.Location;
            race.StartDate = carrera.StartDate;
            race.EndDate = carrera.EndDate;

            this.context.Races.Add(race);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateRaceAsync(Race carrera)
        {
            Race race = await this.FindRaceAsync(carrera.RaceId);
            race.RaceName = carrera.RaceName;
            race.Image = carrera.Image;
            race.Location = carrera.Location;
            race.StartDate = carrera.StartDate;
            race.EndDate = carrera.EndDate;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteRaceAsync(int raceId)
        {
            Race race = await this.FindRaceAsync(raceId);

            this.context.Races.Remove(race);
            await this.context.SaveChangesAsync();
        }

        #endregion
    }
}