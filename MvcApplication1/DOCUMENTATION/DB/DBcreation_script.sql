USE [~\WEBSITEDB.MDF]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 04.05.2020 16:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](160) NOT NULL,
	[Description] [nvarchar](160) NULL,
	[Keywords] [nvarchar](160) NULL,
	[Content] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[Address] [nvarchar](20) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[IsInMenu] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04.05.2020 16:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pages] ADD  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Pages] ADD  DEFAULT ((1)) FOR [IsVisible]
GO
ALTER TABLE [dbo].[Pages] ADD  DEFAULT ((1)) FOR [IsInMenu]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO

INSERT INTO [dbo].[Pages]
           ([Title]
           ,[Description]
           ,[Keywords]
           ,[Content]
           ,[ParentId]
           ,[Address]
           ,[IsVisible]
           ,[IsInMenu]
           ,[DisplayOrder])
     VALUES
           ('CompanyName'
           ,''
           ,''
           ,'type the text about your company'
           ,0
           ,'Index'
           ,1
           ,1
           ,0)
GO

