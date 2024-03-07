USE [master]
GO
/****** Object:  Database [BOXBOX]    Script Date: 07/03/2024 22:48:45 ******/
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
/****** Object:  Table [dbo].[Conversations]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[ConversationID] [int] NOT NULL,
	[TopicID] [int] NULL,
	[UserID] [int] NULL,
	[Title] [varchar](255) NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Conversations__688356E4A9DB70AC] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] NOT NULL,
	[DriverName] [varchar](255) NULL,
	[CarNumber] [int] NULL,
	[TeamID] [int] NULL,
	[Flag] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostID] [int] NOT NULL,
	[ConversationID] [int] NULL,
	[UserID] [int] NULL,
	[Text] [text] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Posts__AA126038AB9243BE] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Races]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Races](
	[RaceID] [int] NOT NULL,
	[RaceName] [varchar](255) NULL,
	[Location] [varchar](255) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[WinnerDriverID] [int] NULL,
	[WinningTeamID] [int] NULL,
 CONSTRAINT [PK__Races__05FBD6D4D2CB7E4A] PRIMARY KEY CLUSTERED 
(
	[RaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RaceSurveys]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RaceSurveys](
	[SurveyID] [int] NOT NULL,
	[RaceID] [int] NULL,
	[Question] [varchar](255) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 07/03/2024 22:48:45 ******/
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
/****** Object:  Table [dbo].[SurveyOptions]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyOptions](
	[OptionID] [int] NOT NULL,
	[SurveyID] [int] NULL,
	[OptionText] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyResults]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyResults](
	[ResultID] [int] NOT NULL,
	[SurveyID] [int] NULL,
	[OptionID] [int] NULL,
	[UserID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 07/03/2024 22:48:45 ******/
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
/****** Object:  Table [dbo].[Topics]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[TopicID] [int] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__Topics__E210AC4F2F7AF7E2] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/03/2024 22:48:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[UserName] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[RegistrationDate] [datetime] NULL,
	[LastAccess] [datetime] NULL,
	[RolID] [int] NULL,
	[ProfilePicture] [varchar](max) NULL,
	[TotalPosts] [int] NULL,
	[TeamID] [int] NULL,
	[DriverID] [int] NULL,
	[Salt] [nvarchar](255) NULL,
	[Estado] [varchar](50) NULL,
 CONSTRAINT [PK__Users__1788CCAC4E67A637] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (1, N'Max Verstappen', 1, 1, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (2, N'Sergio Perez', 11, 1, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (3, N'George Russell', 63, 2, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (4, N'Lewis Hamilton', 44, 2, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (5, N'Charles Leclerc', 16, 3, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (6, N'Carlos Sainz', 55, 3, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (7, N'Oscar Piastri', 81, 4, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (8, N'Lando Norris', 4, 4, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (9, N'Lance Stroll', 18, 5, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (10, N'Fernando Alonso', 14, 5, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (11, N'Esteban Ocon', 31, 6, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (12, N'Pierre Gasly', 10, 6, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (13, N'Alexander Albon', 23, 7, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (14, N'Logan Sargeant', 2, 7, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (15, N'Daniel Ricciardo', 3, 8, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (16, N'Yuki Tsunoda', 22, 8, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (17, N'Valtteri Bottas', 77, 9, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (18, N'Zhou Guanyu', 24, 9, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (19, N'Kevin Magnussen', 20, 10, NULL)
INSERT [dbo].[Drivers] ([DriverID], [DriverName], [CarNumber], [TeamID], [Flag]) VALUES (20, N'Nico Hulkenberg', 27, 10, NULL)
GO
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (1, N'FORMULA 1 GULF AIR BAHRAIN GRAND PRIX 2024', N'Bahrein', CAST(N'2024-02-29' AS Date), CAST(N'2024-03-02' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (2, N'FORMULA 1 STC SAUDI ARABIAN GRAND PRIX 2024', N'Arabia Saudí', CAST(N'2024-03-07' AS Date), CAST(N'2024-03-09' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (3, N'FORMULA 1 ROLEX AUSTRALIAN GRAND PRIX 2024', N'Australia', CAST(N'2024-03-22' AS Date), CAST(N'2024-03-24' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (4, N'FORMULA 1 MSC CRUISES JAPANESE GRAND PRIX 2024', N'Japón', CAST(N'2024-04-05' AS Date), CAST(N'2024-04-07' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (5, N'FORMULA 1 LENOVO CHINESE GRAND PRIX 2024', N'China', CAST(N'2024-04-19' AS Date), CAST(N'2024-04-21' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (6, N'FORMULA 1 CRYPTO.COM MIAMI GRAND PRIX 2024', N'Estados Unidos', CAST(N'2024-05-03' AS Date), CAST(N'2024-05-05' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (7, N'FORMULA 1 MSC CRUISES GRAN PREMIO DELL''EMILIA-ROMAGNA 2024', N'Italia', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-19' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (8, N'FORMULA 1 GRAND PRIX DE MONACO 2024', N'Monaco', CAST(N'2024-05-24' AS Date), CAST(N'2024-05-26' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (9, N'FORMULA 1 AWS GRAND PRIX DU CANADA 2024', N'Canada', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-09' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (10, N'FORMULA 1 ARAMCO GRAN PREMIO DE ESPAÑA 2024', N'España', CAST(N'2024-06-21' AS Date), CAST(N'2024-06-23' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (11, N'FORMULA 1 QATAR AIRWAYS AUSTRIAN GRAND PRIX 2024', N'Austria', CAST(N'2024-06-28' AS Date), CAST(N'2024-06-30' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (12, N'FORMULA 1 QATAR AIRWAYS BRITISH GRAND PRIX 2024', N'Gran Bretaña', CAST(N'2024-07-05' AS Date), CAST(N'2024-07-07' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (13, N'FORMULA 1 HUNGARIAN GRAND PRIX 2024', N'Hungría', CAST(N'2024-07-19' AS Date), CAST(N'2024-07-21' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (14, N'FORMULA 1 ROLEX BELGIAN GRAND PRIX 2024', N'Bélgica', CAST(N'2024-07-26' AS Date), CAST(N'2024-07-28' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (15, N'FORMULA 1 HEINEKEN DUTCH GRAND PRIX 2024', N'Países Bajos', CAST(N'2024-08-23' AS Date), CAST(N'2024-08-25' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (16, N'FORMULA 1 PIRELLI GRAN PREMIO D’ITALIA 2024', N'Italia', CAST(N'2024-08-30' AS Date), CAST(N'2024-09-01' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (17, N'FORMULA 1 QATAR AIRWAYS AZERBAIJAN GRAND PRIX 2024', N'Azerbaiyán', CAST(N'2024-09-13' AS Date), CAST(N'2024-09-15' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (18, N'FORMULA 1 SINGAPORE AIRLINES SINGAPORE GRAND PRIX 2024', N'Singapur', CAST(N'2024-09-20' AS Date), CAST(N'2024-09-22' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (19, N'FORMULA 1 PIRELLI UNITED STATES GRAND PRIX 2024', N'Estados Unidos', CAST(N'2024-10-18' AS Date), CAST(N'2024-10-20' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (20, N'FORMULA 1 GRAN PREMIO DE LA CIUDAD DE MÉXICO 2024', N'México', CAST(N'2024-10-25' AS Date), CAST(N'2024-10-27' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (21, N'FORMULA 1 LENOVO GRANDE PRÊMIO DE SÃO PAULO 2024', N'Brasil', CAST(N'2024-11-01' AS Date), CAST(N'2024-11-03' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (22, N'FORMULA 1 HEINEKEN SILVER LAS VEGAS GRAND PRIX 2024', N'Estados Unidos', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-23' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (23, N'FORMULA 1 QATAR AIRWAYS QATAR GRAND PRIX 2024', N'Qatar', CAST(N'2024-11-29' AS Date), CAST(N'2024-12-01' AS Date), NULL, NULL)
INSERT [dbo].[Races] ([RaceID], [RaceName], [Location], [StartDate], [EndDate], [WinnerDriverID], [WinningTeamID]) VALUES (24, N'FORMULA 1 ETIHAD AIRWAYS ABU DHABI GRAND PRIX 2024', N'Abu Dhabi', CAST(N'2024-12-06' AS Date), CAST(N'2024-12-08' AS Date), NULL, NULL)
GO
INSERT [dbo].[Roles] ([RolID], [Name]) VALUES (2, N'Admin')
INSERT [dbo].[Roles] ([RolID], [Name]) VALUES (1, N'User')
GO
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (1, N'Red Bull Racing', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (2, N'Mercedes', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (3, N'Ferrari', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (4, N'McLaren', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (5, N'Aston Martin', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (6, N'Alpine', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (7, N'Williams', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (8, N'RB', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (9, N'Kick Sauber', NULL)
INSERT [dbo].[Teams] ([TeamID], [TeamName], [Logo]) VALUES (10, N'Haas F1 Team', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__737584F68E159822]    Script Date: 07/03/2024 22:48:46 ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534D7A5A694]    Script Date: 07/03/2024 22:48:46 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__A9D10534D7A5A694] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TotalPosts]  DEFAULT ((0)) FOR [TotalPosts]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK__Conversations__TopicID__2F10007B] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topics] ([TopicID])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK__Conversations__TopicID__2F10007B]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK__Conversations__UserID__300424B4] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK__Conversations__UserID__300424B4]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD FOREIGN KEY([TeamID])
REFERENCES [dbo].[Teams] ([TeamID])
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK__Posts__ConversationID__33D4B598] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[Conversations] ([ConversationID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK__Posts__ConversationID__33D4B598]
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
ALTER TABLE [dbo].[Races]  WITH CHECK ADD  CONSTRAINT [FK__Races__WinningTe__6A30C649] FOREIGN KEY([WinningTeamID])
REFERENCES [dbo].[Teams] ([TeamID])
GO
ALTER TABLE [dbo].[Races] CHECK CONSTRAINT [FK__Races__WinningTe__6A30C649]
GO
ALTER TABLE [dbo].[RaceSurveys]  WITH CHECK ADD  CONSTRAINT [FK__RaceSurve__RaceI__6E01572D] FOREIGN KEY([RaceID])
REFERENCES [dbo].[Races] ([RaceID])
GO
ALTER TABLE [dbo].[RaceSurveys] CHECK CONSTRAINT [FK__RaceSurve__RaceI__6E01572D]
GO
ALTER TABLE [dbo].[SurveyOptions]  WITH CHECK ADD FOREIGN KEY([SurveyID])
REFERENCES [dbo].[RaceSurveys] ([SurveyID])
GO
ALTER TABLE [dbo].[SurveyResults]  WITH CHECK ADD FOREIGN KEY([OptionID])
REFERENCES [dbo].[SurveyOptions] ([OptionID])
GO
ALTER TABLE [dbo].[SurveyResults]  WITH CHECK ADD FOREIGN KEY([SurveyID])
REFERENCES [dbo].[RaceSurveys] ([SurveyID])
GO
ALTER TABLE [dbo].[SurveyResults]  WITH CHECK ADD  CONSTRAINT [FK__SurveyRes__UserI__787EE5A0] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[SurveyResults] CHECK CONSTRAINT [FK__SurveyRes__UserI__787EE5A0]
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
USE [master]
GO
ALTER DATABASE [BOXBOX] SET  READ_WRITE 
GO
