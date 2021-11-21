USE [Imagregram]
GO

--Users
INSERT INTO [dbo].[Users]
           ([Id]
           ,[Username]
           ,[CreatedDateTime])
     VALUES
           (1
           ,N'JohnnyNguyen'
           ,GETDATE())
GO

INSERT INTO [dbo].[Users]
           ([Id]
           ,[Username]
           ,[CreatedDateTime])
     VALUES
           (2
           ,N'Sheldon Cooper'
           ,GETDATE())
GO

--Resource Types
INSERT INTO [dbo].[ResourceTypes]
           ([Name]
		   ,[Code]
           ,[CreatedDateTime])
     VALUES
           ('Image'
		   ,'IMG'
           ,GETDATE())
GO

-- Posts


INSERT INTO [dbo].[Posts]
           ([Caption]
           ,[CreatedDateTime]
           ,[PostBy])
     VALUES
           ('Rose are red!'
           ,GETDATE()
           ,1)
GO

INSERT INTO [dbo].[PostResources]
           ([PostId]
           ,[ResourceUrl]
           ,[ResourceTypeId]
           ,[ResourceIndex]
           ,[CreatedDateTime])
     VALUES
           (1
           ,'https://storage.googleapis.com/gweb-uniblog-publish-prod/images/LightedpixelsXGoogle_001_gUcizRU.max-1000x1000.jpg'
           ,1
           ,0
           ,GETDATE())
GO

INSERT INTO [dbo].[Posts]
           ([Caption]
           ,[CreatedDateTime]
           ,[PostBy])
     VALUES
           ('Check it out!'
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[PostResources]
           ([PostId]
           ,[ResourceUrl]
           ,[ResourceTypeId]
           ,[ResourceIndex]
           ,[CreatedDateTime])
     VALUES
           (2
           ,'https://storage.googleapis.com/gweb-uniblog-publish-prod/images/LightedpixelsXGoogle_001_gUcizRU.max-1000x1000.jpg'
           ,1
           ,0
           ,GETDATE())
GO

INSERT INTO [dbo].[Posts]
           ([Caption]
           ,[CreatedDateTime]
           ,[PostBy])
     VALUES
           ('Holly Cow!'
           ,GETDATE()
           ,1)
GO

INSERT INTO [dbo].[PostResources]
           ([PostId]
           ,[ResourceUrl]
           ,[ResourceTypeId]
           ,[ResourceIndex]
           ,[CreatedDateTime])
     VALUES
           (3
           ,'https://storage.googleapis.com/gweb-uniblog-publish-prod/images/LightedpixelsXGoogle_001_gUcizRU.max-1000x1000.jpg'
           ,1
           ,0
           ,GETDATE())
GO

INSERT INTO [dbo].[Posts]
           ([Caption]
           ,[CreatedDateTime]
           ,[PostBy])
     VALUES
           ('Happy New Year!'
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[PostResources]
           ([PostId]
           ,[ResourceUrl]
           ,[ResourceTypeId]
           ,[ResourceIndex]
           ,[CreatedDateTime])
     VALUES
           (4
           ,'https://storage.googleapis.com/gweb-uniblog-publish-prod/images/LightedpixelsXGoogle_001_gUcizRU.max-1000x1000.jpg'
           ,1
           ,0
           ,GETDATE())
GO

INSERT INTO [dbo].[Posts]
           ([Caption]
           ,[CreatedDateTime]
           ,[PostBy])
     VALUES
           ('Happy Lunar New Year!'
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[PostResources]
           ([PostId]
           ,[ResourceUrl]
           ,[ResourceTypeId]
           ,[ResourceIndex]
           ,[CreatedDateTime])
     VALUES
           (5
           ,'https://storage.googleapis.com/gweb-uniblog-publish-prod/images/LightedpixelsXGoogle_001_gUcizRU.max-1000x1000.jpg'
           ,1
           ,0
           ,GETDATE())
GO

-- Comments

INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Wow! Looks great.'
           ,1
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Thanks dude.'
           ,1
           ,GETDATE()
           ,1)
GO

INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Cool!'
           ,1
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Thanks!'
           ,1
           ,GETDATE()
           ,1)
GO


INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Cool!'
           ,2
           ,GETDATE()
           ,2)
GO

INSERT INTO [dbo].[Comments]
           ([CommentText]
           ,[PostId]
           ,[CreatedDateTime]
           ,[CommentBy])
     VALUES
           ('Thanks!'
           ,2
           ,GETDATE()
           ,1)
GO