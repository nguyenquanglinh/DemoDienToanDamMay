USE [DiemToanDamMay]
GO
/****** Object:  Table [dbo].[FileImg]    Script Date: 9/20/2021 11:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileImg](
	[KeyImg] [nvarchar](2000) NOT NULL,
	[ValueImg] [nvarchar](2000) NOT NULL,
	[FolderName] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FileImg] PRIMARY KEY CLUSTERED 
(
	[FileName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FolderByUser]    Script Date: 9/20/2021 11:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FolderByUser](
	[FolderName] [nvarchar](50) NOT NULL,
	[Question] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Answer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FolderByUser_1] PRIMARY KEY CLUSTERED 
(
	[FolderName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginEmail]    Script Date: 9/20/2021 11:34:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginEmail](
	[Email] [nvarchar](50) NOT NULL,
	[PassWords] [nvarchar](50) NOT NULL,
	[Code] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LoginEmail] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FolderByUser]  WITH CHECK ADD  CONSTRAINT [FK_FolderByUser_LoginEmail] FOREIGN KEY([Email])
REFERENCES [dbo].[LoginEmail] ([Email])
GO
ALTER TABLE [dbo].[FolderByUser] CHECK CONSTRAINT [FK_FolderByUser_LoginEmail]
GO
