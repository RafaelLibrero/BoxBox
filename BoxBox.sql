USE [master]
GO
/****** Object:  Database [BOXBOX]    Script Date: 20/03/2024 9:38:39 ******/
CREATE DATABASE [BOXBOX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BOXBOX', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BOXBOX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BOXBOX_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BOXBOX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BOXBOX] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BOXBOX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BOXBOX] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BOXBOX] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BOXBOX] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BOXBOX] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BOXBOX] SET ARITHABORT OFF 
GO
ALTER DATABASE [BOXBOX] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BOXBOX] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BOXBOX] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BOXBOX] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BOXBOX] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BOXBOX] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BOXBOX] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BOXBOX] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BOXBOX] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BOXBOX] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BOXBOX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BOXBOX] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BOXBOX] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BOXBOX] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BOXBOX] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BOXBOX] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BOXBOX] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BOXBOX] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BOXBOX] SET  MULTI_USER 
GO
ALTER DATABASE [BOXBOX] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BOXBOX] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BOXBOX] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BOXBOX] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BOXBOX] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BOXBOX] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BOXBOX] SET QUERY_STORE = OFF
GO
USE [BOXBOX]
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[ConversationID] [int] NOT NULL,
	[TopicID] [int] NOT NULL,
	[UserID] [int] NULL,
	[Title] [varchar](255) NULL,
	[EntryCount] [int] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Conversations__688356E4A9DB70AC] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] NOT NULL,
	[ConversationID] [int] NOT NULL,
	[UserID] [int] NULL,
	[Text] [text] NULL,
	[CreatedAt] [datetime] NULL,
	[Estado] [int] NULL,
 CONSTRAINT [PK__Posts__AA126038AB9243BE] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[TopicID] [int] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK__Topics__E210AC4F2F7AF7E2] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_Topics]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Topics] AS
SELECT
    t.TopicID,
    t.Title,
    t.Description,
    COUNT(DISTINCT c.ConversationID) AS Conversations,
    COUNT(p.PostID) AS Posts,
    COALESCE(MAX(p.PostID), 0) AS LastMessage
FROM
    topics t
LEFT JOIN
    conversations c ON t.TopicID = c.TopicID
LEFT JOIN
    posts p ON c.ConversationID = p.ConversationID
GROUP BY
    t.TopicID, t.Title, t.Description;



