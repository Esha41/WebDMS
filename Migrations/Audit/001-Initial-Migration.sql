USE [DMSAudit]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/22/2022 1:11:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Audits]    Script Date: 5/22/2022 1:11:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[AuditDateTimeUtc] [bigint] NOT NULL,
	[AuditType] [nvarchar](30) NOT NULL,
	[AuditUser] [nvarchar](50) NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[KeyValues] [nvarchar](max) NOT NULL,
	[OldValues] [nvarchar](max) NOT NULL,
	[NewValues] [nvarchar](max) NOT NULL,
	[ChangedColumns] [nvarchar](max) NOT NULL,
	[RequestId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Audits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audits] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
