USE [master]
GO
/****** Object:  Database [QAManagementSystem]    Script Date: 20-02-2024 19:37:06 ******/
CREATE DATABASE [QAManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QAManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QAManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QAManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QAManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QAManagementSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QAManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QAManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QAManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QAManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QAManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QAManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [QAManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QAManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QAManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QAManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QAManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QAManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QAManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QAManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QAManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QAManagementSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QAManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QAManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QAManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QAManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QAManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QAManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QAManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QAManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QAManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [QAManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QAManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QAManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QAManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QAManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QAManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QAManagementSystem] SET QUERY_STORE = OFF
GO
USE [QAManagementSystem]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 20-02-2024 19:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[QuestionId] [int] NULL,
	[AnswerText] [varchar](255) NULL,
	[CorrectOption] [char](1) NULL,
	[SubmissionTimestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 20-02-2024 19:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionPaperId] [int] NOT NULL,
	[QuestionText] [nvarchar](max) NOT NULL,
	[OptionA] [nvarchar](max) NOT NULL,
	[OptionB] [nvarchar](max) NOT NULL,
	[OptionC] [nvarchar](max) NOT NULL,
	[OptionD] [nvarchar](max) NOT NULL,
	[CorrectAnswer] [nvarchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionPaper]    Script Date: 20-02-2024 19:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionPaper](
	[QuestionPaperId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatorUserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionPaperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubmittedQuestionPapers]    Script Date: 20-02-2024 19:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubmittedQuestionPapers](
	[SubmissionID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[QuestionPaperId] [int] NULL,
	[SubmissionTimestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20-02-2024 19:37:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Answer] ON 

INSERT [dbo].[Answer] ([AnswerID], [UserId], [QuestionId], [AnswerText], [CorrectOption], [SubmissionTimestamp]) VALUES (19, 5, 22, N'language', N'A', CAST(N'2024-02-20T18:52:23.720' AS DateTime))
SET IDENTITY_INSERT [dbo].[Answer] OFF
GO
SET IDENTITY_INSERT [dbo].[Question] ON 

INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (22, 16, N'what is gujarati', N'language', N'software', N'hardware', N'tool', N'A')
INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (23, 17, N'what is maths.', N'language', N'software', N'framwork', N'tool', N'A')
INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (24, 17, N'what is 1+1', N'5', N'6', N'2', N'1', N'D')
INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (25, 18, N'what is 2*5', N'20', N'10', N'30', N'25', N'B')
INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (27, 19, N'what is 20/2', N'121', N'12', N'10', N'111', N'C')
INSERT [dbo].[Question] ([QuestionId], [QuestionPaperId], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]) VALUES (28, 20, N'what is Stack', N'LIFO ', N'FIFO', N'class', N'None', N'A')
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionPaper] ON 

INSERT [dbo].[QuestionPaper] ([QuestionPaperId], [Title], [Description], [CreationDate], [Status], [CreatorUserId]) VALUES (16, N'SSc Exam', N'this is exam', CAST(N'2024-02-20T18:50:42.253' AS DateTime), N'Approved', 1)
INSERT [dbo].[QuestionPaper] ([QuestionPaperId], [Title], [Description], [CreationDate], [Status], [CreatorUserId]) VALUES (17, N'JEEMains2024', N'this is Jeemains 2024', CAST(N'2024-02-20T19:06:06.143' AS DateTime), N'Pending', 3)
INSERT [dbo].[QuestionPaper] ([QuestionPaperId], [Title], [Description], [CreationDate], [Status], [CreatorUserId]) VALUES (18, N'Gujcet2024', N'this is Gujcet2024', CAST(N'2024-02-20T19:07:29.613' AS DateTime), N'Draft', 3)
INSERT [dbo].[QuestionPaper] ([QuestionPaperId], [Title], [Description], [CreationDate], [Status], [CreatorUserId]) VALUES (19, N'TAT Exam', N'this is tat exam', CAST(N'2024-02-20T19:10:32.150' AS DateTime), N'Rejected', 3)
INSERT [dbo].[QuestionPaper] ([QuestionPaperId], [Title], [Description], [CreationDate], [Status], [CreatorUserId]) VALUES (20, N'DSA Exam', N'this is DSA exam.', CAST(N'2024-02-20T19:17:53.473' AS DateTime), N'Approved', 3)
SET IDENTITY_INSERT [dbo].[QuestionPaper] OFF
GO
SET IDENTITY_INSERT [dbo].[SubmittedQuestionPapers] ON 

INSERT [dbo].[SubmittedQuestionPapers] ([SubmissionID], [UserId], [QuestionPaperId], [SubmissionTimestamp]) VALUES (11, 5, 16, CAST(N'2024-02-20T18:52:23.720' AS DateTime))
SET IDENTITY_INSERT [dbo].[SubmittedQuestionPapers] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [Role]) VALUES (1, N'Arya', N'arya123', N'arya@gmail.com', N'Admin')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [Role]) VALUES (3, N'teacher', N'teacher123', N'teacher@gmail.com', N'Teacher')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [Role]) VALUES (5, N'student', N'student123', N'student@gmail.com', N'Student')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [Role]) VALUES (6, N'student2', N'student2', N'student2@gmail.com', N'Student')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534DA1716A5]    Script Date: 20-02-2024 19:37:07 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionPaper] FOREIGN KEY([QuestionPaperId])
REFERENCES [dbo].[QuestionPaper] ([QuestionPaperId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionPaper]
GO
ALTER TABLE [dbo].[QuestionPaper]  WITH CHECK ADD FOREIGN KEY([CreatorUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SubmittedQuestionPapers]  WITH CHECK ADD FOREIGN KEY([QuestionPaperId])
REFERENCES [dbo].[QuestionPaper] ([QuestionPaperId])
GO
ALTER TABLE [dbo].[SubmittedQuestionPapers]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Student' OR [Role]='Teacher' OR [Role]='Admin'))
GO
USE [master]
GO
ALTER DATABASE [QAManagementSystem] SET  READ_WRITE 
GO