GO
/****** Object:  View [dbo].[V_Conversations]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_Conversations] AS
SELECT
    c.ConversationID,
    c.TopicID,
	c.UserID,
    c.Title,
    c.EntryCount,
	c.CreatedAt,
    COUNT(p.PostID) AS PostCount,
    COALESCE(MAX(p.PostID), 0) AS LastMessage
FROM
    Conversations c
LEFT JOIN
    Posts p ON c.ConversationID = p.ConversationID
GROUP BY
    c.ConversationID, c.UserID, c.TopicID, c.Title, c.EntryCount, c.CreatedAt;
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] NOT NULL,
	[DriverName] [varchar](255) NULL,
	[CarNumber] [int] NULL,
	[TeamID] [int] NULL,
	[Flag] [nvarchar](max) NULL,
	[Imagen] [nvarchar](max) NULL,
 CONSTRAINT [PK__Drivers__F1B1CD24523488AD] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Races]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Races](
	[RaceID] [int] NOT NULL,
	[RaceName] [varchar](255) NULL,
	[Image] [varchar](255) NULL,
	[Location] [varchar](255) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[WinnerDriverID] [int] NULL,
 CONSTRAINT [PK__Races__05FBD6D4D2CB7E4A] PRIMARY KEY CLUSTERED 
(
	[RaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RolID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[TeamID] [int] NOT NULL,
	[TeamName] [varchar](255) NULL,
	[Logo] [varchar](max) NULL,
 CONSTRAINT [PK__Teams__123AE7B9BBB98490] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[UserName] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varbinary](max) NOT NULL,
	[RegistrationDate] [date] NULL,
	[LastAccess] [datetime2](7) NULL,
	[RolID] [int] NULL,
	[ProfilePicture] [varchar](max) NULL,
	[TotalPosts] [int] NULL,
	[TeamID] [int] NULL,
	[DriverID] [int] NULL,
	[Salt] [nvarchar](50) NULL,
 CONSTRAINT [PK__Users__1788CCAC4E67A637] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (3, 3, 1, N'Test3', 2, CAST(N'2023-08-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (4, 4, 1, N'Test4', 2, CAST(N'2023-08-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (5, 5, 1, N'Test5', 0, CAST(N'2023-08-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (6, 1, 2, N'Fernando Alonso a RedBull?', 34, CAST(N'2024-03-19T09:12:48.707' AS DateTime))
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (7, 1, 3, N'Horner a la calle', 4, CAST(N'2024-03-19T10:24:03.920' AS DateTime))
INSERT [dbo].[Conversations] ([ConversationID], [TopicID], [UserID], [Title], [EntryCount], [CreatedAt]) VALUES (8, 2, 2, N'Estrategia para Australia', 2, CAST(N'2024-03-19T10:45:17.077' AS DateTime))
GO
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (1, N'Max Verstappen', 1, 1, N'netherlands.png', N'max-verstappen-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (2, N'Sergio Perez', 11, 1, N'mexico.png', N'sergio-perez-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (3, N'George Russell', 63, 2, N'united_kingdomgreat_britain.png', N'george-russell-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (4, N'Lewis Hamilton', 44, 2, N'united_kingdomgreat_britain.png', N'lewis-hamilton-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (5, N'Charles Leclerc', 16, 3, N'monaco.png', N'charles-leclerc-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (6, N'Carlos Sainz', 55, 3, N'spain.png', N'carlos-sainz-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (7, N'Oscar Piastri', 81, 4, N'australia.png', N'oscar-piastri-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (8, N'Lando Norris', 4, 4, N'united_kingdomgreat_britain.png', N'lando-norris-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (9, N'Lance Stroll', 18, 5, N'canada.png', N'lance-stroll-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (10, N'Fernando Alonso', 14, 5, N'spain.png', N'fernando-alonso-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (11, N'Esteban Ocon', 31, 6, N'france.png', N'esteban-ocon-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (12, N'Pierre Gasly', 10, 6, N'france.png', N'pierre-gsly-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (13, N'Alexander Albon', 23, 7, N'thailand.png', N'alexander-albon-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (14, N'Logan Sargeant', 2, 7, N'united_states_of_americausa.png', N'logan-sargeant-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (15, N'Daniel Ricciardo', 3, 8, N'australia.png', N'daniel-ricciardo-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (16, N'Yuki Tsunoda', 22, 8, N'japan.png', N'yuki-tsunoda-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (17, N'Valtteri Bottas', 77, 9, N'finland.png', N'valtteri-bottas-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (18, N'Zhou Guanyu', 24, 9, N'china.png', N'guanyu-zhou-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (19, N'Kevin Magnussen', 20, 10, N'denmark.png', N'kevin-magnussen-2024.png')
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag], [Imagen]) VALUES (20, N'Nico Hulkenberg', 27, 10, N'germany.png', N'nico-hulkenberg-2024.png')
GO
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (3, 3, 1, N'Testeo3', CAST(N'2024-08-07T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (4, 4, 1, N'Testeo4', CAST(N'2024-08-08T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (6, 6, 2, N'Pues eso, que pensais? Se atrevera RedBull?', CAST(N'2024-03-19T09:17:22.603' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (7, 6, 3, N'Tus ganas', CAST(N'2024-03-19T09:59:53.613' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (8, 7, 3, N'Me cae mal.', CAST(N'2024-03-19T10:32:12.583' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (9, 7, 2, N'Se viene mi puesto en RedBull', CAST(N'2024-03-19T10:33:15.330' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (10, 8, 2, N'Ayudadme a conseguir la 33', CAST(N'2024-03-19T10:45:26.143' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (11, 8, 3, N'Como no me pincheis las ruedas lo veo complicado...', CAST(N'2024-03-19T10:46:32.960' AS DateTime), 0)
INSERT [dbo].[Posts] ([PostID], [ConversationID], [UserID], [Text], [CreatedAt], [Estado]) VALUES (12, 6, 4, N'Ese puesto es mío, feos.', CAST(N'2024-03-20T08:07:44.690' AS DateTime), 0)
GO
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (1, N'FORMULA 1 GULF AIR BAHRAIN GRAND PRIX 2024', N'sakhir.jpg', N'Bahrein', CAST(N'2024-02-29' AS Date), CAST(N'2024-03-02' AS Date), 1)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (2, N'FORMULA 1 STC SAUDI ARABIAN GRAND PRIX 2024', N'jeddah.jpg', N'Arabia Saudí', CAST(N'2024-03-07' AS Date), CAST(N'2024-03-09' AS Date), 1)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (3, N'FORMULA 1 ROLEX AUSTRALIAN GRAND PRIX 2024', N'albert-park.jpg', N'Australia', CAST(N'2024-03-22' AS Date), CAST(N'2024-03-24' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (4, N'FORMULA 1 MSC CRUISES JAPANESE GRAND PRIX 2024', N'suzuka.jpg', N'Japón', CAST(N'2024-04-05' AS Date), CAST(N'2024-04-07' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (5, N'FORMULA 1 LENOVO CHINESE GRAND PRIX 2024', N'shanghai.jpg', N'China', CAST(N'2024-04-19' AS Date), CAST(N'2024-04-21' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (6, N'FORMULA 1 CRYPTO.COM MIAMI GRAND PRIX 2024', N'hard-rock-stadium.jpg', N'Estados Unidos', CAST(N'2024-05-03' AS Date), CAST(N'2024-05-05' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (7, N'FORMULA 1 MSC CRUISES GRAN PREMIO DELL''EMILIA-ROMAGNA 2024', N'imola.jpg', N'Italia', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-19' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (8, N'FORMULA 1 GRAND PRIX DE MONACO 2024', N'montecarlo.jpg', N'Monaco', CAST(N'2024-05-24' AS Date), CAST(N'2024-05-26' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (9, N'FORMULA 1 AWS GRAND PRIX DU CANADA 2024', N'gilles-villeneuve.jpg', N'Canada', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-09' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (10, N'FORMULA 1 ARAMCO GRAN PREMIO DE ESPAÑA 2024', N'catalunya.jpg', N'España', CAST(N'2024-06-21' AS Date), CAST(N'2024-06-23' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (11, N'FORMULA 1 QATAR AIRWAYS AUSTRIAN GRAND PRIX 2024', N'red-bull-ring.jpg', N'Austria', CAST(N'2024-06-28' AS Date), CAST(N'2024-06-30' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (12, N'FORMULA 1 QATAR AIRWAYS BRITISH GRAND PRIX 2024', N'silverstone.jpg', N'Gran Bretaña', CAST(N'2024-07-05' AS Date), CAST(N'2024-07-07' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (13, N'FORMULA 1 HUNGARIAN GRAND PRIX 2024', N'hungaroring.jpg', N'Hungría', CAST(N'2024-07-19' AS Date), CAST(N'2024-07-21' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (14, N'FORMULA 1 ROLEX BELGIAN GRAND PRIX 2024', N'spa-francorchamps.jpg', N'Bélgica', CAST(N'2024-07-26' AS Date), CAST(N'2024-07-28' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (15, N'FORMULA 1 HEINEKEN DUTCH GRAND PRIX 2024', N'zandvoort.jpg', N'Países Bajos', CAST(N'2024-08-23' AS Date), CAST(N'2024-08-25' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (16, N'FORMULA 1 PIRELLI GRAN PREMIO D’ITALIA 2024', N'monza.jpg', N'Italia', CAST(N'2024-08-30' AS Date), CAST(N'2024-09-01' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (17, N'FORMULA 1 QATAR AIRWAYS AZERBAIJAN GRAND PRIX 2024', N'baku.jpg', N'Azerbaiyán', CAST(N'2024-09-13' AS Date), CAST(N'2024-09-15' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (18, N'FORMULA 1 SINGAPORE AIRLINES SINGAPORE GRAND PRIX 2024', N'marina-bay.jpg', N'Singapur', CAST(N'2024-09-20' AS Date), CAST(N'2024-09-22' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (19, N'FORMULA 1 PIRELLI UNITED STATES GRAND PRIX 2024', N'las-americas.jpg', N'Estados Unidos', CAST(N'2024-10-18' AS Date), CAST(N'2024-10-20' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (20, N'FORMULA 1 GRAN PREMIO DE LA CIUDAD DE MÉXICO 2024', N'hermanos-rodriguez.jpg', N'México', CAST(N'2024-10-25' AS Date), CAST(N'2024-10-27' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (21, N'FORMULA 1 LENOVO GRANDE PRÊMIO DE SÃO PAULO 2024', N'interlagos.jpg', N'Brasil', CAST(N'2024-11-01' AS Date), CAST(N'2024-11-03' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (22, N'FORMULA 1 HEINEKEN SILVER LAS VEGAS GRAND PRIX 2024', N'las-vegas.jpg', N'Estados Unidos', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-23' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (23, N'FORMULA 1 QATAR AIRWAYS QATAR GRAND PRIX 2024', N'losail.jpg', N'Qatar', CAST(N'2024-11-29' AS Date), CAST(N'2024-12-01' AS Date), NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Image], [Location], [StartDate], [EndDate], [WinnerDriverID]) VALUES (24, N'FORMULA 1 ETIHAD AIRWAYS ABU DHABI GRAND PRIX 2024', N'abu-dhabi.jpg', N'Abu Dhabi', CAST(N'2024-12-06' AS Date), CAST(N'2024-12-08' AS Date), NULL)
GO
INSERT [dbo].[Roles] ([RolID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RolID], [Name]) VALUES (2, N'Usuario')
GO
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (1, N'Red Bull Racing', N'redbull-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (2, N'Mercedes', N'mercedes-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (3, N'Ferrari', N'ferrari-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (4, N'McLaren', N'mclaren-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (5, N'Aston Martin', N'aston-martin-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (6, N'Alpine', N'alpine-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (7, N'Williams', N'williams-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (8, N'RB', N'visa-rb-2024.png')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (9, N'Kick Sauber', N'stake-2024.jpeg')
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (10, N'Haas F1 Team', N'haas-2024.png')
GO
INSERT [dbo].[Topics] ([TopicID], [Title], [Description]) VALUES (1, N'Noticias', N'Últimas noticias, actualizaciones y eventos relacionados con la Fórmula 1.')
INSERT [dbo].[Topics] ([TopicID], [Title], [Description]) VALUES (2, N'Análisis y Carreras', N'Estrategias, momentos destacados, y rendimiento de los equipos y pilotos.')
INSERT [dbo].[Topics] ([TopicID], [Title], [Description]) VALUES (3, N'Técnica y Desarrollo', N'Avances en diseño, aerodinámica, y desarrollos técnicos')
INSERT [dbo].[Topics] ([TopicID], [Title], [Description]) VALUES (4, N'Off-Topic', N'Un espacio más relajado para conversar sobre temas no relacionados directamente con la Fórmula 1.')
INSERT [dbo].[Topics] ([TopicID], [Title], [Description]) VALUES (5, N'Sugerencias', N'Feedback sobre la administración y el funcionamiento del foro.')
GO
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [RegistrationDate], [LastAccess], [RolID], [ProfilePicture], [TotalPosts], [TeamID], [DriverID], [Salt]) VALUES (1, N'admin', N'admin@gmail.com', 0xDBEAC0AC4FCC180A1ACD6A4613855D540BA3B268AB067B46D213D686B13DB613D5BA93814E0D3E7FF7470659EA647E3434FD0F7E2DD2E3470D8308C5FB295694, CAST(N'2024-03-11' AS Date), CAST(N'2024-03-20T08:01:45.6397518' AS DateTime2), 1, N'default.png', 0, 3, 20, N'SBrLI\UPpdWt]bfIdWh]rOZqUgAdSZInL`fJl`NSG^JZkqc`F_')
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [RegistrationDate], [LastAccess], [RolID], [ProfilePicture], [TotalPosts], [TeamID], [DriverID], [Salt]) VALUES (2, N'El Nano', N'user@gmail.com', 0x1B739B7049BB738BF2DEE3D6073A70B6C3ABE5A7B766CB6F9703857EE431C12D596CFF6AE7D1BEC80825FBB1FDD47F6012D046E1F4B48F09A4EB3898475781BD, CAST(N'2024-03-19' AS Date), CAST(N'2024-03-19T10:44:57.7955523' AS DateTime2), 2, N'fernando.jpg', 0, 5, 10, N'XGILcFQmaoZhRrIUVuMEQb`Ovlta\CSdQOsf`PGoTpsEHF\ube')
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [RegistrationDate], [LastAccess], [RolID], [ProfilePicture], [TotalPosts], [TeamID], [DriverID], [Salt]) VALUES (3, N'Supermax', N'supermax@gmail.com', 0x7C2EFC21EF3B77080370CF9ABF159AEE0291C8E004541D4A3C4BDA7332C9419D447E88ECB1CBEC7E573AB93B20B2AA74B682CD9D643B99E89557BF69F7862BBA, CAST(N'2024-03-19' AS Date), CAST(N'2024-03-19T10:45:44.4287069' AS DateTime2), 2, N'supermax.jpg', 0, 1, 1, N'gIj[CP\iTLcfJceWAqukRJYPbqsdK`qfPKk[JhvknJIwyt]OEB')
INSERT [dbo].[Users] ([UserID], [UserName], [Email], [Password], [RegistrationDate], [LastAccess], [RolID], [ProfilePicture], [TotalPosts], [TeamID], [DriverID], [Salt]) VALUES (4, N'Ivan', N'ivan@gmail.com', 0x39EAE0851A40B42C5E28BC89F947274761AF143C5F0F063A2D7AE85E6B4A88ED2916114515BAB563AC2ADBD7AC27391EF984A567201C55A67FE3988EC91588AD, CAST(N'2024-03-20' AS Date), CAST(N'2024-03-20T08:05:23.9638945' AS DateTime2), 2, N'ivan2.png', 0, 1, 1, N'[dcL`ah]JAPQ\aKQFwDwAR^gEHXxW[fhvROjAfLAbdgXyaE`sV')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__737584F62DF0D2DC]    Script Date: 20/03/2024 9:38:39 ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534D7A5A694]    Script Date: 20/03/2024 9:38:39 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__A9D10534D7A5A694] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Conversations] ADD  CONSTRAINT [DF__Conversat__Entri__0D7A0286]  DEFAULT ((0)) FOR [EntryCount]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TotalPosts]  DEFAULT ((0)) FOR [TotalPosts]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK__Conversations__TopicID__04E4BC85] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topics] ([TopicID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK__Conversations__TopicID__04E4BC85]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK__Conversations__UserID__300424B4] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK__Conversations__UserID__300424B4]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_Topics] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topics] ([TopicID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_Topics]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK__Drivers__TeamID__3C69FB99] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Teams] ([TeamID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK__Drivers__TeamID__3C69FB99]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK__Posts__Conversat__08B54D69] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[Conversations] ([ConversationID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK__Posts__Conversat__08B54D69]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK__Posts__UserID__32E0915F] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK__Posts__UserID__32E0915F]
GO
ALTER TABLE [dbo].[Races]  WITH CHECK ADD  CONSTRAINT [FK__Races__WinnerDri__693CA210] FOREIGN KEY([WinnerDriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Races] CHECK CONSTRAINT [FK__Races__WinnerDri__693CA210]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__RolID__29572725] FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__RolID__29572725]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_UserDriver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_UserDriver]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_UserTeam] FOREIGN KEY([TeamID])
REFERENCES [dbo].[Teams] ([TeamID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_UserTeam]
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGINACION_CONVERSATIONS]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_PAGINACION_CONVERSATIONS]
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
/****** Object:  StoredProcedure [dbo].[SP_PAGINACION_POSTS]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_PAGINACION_POSTS]
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
	where query.posicion >= @posicion and query.posicion < (@posicion + 2)
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_ENTRYCOUNT]    Script Date: 20/03/2024 9:38:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UPDATE_ENTRYCOUNT]
    @conversationID INT
AS
BEGIN
    UPDATE Conversations
    SET EntryCount = EntryCount + 1
    WHERE ConversationID = @conversationID;
END;
GO
USE [master]
GO
ALTER DATABASE [BOXBOX] SET  READ_WRITE 
GO
