CREATE DATABASE [Imagregram]
GO

USE [Imagregram]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 21/11/2021 9:38:56 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentText] [nvarchar](500) NULL,
	[PostId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
	[CommentBy] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostResources]    Script Date: 21/11/2021 9:38:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostResources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[ResourceUrl] [nvarchar](max) NOT NULL,
	[ResourceTypeId] [int] NOT NULL,
	[ResourceIndex] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_PostResources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 21/11/2021 9:38:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](500) NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
	[PostBy] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceTypes]    Script Date: 21/11/2021 9:38:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Code] [varchar](30) NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_ResourceTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/11/2021 9:38:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[Username] [varchar](50) NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Posts]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([CommentBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[PostResources]  WITH CHECK ADD  CONSTRAINT [FK_PostResources_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[PostResources] CHECK CONSTRAINT [FK_PostResources_Posts]
GO
ALTER TABLE [dbo].[PostResources]  WITH CHECK ADD  CONSTRAINT [FK_PostResources_ResourceTypes] FOREIGN KEY([ResourceTypeId])
REFERENCES [dbo].[ResourceTypes] ([Id])
GO
ALTER TABLE [dbo].[PostResources] CHECK CONSTRAINT [FK_PostResources_ResourceTypes]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([PostBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
/****** Object:  StoredProcedure [dbo].[GetPostsWithComments]    Script Date: 21/11/2021 9:38:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPostsWithComments]
@FromPostId INT, 
@MaxItemPerPage INT
AS
BEGIN

	SET NOCOUNT ON;
	
	 SELECT p.Id, p.Caption, u.Username AS PostedBy, COUNT(c.Id) AS NumberOfComments
	 INTO #Temp_Posts
	 FROM Posts p INNER JOIN Users u ON p.PostBy = u.Id
		LEFT JOIN Comments c ON c.PostId = p.Id
	 WHERE p.Id > @FromPostId
	 GROUP BY p.Id, p.Caption, u.Username
	 ORDER BY NumberOfComments DESC
	 OFFSET 0 ROWS
	 FETCH NEXT @MaxItemPerPage ROWS ONLY;

	 SELECT *
	 FROM #Temp_Posts

	 SELECT pr.PostId, pr.ResourceUrl, pr.ResourceIndex
	 FROM PostResources pr INNER JOIN #Temp_Posts tp ON pr.PostId = tp.Id

	 SELECT c.PostId, c.CommentText, c.Username
	 FROM (
		 SELECT TOP 2 c.CommentText, c.PostId, u.Username, c.CreatedDateTime
		 FROM Comments c INNER JOIN #Temp_Posts tp ON c.PostId = tp.Id
			INNER JOIN Users u ON c.CommentBy = u.Id 
		 ORDER BY c.Id DESC
	 ) c
	 ORDER BY c.CreatedDateTime
END
GO
