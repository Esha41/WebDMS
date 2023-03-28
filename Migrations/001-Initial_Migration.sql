USE [DMS]
GO
/****** Object:  Table [dbo].[SystemUsers]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[Jmbg] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentsCheckedOutLogs]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentsCheckedOutLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[BatchItemId] [int] NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[CheckedOutAt] [bigint] NOT NULL,
	[CheckedInAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentsCheckedOutLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DocumentCheckOutLogsView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

              CREATE View [dbo].[DocumentCheckOutLogsView] As                
                        SELECT TOP (1000) DocCheckLogs.[Id]
									      ,DocCheckLogs.[IsActive]
									      ,DocCheckLogs.[CreatedAt]
									      ,DocCheckLogs.[UpdatedAt]
									      ,DocCheckLogs.[BatchItemId]
									      , isNull((
										  select top 1 SysUser.FullName from SystemUsers SysUser
										  where SysUser.Id = DocCheckLogs.SystemUserId
										  ),'_') [UserName]
									      ,DocCheckLogs.[CheckedOutAt]
									      ,DocCheckLogs.[CheckedInAt]
                                  FROM [DMS].[dbo].[DocumentsCheckedOutLogs] DocCheckLogs
                                  
GO
/****** Object:  Table [dbo].[Batches]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [nvarchar](50) NOT NULL,
	[BusinessUnitId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[BatchStatusId] [int] NOT NULL,
	[LockedBy] [int] NULL,
	[VerifiedStartDate] [datetime] NULL,
	[VerifiedEndDate] [datetime] NULL,
	[BatchSourceId] [int] NOT NULL,
	[PublishedDate] [datetime] NULL,
	[RetriesCount] [int] NULL,
	[MandatoryAlerts] [nvarchar](max) NULL,
	[ValidationAlerts] [nvarchar](max) NULL,
	[CurrentOTP] [int] NULL,
	[OTPValidUntil] [datetime] NULL,
	[AppliedGDPR] [bit] NULL,
	[RecognizedDate] [datetime] NULL,
	[StartProcessDate] [datetime] NULL,
	[InternalRequestId] [uniqueidentifier] NULL,
	[LockedByNavigationId] [nvarchar](450) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Batches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchStatuses]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchStatus] [nvarchar](50) NOT NULL,
	[EnumValue] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BatchStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[GDPRDaysToBeKept] [int] NOT NULL,
	[Email] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[UsersPerCompany] [nvarchar](50) NULL,
	[CompanyCode] [nvarchar](max) NULL,
	[Password] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AFM] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[IsNotValidForTransaction] [bit] NOT NULL,
	[Reason] [varchar](500) NULL,
	[GdprdaysToBeKept] [int] NOT NULL,
	[ExternalId] [nvarchar](50) NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ClientRepositoryView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


                                CREATE  View [dbo].[ClientRepositoryView] As
                                    SELECT Repo.[Id] 
                                        ,Repo.[AppliedGDPR]
                                        ,cus.[Id] as ClientId
                                		,cus.[FirstName]+' '+cus.[LastName] as ClientName
                                	    ,c.[Id] as CompanyId
                                	    ,c.[CompanyName]
                                		,Repo.[Id] as RepositoryName
                                		, isNull((
										 select top 1 batstatus.BatchStatus from BatchStatuses batstatus
										 where batstatus.Id = Repo.BatchStatusId
										 ),'-') [ClientStatus]
                                		,Repo.[CreatedDate]
                                        ,cus.[IsActive]
                                        ,Repo.[CreatedAt]
                                        ,Repo.[UpdatedAt]
                                FROM [Batches] Repo
                                LEFT OUTER JOIN  [dbo].[Clients] cus on Repo.[CustomerId] = cus.[Id]   
                                LEFT OUTER JOIN  [dbo].[Companies] c on Repo.[CompanyId] = c.[Id]

             
GO
/****** Object:  Table [dbo].[DocumentVersion]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentVersion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_DocumentVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchItemStatuses]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchItemStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BatchItemStatus] [nvarchar](100) NOT NULL,
	[EnumValue] [nvarchar](100) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BatchItemStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentApprovalHistory]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentApprovalHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchItemReference] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Approvedby] [nvarchar](max) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentApprovalHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchItems]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[BatchItemStatusId] [int] NOT NULL,
	[DocumentClassId] [int] NULL,
	[OccuredAt] [datetime] NOT NULL,
	[ParentId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[DocumentVersionId] [int] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[BatchItemReference] [nvarchar](max) NULL,
	[CompanyId] [int] NOT NULL,
	[SystemRoleId] [int] NULL,
 CONSTRAINT [PK_BatchItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentsCheckedOut]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentsCheckedOut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchItemId] [int] NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentsCheckedOut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DocumentCheckOutView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[DocumentCheckOutView] As
SELECT
bi.[Id]
, bi.[FileName]
, DocVer.[Id] as VersionId
, isNull((
select top 1 cl.FirstName+' '+cl.LastName  from Clients cl
inner join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
),'-') [ClientName]
,case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, bi.BatchId
, bi.CompanyId
, docCheckOut.CreatedAt as CreatedOn 
, docCheckOut.UpdatedAt as LastModifiedOn
, docCheckOut.CreatedAt 
, docCheckOut.UpdatedAt
, docCheckOut.IsActive
,SysUser.Id As UserId
FROM [DocumentsCheckedOut] docCheckOut
inner join [BatchItems] bi on bi.Id = docCheckOut.BatchItemId
inner join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
inner join [DocumentVersion] DocVer on DocVer.Id = bi.DocumentVersionId
inner join [SystemUsers] SysUser on docCheckOut.SystemUserId = SysUser.Id


                                  
GO
/****** Object:  Table [dbo].[BatchItemPages]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchItemPages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchItemId] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[Number] [int] NOT NULL,
	[OriginalName] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[DocumentVersionId] [int] NOT NULL,
 CONSTRAINT [PK_BatchItemPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentClasses]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassName] [nvarchar](250) NOT NULL,
	[DocumentClassCode] [nvarchar](100) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_DocumentClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchMetaHistory]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchMetaHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OccuredAt] [datetime] NOT NULL,
	[BatchId] [int] NOT NULL,
	[PreviousValues] [nvarchar](max) NOT NULL,
	[CurrentValues] [nvarchar](max) NOT NULL,
	[SystemUserId] [int] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BatchMetaHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentTypes]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTypeName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[DocumentTypeCode] [nvarchar](50) NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DocumentReviewView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View[dbo].[DocumentReviewView] As
SELECT bi.Id
, bi.[FileName]
, dc.DocumentClassName
, dtc.DocumentTypeName
, isNull((
select top 1 cl.FirstName + ' ' + cl.LastName  from Clients cl
    inner
                                               join [Batches] batch on batch.CustomerId = cl.Id
where batch.Id = bi.BatchId
), '-')[ClientName]
, isNull((
select top 1 su.FullName from BatchMetaHistory bmh
inner
                         join SystemUsers su on bmh.SystemUserId = su.Id
where bmh.BatchId = bi.BatchId
),'-') [LastModifiedBy]
, case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
, isNull((
select top 1 batstatus.ID from BatchItemStatuses batstatus
where batstatus.Id = bi.BatchItemStatusId
),'-') [Status]
, isNull((
select top 1 docAppHis.RoleId from DocumentApprovalHistory docAppHis
where docAppHis.BatchItemReference = bi.BatchItemReference And docAppHis.Approved = 0
),'-') [CurrentReviewRole]
, bi.BatchId
, bi.IsActive
, bi.CreatedAt
, bi.UpdatedAt
, bi.CompanyId
, DocVer.[LastModifiedBy] as LastModifiedById
,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
(case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
                (select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id = dco.SystemUserId

                            where dco.BatchItemId = bi.Id )end) as CheckedOutBy
FROM[BatchItems] bi
left outer join[BatchItemPages] bip on bi.Id = bip.BatchItemId
left outer join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
left outer join[DocumentClasses] dc on bi.DocumentClassId = dc.Id
left outer join[DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
left outer join[DocumentVersion] DocVer on DocVer.Id = bi.DocumentVersionId
left outer join[Batches] bat on bi.BatchId = bat.Id
join
(
    SELECT max(id) as id
    FROM[BatchItems]
    GROUP BY[BatchItemReference]
) bi2 on bi.id = bi2.id

                                
GO
/****** Object:  Table [dbo].[SystemRoles]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_SystemRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchMeta]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchMeta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[DocumentClassFieldId] [int] NOT NULL,
	[DictionaryValueId] [int] NULL,
	[FieldValue] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[DocumentVersionId] [int] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[BatchItemReference] [nvarchar](max) NULL,
 CONSTRAINT [PK_BatchMeta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentClassFields]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentClassFields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassId] [int] NOT NULL,
	[DocumentClassFieldTypeId] [int] NOT NULL,
	[UILabel] [nvarchar](128) NOT NULL,
	[DictionaryTypeId] [int] NULL,
	[IsMandatory] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[MinLength] [int] NULL,
	[MaxLength] [int] NULL,
	[UISort] [int] NULL,
	[DocumentClassFieldCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_DocumentClassFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientCompanyCustomFieldValues]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientCompanyCustomFieldValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[FieldId] [int] NOT NULL,
	[DictionaryValueId] [int] NULL,
	[RegisteredFieldValue] [nvarchar](max) NULL,
	[IsUpdated] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ClientCompanyCustomFieldValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyCustomFieldes]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyCustomFieldes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentClassFieldTypeId] [int] NOT NULL,
	[Uilabel] [nvarchar](max) NULL,
	[UISort] [int] NULL,
	[DictionaryTypeId] [int] NULL,
	[IsMandatory] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[MinLength] [int] NULL,
	[MaxLength] [int] NULL,
 CONSTRAINT [PK_CompanyCustomFieldes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[DocumentSearchView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


  CREATE View [dbo].[DocumentSearchView] As
                            SELECT 
                            bi.Id As Id
                            ,bi.BatchId As RepositoryName
                            , bi.[FileName]
                            , isNull((
                            select top 1 cl.FirstName+' '+cl.LastName  from Clients cl
                            inner join [Batches] batch on batch.CustomerId = cl.Id
                            where batch.Id = bi.BatchId
                            ),'-') [ClientName]
                            ,docver.Version As FileVersion
                            , dc.DocumentClassName
                            , dtc.DocumentTypeName
                            
                            , isNull((
                            select top 1 su.FullName from BatchMetaHistory bmh
                            inner join SystemUsers su on bmh.SystemUserId = su.Id
                            where bmh.BatchId = bi.BatchId
                            ),'-') [LastModifiedBy]
                            , 
                            case 
                               when ((SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference) <> (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1) And (SELECT  Count(*) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)<>0)
                               then 'Approved Level '+ (SELECT  Cast(Count(*) as varchar) from DocumentApprovalHistory DocAppHis where DocAppHis.BatchItemReference = bi.BatchItemReference And DocAppHis.Approved=1)
                            Else BatItemStatu.BatchItemStatus 
                            End As [DocumentState]
                            , isNull((
                            select top 1 batstatus.ID from BatchItemStatuses batstatus
                            where batstatus.Id = bi.BatchItemStatusId
                            ),'-') [FileStatus]
                            , isNull((
                            select top 1 batstatus.BatchStatus from BatchStatuses batstatus
                            where batstatus.Id = bat.BatchStatusId
                            ),'-') [ClientStatus]
                            , bi.CreatedAt As CreatedOn
                            , bi.UpdatedAt As LastModifiedOn
                            , bi.CreatedAt 
                            , bi.UpdatedAt 
                            , bi.IsActive
							, bi.CompanyId
							, isNull((select top 1 SysRole.[Name] as CurrentReviewRole from SystemRoles as SysRole where Id = (
							select top 1 docAppHis.RoleId from DocumentApprovalHistory docAppHis
							where docAppHis.BatchItemReference = bi.BatchItemReference And docAppHis.Approved = 0
							)),'-') [CurrentReviewRole]
                            , DocVer.[LastModifiedBy] as LastModifiedById
                            ,(select 
							string_agg(DocClasFields.UILabel+':'+meta.FieldValue, ',') from dbo.BatchMeta meta 
							left outer join [DocumentClassFields] DocClasFields on meta.DocumentClassFieldId =DocClasFields.Id
							where 
							meta.DocumentVersionId=(select max(meta.DocumentVersionId) 
							from dbo.BatchMeta meta 
							
							where meta.BatchItemReference = bi.BatchItemReference)
							) AS DocumentMetaData
							,(select 
							string_agg(CompanyCustomFields.UILabel+':'+ meta.RegisteredFieldValue, ',') from dbo.ClientCompanyCustomFieldValues meta 
							left outer join [CompanyCustomFieldes] CompanyCustomFields on meta.FieldId = CompanyCustomFields.Id 
							where meta.ClientId=bat.CustomerId
							) AS ClientMetaData
							,try_Cast((select top 1 ISNULL(meta.FieldValue, ' ') from dbo.BatchMeta meta where meta.BatchItemReference=bi.BatchItemReference and (Select DocumentClassFieldTypeId from DocumentClassFields where Id =meta.DocumentClassFieldId)=10 order by meta.DocumentVersionId desc) as bigint) AS ExpirationDate
                            ,cast((case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then 0 else 1 end) as bit) IsCheckedOut , 
                            (case isnull((select dco.Id from DocumentsCheckedOut dco where dco.BatchItemId = bi.Id), 0) when 0 then null else
                            						 	(select sysuser.FullName FROM SystemUsers sysuser INNER JOIN DocumentsCheckedOut dco ON sysuser.Id=dco.SystemUserId
                            							where dco.BatchItemId=bi.Id )end) as CheckedOutBy
                            FROM [BatchItems] bi
                            left outer join [BatchItemPages] bip on bi.Id = bip.BatchItemId
                            left outer join [BatchItemStatuses] BatItemStatu on BatItemStatu.Id = bi.BatchItemStatusId
                            left outer join [DocumentVersion] docver on docver.Id = bi.DocumentVersionId
                            left outer join [DocumentClasses] dc on bi.DocumentClassId = dc.Id
                            left outer join [DocumentTypes] dtc on dc.DocumentTypeId = dtc.Id
                            left outer join [Batches] bat on bi.BatchId = bat.Id
                            join
                            (
                                SELECT max(id) as id
                                FROM [BatchItems]  
                                GROUP BY [BatchItemReference]
                            ) bi2 on bi.id = bi2.id

     

                                  
GO
/****** Object:  Table [dbo].[UserCompanies]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCompanies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_UserCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SystemUsersView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[SystemUsersView] As
SELECT Distinct su.[Id]
,su.[FullName]
,su.[Email]
,su.[IsActive]
,su.[CreatedAt]
,su.[UpdatedAt]
,(
	select string_agg( ISNULL(c.CompanyName, ' '), ',') from UserCompanies uc inner join Companies c on uc.CompanyId = c.Id where uc.SystemUserId = su.Id	
) [CompanyName],(
	select top 1 CompanyId from UserCompanies uc inner join Companies c on uc.CompanyId = c.Id where uc.SystemUserId = su.Id	
) [CompanyId]
FROM [SystemUsers] su
GO
/****** Object:  View [dbo].[ClientView]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



                                CREATE View [dbo].[ClientView] As
                                
                                SELECT cl.[Id]
                                      ,cl.[FirstName]
                                      ,cl.[LastName]
                                      ,cl.[AFM]
                                      ,cl.[IsActive]
                                      ,cl.[CreatedAt]
                                      ,cl.[UpdatedAt]
                                      ,cl.[IsNotValidForTransaction]
                                      ,cl.[Reason]
                                      ,cl.[GdprdaysToBeKept]
                                      ,cl.[ExternalId]
                                      ,cl.[CompanyId]
                                	  , isNull((
                                select top 1 stat.BatchStatus from BatchStatuses stat
                                where bat.BatchStatusId = stat.Id
                                ),'Created') [ClientStatus]
                                  FROM [DMS].[dbo].[Clients] cl
                                left outer JOIN  [dbo].[Batches] bat on bat.[CustomerId] = cl.[Id]  

                             
                                  
GO
/****** Object:  View [dbo].[ClientsDataToBeDeleted]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE View [dbo].[ClientsDataToBeDeleted] As
								  SELECT B.* , C.id as cId
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.Clients AS C 
												   ON B.CustomerId = C.id
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 2 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.CreatedDate, Getdate()) > C.gdprdaystobekept )
											   ) 
		 
											 )

GO
/****** Object:  View [dbo].[BatchesDataTobeDeleted]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE View [dbo].[BatchesDataTobeDeleted] As
								  SELECT B.*  
									FROM   dbo.batches AS B 
										   INNER JOIN dbo.companies AS C 
												   ON B.companyid = C.id 
									WHERE  ( C.gdprdaystobekept > 0 ) 
										AND (
											  ( 
												   ( B.batchstatusid = 2 ) 
												   AND ( Isnull(B.appliedgdpr, 0) = 0 ) 
												   AND ( Datediff(day, B.CreatedDate, Getdate()) > C.gdprdaystobekept )
											   ) )


GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvancedLogging]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvancedLogging](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[RequestId] [nvarchar](50) NOT NULL,
	[IP] [nvarchar](50) NULL,
	[Browser] [nvarchar](100) NULL,
	[Device] [nvarchar](500) NULL,
	[System] [nvarchar](100) NULL,
	[Controller] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NULL,
	[ActionCompletion] [bit] NULL,
	[Message] [nvarchar](max) NULL,
	[Level] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[RequestURL] [nvarchar](3000) NULL,
	[RequestPayload] [nvarchar](max) NULL,
	[RequestTime] [datetime] NULL,
	[ResponceStatus] [nvarchar](50) NULL,
	[ResponceError] [nvarchar](max) NULL,
	[ResponcePayload] [nvarchar](max) NULL,
	[ResponceTime] [datetime] NULL,
	[ExitDate] [datetime] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_AdvancedLogging] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdvancedSignatureCallHistory]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvancedSignatureCallHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CallBodyInput] [nvarchar](max) NULL,
	[SigningSucceeded] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_AdvancedSignatureCallHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alerts]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alerts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[Msg] [nvarchar](max) NULL,
	[IsRead] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Alerts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplyGDPR]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplyGDPR](
	[requestId] [nvarchar](max) NULL,
	[GDPRStatus] [nvarchar](max) NULL,
	[appliedTime] [datetime2](7) NOT NULL,
	[batchId] [int] NULL,
	[clientId] [int] NULL,
	[clientName] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Audits]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuditDateTimeUtc] [bigint] NOT NULL,
	[AuditType] [nvarchar](30) NOT NULL,
	[AuditUser] [nvarchar](50) NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[KeyValues] [nvarchar](max) NOT NULL,
	[OldValues] [nvarchar](max) NOT NULL,
	[NewValues] [nvarchar](max) NOT NULL,
	[ChangedColumns] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[RequestId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Audits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchesCount]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchesCount](
	[Count] [int] NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[CreatedDate] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchItemFields]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchItemFields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchItemId] [int] NOT NULL,
	[DocumentClassFieldId] [int] NOT NULL,
	[DictionaryValueId] [int] NULL,
	[IsLast] [bit] NOT NULL,
	[RegisteredFieldValue] [nvarchar](2000) NULL,
	[DictionaryValueId_old] [int] NULL,
	[RegisteredFieldValue_old] [nvarchar](255) NULL,
	[IsUpdated] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[DocumentVersionId] [int] NOT NULL,
 CONSTRAINT [PK_BatchItemFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchSourceDocumentsSpecifications]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchSourceDocumentsSpecifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchSourceId] [int] NOT NULL,
	[DocumentClassId] [int] NOT NULL,
	[Description] [nvarchar](300) NULL,
	[IsVirtual] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BatchSourceDocumentsSpecifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchSources]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchSources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchSource] [nvarchar](50) NOT NULL,
	[EnumValue] [nvarchar](50) NOT NULL,
	[BatchSourceCode] [nvarchar](10) NULL,
	[Comments] [nvarchar](50) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BatchSources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BopConfigs]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BopConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Setting] [nvarchar](50) NOT NULL,
	[EnumValue] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BopConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BopDictionaries]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BopDictionaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DictionaryTypeId] [int] NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Code] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BopDictionaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BUs]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_BUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatalogNameProducts]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogNameProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Product] [nvarchar](255) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_CatalogNameProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientTag]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[CreatedById] [int] NULL,
 CONSTRAINT [PK_ClientTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColumnPreferences]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColumnPreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[ScreenId] [int] NOT NULL,
	[ColumnName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ColumnPreferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyAbbyTemplateFields]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyAbbyTemplateFields](
	[CompanyID] [int] NOT NULL,
	[DocumentClassFieldID] [int] NOT NULL,
	[CompanyFlowId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyAbbyTemplates]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyAbbyTemplates](
	[CompanyID] [int] NOT NULL,
	[DocumentClassID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyFlows]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyFlows](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[FlowName] [nvarchar](50) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_CompanyFlows] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanySigningDocuments]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanySigningDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[DocumentType] [nvarchar](50) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_CompanySigningDocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configurations]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PasswordRequireNonAlphanumeric] [bit] NOT NULL,
	[PasswordRequireLowercase] [bit] NOT NULL,
	[PasswordRequireUppercase] [bit] NOT NULL,
	[PasswordRequireDigit] [bit] NOT NULL,
	[PasswordRequiredLength] [int] NOT NULL,
	[RestrictLastUsedPasswords] [int] NOT NULL,
	[ForcePasswordChangeDays] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Code2D] [nvarchar](2) NOT NULL,
	[Code3D] [nvarchar](3) NOT NULL,
	[MobileCode] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DashboardMenu]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DashboardMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[ViewName] [nvarchar](100) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DashboardMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataMigrationHistories]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataMigrationHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](max) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DataMigrationHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataMigrationReordHistories]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataMigrationReordHistories](
	[TableName] [nvarchar](450) NOT NULL,
	[RecordId] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_DataMigrationReordHistories] PRIMARY KEY CLUSTERED 
(
	[TableName] ASC,
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictionaryTypes]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictionaryTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DictionaryType] [nvarchar](50) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DictionaryTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DMSOutLookAddInTempFiles]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DMSOutLookAddInTempFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[OriginalFileName] [nvarchar](max) NULL,
 CONSTRAINT [PK_DMSOutLookAddInTempFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentClassFieldTypes]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentClassFieldTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentClassFieldTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentRejectionReasonCompany]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentRejectionReasonCompany](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DocumentRejectionReasonId] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentRejectionReasonCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentRejectionReasons]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentRejectionReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](70) NULL,
	[Descr] [nvarchar](70) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentRejectionReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentsPerCompanies]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentsPerCompanies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentsPerCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentTypeRoleAccess]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTypeRoleAccess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentTypeRoleAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentTypeRoles]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTypeRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_DocumentTypeRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[License] [nvarchar](50) NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Comments] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LivenessTokens]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LivenessTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Gr] [nvarchar](50) NOT NULL,
	[En] [nvarchar](50) NOT NULL,
	[NumberResult] [nvarchar](5) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_LivenessTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NLog]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[PosId] [int] NULL,
	[Level] [nvarchar](20) NOT NULL,
	[ClassName] [nvarchar](100) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Stacktrace] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[Data] [nvarchar](max) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[LoggedOn] [int] NOT NULL,
	[RequestId] [nvarchar](max) NULL,
 CONSTRAINT [PK_NLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OCREngines]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OCREngines](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_OCREngines] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OCREnginesDocumentClasses]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OCREnginesDocumentClasses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentClassId] [int] NOT NULL,
	[OCREngineId] [int] NOT NULL,
	[OCREngineDocumentClassCode] [nvarchar](50) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_OCREnginesDocumentClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordHistory]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
 CONSTRAINT [PK_PasswordHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Programme_List]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Programme_List](
	[Agreed_Power] [nvarchar](50) NOT NULL,
	[Hron_Programme_ApplicationForm] [nvarchar](150) NOT NULL,
	[Day_Charge] [nvarchar](150) NULL,
	[Night_Charge] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreenColumns]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreenColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ScreenColumnId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreenColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreenElements]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreenElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ScreenElementId] [int] NOT NULL,
	[Privilege] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreenElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleScreens]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleScreens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[ScreenId] [int] NOT NULL,
	[Privilege] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleScreens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rules_xref]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rules_xref](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[DocClassFieldID] [int] NOT NULL,
	[RuleType] [tinyint] NOT NULL,
	[Value] [int] NULL,
	[Formula] [varchar](500) NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Rules_xref] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenColumns]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenId] [int] NOT NULL,
	[ColumnName] [nvarchar](50) NOT NULL,
	[DefaultOrder] [int] NOT NULL,
	[DefaultVisibility] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ScreenColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenElements]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenElementName] [nvarchar](50) NOT NULL,
	[ScreenId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_ScreenElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Screens]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Screens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScreenName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Screens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceLastExcecution]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceLastExcecution](
	[ServiceName] [nvarchar](30) NOT NULL,
	[Time] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stations]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ComputerName] [nvarchar](255) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Stations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationVariables]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationVariables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationId] [int] NULL,
	[StationVariableTypeId] [int] NOT NULL,
	[VariableValue] [nvarchar](255) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_StationVariables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationVariableTypes]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationVariableTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationVariableType] [nvarchar](250) NOT NULL,
	[EnumValue] [nvarchar](250) NOT NULL,
	[Comments] [nvarchar](250) NULL,
	[SupportsGlobal] [bit] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_StationVariableTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserCountries]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserCountries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserCountries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUserRole]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUserId] [int] NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_SystemUserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMissingTemplateFields]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMissingTemplateFields](
	[CompanyID] [int] NOT NULL,
	[DocumentClassFieldID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempBatchPages]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempBatchPages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[KeyFileName] [nvarchar](200) NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[IsActive] [bit] NULL,
	[UpdatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_TempBatchPages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language] [nvarchar](5) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [bigint] NOT NULL,
	[UpdatedAt] [bigint] NOT NULL,
	[GridPageSize] [int] NOT NULL,
	[SystemUserId] [int] NOT NULL,
 CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSessions]    Script Date: 7/7/2022 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSessions](
	[SystemUserId] [int] NOT NULL,
	[SessionId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserSessions] PRIMARY KEY CLUSTERED 
(
	[SystemUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309034533_InitialMigration', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309062102_InitialData', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309080355_addDocumentTypeScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309081400_AddRoleSreenOfDocumentTypeScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309083654_AddScreenElementsOfDocumentTypeScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309084254_AddRoleScreenElementsOfDocumentTypeScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309091645_RemoveSystemUserViewTable', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309091739_RemoveSystemUserViewTableFromDB', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309093548_AddDocumentCategoryTable', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309094021_AddDocumentSubCategoryTable', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309112223_SeedDocumentCategoryAndSubCategoryScreens', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309135127_SeedScreensToSuperAdmin', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310092627_AutoIncrementAddedInDocumentType', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310105713_SeedDocumentClassScreenAndAddRole', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310110501_AddScreenElementOfDocumentClassScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310112018_SeedScreensToSuperAdminUpdate', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310121404_AddColumnsInDocumentClass', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315111516_DictionaryModelChanges', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315141047_EnableIdentityInsert', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315141245_RemovedValueGeneratedNeverForDocumentClassField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325202539_InitialMigration', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325202848_DummyData', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325211355_AddIEntityFields', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325211442_AddSystemUserView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325212115_RemoveCategoryTables', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220325212937_UpdateNameFields', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220326134333_ChangeCustomerToClient', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220328063756_DocumentsCheckedOutAdded', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220328130516_AddDocumentsReviewView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220328132355_AddBatchesCountSP', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220328182428_AddAlertModel', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220328183446_SeedDummyAlerts', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220329065351_ChangeSeededScreens', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220402103712_RemoveUnusedScreens', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220404100117_SeedDocumentSearchScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220405064228_logging_columns', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220405075206_AddClientRepositoryView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220406055256_ChangeNLogColumns', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220406055546_ChangeNLogRemoveColumn', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220406055628_ChangeNLogRenameColumn', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220406060841_ChangeNLogRenameColumn2', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220406063119_AddGdprdaysToBeKeptInClientData', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220407054416_DropColumnsInDocumentClassField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220407060852_AddExternelIdColumnInClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220407102518_AddClientTagEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408081507_AddDocumenTVersionEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408082710_AddDocumenTVersionEntityRelationshipsColumns', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408092131_AddDocumenTVersionEntityRelationshipsData', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408092436_AddDocumenTVersionEntityRelationshipsFK', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220409201634_SetIsActiveTrueForLogs', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220411053119_AddColumnDocumentTypeCode', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220411070957_ChangeColumnDocumentTypeCodeLength', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220411110343_CreateDocumentSearchView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220412151013_AddFilenameColumnInBetchItemEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220412173107_AddCoomentAndLastModifiedByColomunsInDocumentVersion', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220413155950_RemoveCompanyIdFromBatch', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220413163620_RemoveCompanyIdInBatches', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220413165434_removecompanyidfromClientRepositoryView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220414042438_CustomerIdMustBeEnterInBatchhEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220414065232_AddFileNameColmunInDocumentVersionEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220414091534_AddIsCheckoutandIsCheckoutByInDocumentReviewView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220414170155_AddStateAndStausColumnsInDocumentReviewView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220415043857_SetFileNameAndClientNameInDocumentReview_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220416170335_CreateDocumentCheckoutView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418075336_AddUISortColumnInDocumentClassField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418102444_RemoveDuplicateFilenameInDocumentReview_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418104011_SetDocumentSreach_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418112723_UpdateSystemUsersView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418130421_AddFileNameColmunInBatchMetaEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418131547_ChangeDocumentMetaDataInDocumentSreachView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220418150506_SetDocumentSreachViewDocumentMetadata', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419053125_AddCompanyIdInBatches', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419061606_AddDocumentTypeRolesEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419125139_ChangeDocumentCheckoutViewForSortingAndFilter', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419135809_ChangeStateToDocumentStateInDocumentReview_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220420074755_AddLastModifiedByIdInDocumentSreachView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220420075138_AddLastModifiedByIdInDocumentReview_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220420084914_AddDocumentApprovalHistoryEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220421060726_AddCurrentReviewRoleColumnInDocumentReview_View', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220422112306_AddBatchItemRefferenceColumnInBatchItemEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220422160221_AddBatchItemReferenceColmunInEntities', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220422161829_changeDocumentRevie_View_For_BatchItemReference', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220422162553_changeDocumentSearchView_For_BatchItemReference', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220423084321_RemoveUseLessColumns', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220423131804_AddStatusInBatchItemStatusEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220423132012_AddStatusInBatchStatusEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220423173604_SetClientStatusInDocumentSrreachView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220425060352_associateCompanyWithDocument', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220425062253_AddingScreenElements', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220425081240_ResetBatchItemStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220425100756_ScreenElementDuplicationRemove', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220426050022_RemoveExtraStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220426052115_addGDPRViewsAnd', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220426091429_AddClientView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220427043612_ChangeBatchandBatchItemStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220427080647_ResetDocumentStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220427105546_ChangeClientViewe', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220427160414_ResetBatchAndBatchItemStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428054342_AddCompanyToBatchItem', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428054609_ChangeDocumentSreachAndDocumentReviewViewsForDocumentStatus', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428062948_setspellDocumentStatus', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428075955_ChangeDocumentStatusInCheckoutDocumentView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220428130032_AddCompanyId', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220429060553_makecompanyidnotnullableforclient', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220429081434_seedcompanyidtobatchestable', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220429102252_addcompanyIdToDocumentClass', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220429103523_AddSystemRoleInBatchItemEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220429210843_addcompanyidtodocumenttype', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220504080700_ChangeClientView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220506063723_ChangeExternelIdDataTypeInClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220506095847_UpdateApplyGDPRStoredProcedure', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220506100037_UpdateApplyGDPRStoredProcedureImplementation', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220506135042_AddCreatedByIdInClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509072355_AddCurrentReviewColumnInDocumentSreachView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509092513_AddActionColumnInDocumentCheckOutLogEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509095712_RemoveColumnsFromComanyEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509105709_AddDocumentCheckoutLogView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509142201_RemoveActionDocumentCheckoutLogView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220510055912_AddDocumentTypeRoleAccessEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220510134829_AddSpaceInClientNameInDocumentSreachView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511081740_RemoveCDIFromClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511082521_AddDocumentClassFieldNameColumnInBatchMetaEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511083118_AddDocumentClassFieldNameDataInBatchMetaEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511090129_AddDocumentClassFieldUiLabelInBatchItemField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511091820_AddDocumentClassFieldUiLabelDataInBatchItemField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511111448_RemoveDocumentClassFieldUiLabelInBatchItemField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220511124718_AddExpirationDateColumnInDocumentSearchView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220513081620_SetExpirationDateInDocumentSearch', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220513130454_SetExpirationDateupdatedversionshowInDocumentSearch', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220513141416_ClientViewSet', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220516181219_AddDocumentClassFieldCodeInDocumentClassFieldEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220517122207_AddDataToDocumentClassFieldCodes', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220518084911_RemoveColumnsInCompanyEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220518090318_ChangeExternalIdLengthTo50InClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220518092722_ClientTagEntityNameSet', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220518093056_LabelLengthSetTo100InClientTagEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220518110724_AddScreenElentforPreviewAndDocumentClassFieldCodeColumns', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220519151413_RenameValueToCommentsAndLAbelColumnInClientTAGSEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220519175920_AddCompanyCodeInCompanyEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220519195456_SetDocumentSearchViewforMetaDataFilter', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220520092851_AddClientCommentsandAddCompanyCodePrivilage', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220521093323_AddRequestIdColumnInNlogAndAuditEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220523192050_AddCompanyCustomFieldAndClientCompanyCustomFieldValueEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220524070848_AddRequestIdInAduitEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220524164656_RenameEnumvalueColumAndRemoveColumnsInDocumentClassEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220525095643_RenameExternelIdColumnInClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220525101259_AlterClientViewForExternalId', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220525104915_ScreenElementNameChangeForDocumentClassCodeColumnOfDocumentClassEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220525113236_RemoveAFMUniqueIndexInClientEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220530072613_AddDataMigrationHistoryEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220530113221_ChangeCienCompanyCustomFieldValuesEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220530140948_ChangeCienCompanyCustomFieldValuesEntity2', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220614115216_setDocumentFieldTypeFK', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220615064937_SeedScanDocumentSceen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220616180411_AddDMSOutLookAddInTempFileEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220617135018_AddOriginalFileNameColumnInDMSOutLookAddInTempFileEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220620070346_SeedEmailDocumentScreen', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220620134733_AddDataMigrationReordHistoryEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220621054553_RenameVariablesCompanyCustomFiledEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220621064429_AddBasicAuthUserNamePasswordInCompanyEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220625100356_ScanDocumentAndEmailDocumentScreenElementsSeed', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220629125534_ChngeDatatypeandlengthofAFMInCliententityEntity', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220630105314_ChangeBatchesDataTobeDeletedView', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220706142054_ChangeUserPerCompanyDataType', N'5.0.9')
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0C6CA187-942F-4EA5-BB0F-3192C72D8AFD', 2, N'super@mailinator.com', N'SUPER@MAILINATOR.COM', N'super@mailinator.com', N'SUPER@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'82d68add-f6b1-4c57-bfbc-258582bb3c11', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[BatchItemStatuses] ON 

INSERT [dbo].[BatchItemStatuses] ([ID], [BatchItemStatus], [EnumValue], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'Created', N'Created', 0, 1, 0)
INSERT [dbo].[BatchItemStatuses] ([ID], [BatchItemStatus], [EnumValue], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'Checked', N'Checked', 0, 1, 0)
INSERT [dbo].[BatchItemStatuses] ([ID], [BatchItemStatus], [EnumValue], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (3, N'Approved', N'Approved', 0, 1, 0)
INSERT [dbo].[BatchItemStatuses] ([ID], [BatchItemStatus], [EnumValue], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (4, N'Rejected', N'Rejected', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[BatchItemStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchSources] ON 

INSERT [dbo].[BatchSources] ([Id], [BatchSource], [EnumValue], [BatchSourceCode], [Comments], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'Default', N'Default', N'Default', N'Default', 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[BatchSources] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchStatuses] ON 

INSERT [dbo].[BatchStatuses] ([Id], [BatchStatus], [EnumValue], [Description], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'Created', N'Created', N'Created', 0, 1, 0)
INSERT [dbo].[BatchStatuses] ([Id], [BatchStatus], [EnumValue], [Description], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'Checked', N'Checked', N'Checked', 0, 1, 0)
INSERT [dbo].[BatchStatuses] ([Id], [BatchStatus], [EnumValue], [Description], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (3, N'Pending', N'Pending', N'Pending', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[BatchStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[BopDictionaries] ON 

INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, 1, N'Male', NULL, N'Male', NULL, 0, 0)
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, 1, N'Female', NULL, N'Female', NULL, 0, 0)
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, 1, N'Other', NULL, N'Other', NULL, 0, 0)
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, 2, N'Single', NULL, N'Single', NULL, 0, 0)
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, 2, N'Married', NULL, N'Married', NULL, 0, 0)
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, 2, N'Widowed', NULL, N'Widowed', NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[BopDictionaries] OFF
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([Id], [CompanyName], [GDPRDaysToBeKept], [Email], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany], [CompanyCode], [Password], [UserName]) VALUES (2, N'Intelli Solutions', 5, N'int-sol@mailinator.com', 1, 1604403101, 1655370999, N'wG4GuthwAJoQcF07zQ6lGA==', N'Int', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[CompanyCustomFieldes] ON 

INSERT [dbo].[CompanyCustomFieldes] ([Id], [CompanyId], [DocumentClassFieldTypeId], [Uilabel], [UISort], [DictionaryTypeId], [IsMandatory], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (17, 2, 3, N'age', 2, NULL, 1, 1655370677, 0, 1655370999, 2, 6)
SET IDENTITY_INSERT [dbo].[CompanyCustomFieldes] OFF
GO
SET IDENTITY_INSERT [dbo].[Configurations] ON 

INSERT [dbo].[Configurations] ([Id], [PasswordRequireNonAlphanumeric], [PasswordRequireLowercase], [PasswordRequireUppercase], [PasswordRequireDigit], [PasswordRequiredLength], [RestrictLastUsedPasswords], [ForcePasswordChangeDays], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, 1, 1, 1, 1, 8, 3, 15, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Configurations] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [CountryName], [Description], [Code2D], [Code3D], [MobileCode], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Greece', N'', N'GR', N'GRC', N'+30', 1, 0, 0)
INSERT [dbo].[Countries] ([Id], [CountryName], [Description], [Code2D], [Code3D], [MobileCode], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Italy', N'', N'IT', N'ITA', N'+39', 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DictionaryTypes] ON 

INSERT [dbo].[DictionaryTypes] ([Id], [DictionaryType], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'Gender', 0, 1, 0)
INSERT [dbo].[DictionaryTypes] ([Id], [DictionaryType], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'Martial Status', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[DictionaryTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentClassFieldTypes] ON 

INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'SingleLineText', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'MultiLineText', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (3, N'Integer', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (4, N'Decimal', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (5, N'Boolean', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (6, N'Date', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (7, N'Time', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (8, N'DateTime', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (9, N'Dictionary', 0, 1, 0)
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (10, N'Expiration Date', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[DocumentClassFieldTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleScreens] ON 

INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (409, 2, 23, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (410, 2, 21, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (411, 2, 2, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (412, 2, 3, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (413, 2, 4, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (414, 2, 5, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (415, 2, 6, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (416, 2, 7, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (417, 2, 8, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (418, 2, 9, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (419, 2, 22, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (420, 2, 10, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (421, 2, 12, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (422, 2, 13, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (423, 2, 14, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (424, 2, 15, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (425, 2, 17, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (426, 2, 18, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (427, 2, 20, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (428, 2, 11, 2, 1, 1655717529, 1655717529)
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (429, 2, 1, 2, 1, 1655717529, 1655717529)
SET IDENTITY_INSERT [dbo].[RoleScreens] OFF
GO
SET IDENTITY_INSERT [dbo].[ScreenElements] ON 

INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (201, N'Filter', 1, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (202, N'ExportGridData', 1, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (203, N'ExportAllData', 1, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (204, N'Sort', 1, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (205, N'Pagination', 1, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (206, N'Filter', 2, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (207, N'AddNew', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (208, N'Edit', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (209, N'Delete', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (210, N'Filter', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (211, N'Export', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (212, N'ExportGridData', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (213, N'ExportAllData', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (214, N'Sort', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (215, N'Pagination', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (216, N'RoleNameInput', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (217, N'ScreenPrivilegesSelect', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (218, N'RoleNameSubmit', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (219, N'RolePrivilegesSubmit', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (220, N'ElementSubmitButton', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (221, N'RolesElementSelect', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (222, N'CompanySelect', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (223, N'PrioritySelect', 3, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (224, N'AddNew', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (225, N'Edit', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (226, N'Delete', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (227, N'Filter', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (228, N'Export', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (229, N'ExportGridData', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (230, N'ExportAllData', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (231, N'Sort', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (232, N'Pagination', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (233, N'UserNameInput', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (234, N'UserCompanySelect', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (235, N'UserEmail', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (236, N'UserSubmitButton', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (237, N'UserRolesSelect', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (238, N'UserCountriesSelect', 4, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (239, N'AddNew', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (240, N'Edit', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (241, N'Delete', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (242, N'Filter', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (243, N'Export', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (244, N'ExportGridData', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (245, N'ExportAllData', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (246, N'Sort', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (247, N'Pagination', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (248, N'CompanyNameInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (249, N'CallBackUrlInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (250, N'Slaimportance', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (251, N'CompanyEmailInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (252, N'IsSignedCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (253, N'SendRejectetionCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (254, N'SendLinkCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (255, N'SupportCallCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (256, N'VideoCallBackInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (257, N'IsActiveCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (258, N'CompanySubmit', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (259, N'HawkAppIdInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (260, N'HawkUserInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (261, N'HawkSecretInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (262, N'FtpHostNameInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (263, N'FtpUserNameInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (264, N'FtpPasswordInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (265, N'FtpDirectoryInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (266, N'RetriesWhenFailPublishedInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (267, N'GdprdaysToBeKeptInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (268, N'CodeInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (269, N'FtpPortInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (270, N'FTpUserSecureProtocolCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (271, N'FtpActiveCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (272, N'FtpResponseHostNameInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (273, N'FtpResponseUserNameInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (274, N'FtpResponsePasswordInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (275, N'FtpResponsePortInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (276, N'FtpResponseUserSecureProtocolInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (277, N'FtpResponseActiveCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (278, N'FtpResponseDirectoryInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (279, N'FtpResponseCheckbox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (280, N'SimilarityThresholdInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (281, N'EnableCheckBox', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (282, N'SlaminutesInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (283, N'SlabBatchQuantityInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (284, N'LogoInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (285, N'MaxCallInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (286, N'AgentControllerInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (287, N'CustomerRetriesInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (288, N'SmsProviderInput', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (289, N'UserPerCompany', 5, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (290, N'GridPageSize', 6, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (291, N'PreferencesSubmit', 6, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (292, N'PasswordRequireNonAlphanumericCheckBox', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (293, N'PasswordRequireLowercaseCheckBox', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (294, N'PasswordRequireUppercaseCheckBox', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (295, N'PasswordRequireDigitCheckBox', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (296, N'PasswordRequiredLengthCheckInput', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (297, N'RestrictLastUsedPasswordsInput', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (298, N'ForcePasswordChangeDaysInput', 10, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (299, N'PasswordPolicySubmit', 10, 1, 1650881914, 1650881914)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (300, N'AddNew', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (301, N'Edit', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (302, N'Delete', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (303, N'Filter', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (304, N'Export', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (305, N'ExportGridData', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (306, N'ExportAllData', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (307, N'Sort', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (308, N'Pagination', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (309, N'DocumentTypeNameInput', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (310, N'DocumentTypeCodeInput', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (311, N'RolesCheckbox', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (312, N'DocumentTypeSubmit', 11, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (313, N'Edit', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (314, N'CheckIn', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (315, N'Filter', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (316, N'Export', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (317, N'ExportGridData', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (318, N'ExportAllData', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (319, N'Sort', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (320, N'Pagination', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (321, N'ChooseClientSelect', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (322, N'ChooseDocumentClassSelect', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (323, N'EditDocumentUpdate', 12, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (324, N'DocumentExport', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (325, N'DocumentSearchCheckOut', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (326, N'Filter', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (327, N'Export', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (328, N'ExportGridData', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (329, N'ExportAllData', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (330, N'Sort', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (331, N'Pagination', 13, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (332, N'AddNew', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (333, N'Edit', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (334, N'Delete', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (335, N'Filter', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (336, N'Export', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (337, N'ExportGridData', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (338, N'ExportAllData', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (339, N'Sort', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (340, N'Pagination', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (341, N'DocumentClassInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (342, N'DocumentClassCodeInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (343, N'RecognitionMappedNameInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (344, N'GroupMandatoryNameInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (345, N'DocumentTypeNameSelect', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (346, N'AddNewDocumentClass', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (347, N'UiLabelInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (348, N'UiSortInput', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (349, N'DocumentClassFieldTypeSelect', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (350, N'IsMandatoryCheckbox', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (351, N'DocumentClassFieldSubmit', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (352, N'DocumentClassSubmit', 14, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (353, N'ChooseDocumentBrowse', 15, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (354, N'ChooseClientSelect', 15, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (355, N'ChooseDocumentClassSelect', 15, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (356, N'DocumentUploadSubmit', 15, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (357, N'Filter', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (358, N'Export', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (359, N'ExportGridData', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (360, N'ExportAllData', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (361, N'Sort', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (362, N'Pagination', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (363, N'Preview', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (364, N'ClientDetailHistory', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (365, N'ClientDetailCheckOut', 17, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (366, N'Filter', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (367, N'Export', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (368, N'ExportGridData', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (369, N'ExportAllData', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (370, N'Sort', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (371, N'Pagination', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (372, N'DocumentReviewCheckOut', 18, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (373, N'Filter', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (374, N'Export', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (375, N'ExportGridData', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (376, N'ExportAllData', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (377, N'Sort', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (378, N'Pagination', 20, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (379, N'AddNew', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (380, N'Edit', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (381, N'Delete', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (382, N'Filter', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (383, N'Export', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (384, N'ExportGridData', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (385, N'ExportAllData', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (386, N'Sort', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (387, N'Pagination', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (388, N'FirstNameInput', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (389, N'LastNameInput', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (390, N'AFMInput', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (391, N'CDIInput', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (392, N'ExternalIdInput', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (393, N'IsNotValidForTransactionCheckbox', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (394, N'AddNewTag', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (395, N'ClientComments', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (396, N'SubmitTag', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (397, N'SubmitClient', 21, 1, 1650881914, 1650881914)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (398, N'DocumentSearchPreview', 13, 1, 1652875310, 1652875310)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (399, N'DocumentClassFieldCode', 14, 1, 1652875310, 1652875310)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (400, N'CompanyCodeInput', 5, 1, 1653039445, 1653039445)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (401, N'ScanDocument', 22, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (402, N'RemoveScannedDocument', 22, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (403, N'RemoveAllScannedDocument', 22, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (404, N'SaveScannedDocument', 22, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (405, N'Filter', 23, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (406, N'Export', 23, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (407, N'DocumentUpload', 23, 1, 1656152292, 1656152292)
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (408, N'Delete', 23, 1, 1656152292, 1656152292)
SET IDENTITY_INSERT [dbo].[ScreenElements] OFF
GO
SET IDENTITY_INSERT [dbo].[Screens] ON 

INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Audit', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Reporting', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Roles', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Users', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Company', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Preferences', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'UserCountries', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'UserRoles', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'DocumentsPerCompany', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'Configurations', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, N'DocumentType', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, N'CheckedOutDocuments', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, N'DocumentSearch', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (14, N'DocumentClass', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, N'DocumentUpload', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (17, N'ClientRepository', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (18, N'DocumentReview', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (20, N'ExportLogs', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (21, N'Client', 1, 0, 0)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (22, N'ScanDocument', 1, 1655279645, 1655279645)
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (23, N'EmailedDocuments', 1, 1655710675, 1655710675)
SET IDENTITY_INSERT [dbo].[Screens] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemRoles] ON 

INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (2, N'superAdmin', 1, 1604403101, 1655717528, 2, 1)
SET IDENTITY_INSERT [dbo].[SystemRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUserRole] ON 

INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (60, 2, 2, 1, 1655372904, 1655372904)
SET IDENTITY_INSERT [dbo].[SystemUserRole] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] ON 

INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'super@mailinator.com', N'super@mailinator.com', NULL, 1, 0, 1655372904)
SET IDENTITY_INSERT [dbo].[SystemUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[UserPreferences] ON 

INSERT [dbo].[UserPreferences] ([Id], [Language], [IsActive], [CreatedAt], [UpdatedAt], [GridPageSize], [SystemUserId]) VALUES (5, N'en-US', 1, 1656074106, 1656074134, 10, 2)
SET IDENTITY_INSERT [dbo].[UserPreferences] OFF
GO
ALTER TABLE [dbo].[AdvancedLogging] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AdvancedLogging] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[AdvancedSignatureCallHistory] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[AdvancedSignatureCallHistory] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[ApplyGDPR] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [appliedTime]
GO
ALTER TABLE [dbo].[Audits] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Batches] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Batches] ADD  DEFAULT ((1)) FOR [BatchSourceId]
GO
ALTER TABLE [dbo].[Batches] ADD  DEFAULT ((0)) FOR [RetriesCount]
GO
ALTER TABLE [dbo].[Batches] ADD  DEFAULT ((0)) FOR [AppliedGDPR]
GO
ALTER TABLE [dbo].[BatchItemFields] ADD  DEFAULT ((1)) FOR [IsLast]
GO
ALTER TABLE [dbo].[BatchItemFields] ADD  DEFAULT ((0)) FOR [DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItemPages] ADD  DEFAULT ((0)) FOR [DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItems] ADD  DEFAULT ((0)) FOR [DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItems] ADD  DEFAULT ((0)) FOR [CompanyId]
GO
ALTER TABLE [dbo].[BatchItemStatuses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BatchItemStatuses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BatchMeta] ADD  DEFAULT ((0)) FOR [DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchMetaHistory] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BatchMetaHistory] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BatchSources] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BatchSources] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BatchStatuses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BatchStatuses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BopConfigs] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BopConfigs] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[BopDictionaries] ADD  DEFAULT ((1)) FOR [Code]
GO
ALTER TABLE [dbo].[BUs] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[BUs] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[CatalogNameProducts] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CatalogNameProducts] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((0)) FOR [GdprdaysToBeKept]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [CompanyId]
GO
ALTER TABLE [dbo].[CompanyFlows] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CompanyFlows] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[CompanySigningDocuments] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CompanySigningDocuments] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DashboardMenu] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DashboardMenu] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DictionaryTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DictionaryTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentClasses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DocumentClasses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentClasses] ADD  DEFAULT ((1)) FOR [CompanyId]
GO
ALTER TABLE [dbo].[DocumentClassFields] ADD  DEFAULT ((1)) FOR [DocumentClassFieldTypeId]
GO
ALTER TABLE [dbo].[DocumentClassFields] ADD  DEFAULT ('') FOR [UILabel]
GO
ALTER TABLE [dbo].[DocumentClassFields] ADD  DEFAULT ((1)) FOR [IsMandatory]
GO
ALTER TABLE [dbo].[DocumentClassFields] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DocumentClassFields] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentClassFieldTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DocumentClassFieldTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentRejectionReasons] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DocumentRejectionReasons] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[DocumentTypes] ADD  DEFAULT ((1)) FOR [CompanyId]
GO
ALTER TABLE [dbo].[LivenessTokens] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LivenessTokens] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[NLog] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[NLog] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[NLog] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[NLog] ADD  DEFAULT ((0)) FOR [LoggedOn]
GO
ALTER TABLE [dbo].[OCREngines] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[OCREngines] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Rules_xref] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Rules_xref] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Stations] ADD  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[Stations] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Stations] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[StationVariables] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[StationVariables] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[StationVariableTypes] ADD  DEFAULT ((1)) FOR [SupportsGlobal]
GO
ALTER TABLE [dbo].[StationVariableTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[StationVariableTypes] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[TempBatchPages] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TempBatchPages] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[AdvancedSignatureCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_AdvancedSignatureCallHistory_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdvancedSignatureCallHistory] CHECK CONSTRAINT [FK_AdvancedSignatureCallHistory_Companies_CompanyId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_Batches_BatchSources] FOREIGN KEY([BatchSourceId])
REFERENCES [dbo].[BatchSources] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_Batches_BatchSources]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_Batches_BatchStatuses1] FOREIGN KEY([BatchStatusId])
REFERENCES [dbo].[BatchStatuses] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_Batches_BatchStatuses1]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_Batches_BUs] FOREIGN KEY([BusinessUnitId])
REFERENCES [dbo].[BUs] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_Batches_BUs]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_Batches_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_Batches_Companies_CompanyId]
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD  CONSTRAINT [FK_Batches_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Batches] CHECK CONSTRAINT [FK_Batches_Customers]
GO
ALTER TABLE [dbo].[BatchItemFields]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemFields_BatchItems] FOREIGN KEY([BatchItemId])
REFERENCES [dbo].[BatchItems] ([Id])
GO
ALTER TABLE [dbo].[BatchItemFields] CHECK CONSTRAINT [FK_BatchItemFields_BatchItems]
GO
ALTER TABLE [dbo].[BatchItemFields]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemFields_BopDictionaries] FOREIGN KEY([DictionaryValueId])
REFERENCES [dbo].[BopDictionaries] ([Id])
GO
ALTER TABLE [dbo].[BatchItemFields] CHECK CONSTRAINT [FK_BatchItemFields_BopDictionaries]
GO
ALTER TABLE [dbo].[BatchItemFields]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemFields_DocumentClassFields] FOREIGN KEY([DocumentClassFieldId])
REFERENCES [dbo].[DocumentClassFields] ([Id])
GO
ALTER TABLE [dbo].[BatchItemFields] CHECK CONSTRAINT [FK_BatchItemFields_DocumentClassFields]
GO
ALTER TABLE [dbo].[BatchItemFields]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemFields_DocumentVersion_DocumentVersionId] FOREIGN KEY([DocumentVersionId])
REFERENCES [dbo].[DocumentVersion] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchItemFields] CHECK CONSTRAINT [FK_BatchItemFields_DocumentVersion_DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItemPages]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemPages_BatchItems] FOREIGN KEY([BatchItemId])
REFERENCES [dbo].[BatchItems] ([Id])
GO
ALTER TABLE [dbo].[BatchItemPages] CHECK CONSTRAINT [FK_BatchItemPages_BatchItems]
GO
ALTER TABLE [dbo].[BatchItemPages]  WITH CHECK ADD  CONSTRAINT [FK_BatchItemPages_DocumentVersion_DocumentVersionId] FOREIGN KEY([DocumentVersionId])
REFERENCES [dbo].[DocumentVersion] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchItemPages] CHECK CONSTRAINT [FK_BatchItemPages_DocumentVersion_DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_Batches] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([Id])
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_Batches]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_BatchItems_PARENT] FOREIGN KEY([ParentId])
REFERENCES [dbo].[BatchItems] ([Id])
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_BatchItems_PARENT]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_BatchItemStatuses] FOREIGN KEY([BatchItemStatusId])
REFERENCES [dbo].[BatchItemStatuses] ([ID])
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_BatchItemStatuses]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_Companies_CompanyId]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_DocumentClasses] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_DocumentClasses]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_DocumentVersion_DocumentVersionId] FOREIGN KEY([DocumentVersionId])
REFERENCES [dbo].[DocumentVersion] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_DocumentVersion_DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchItems_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
GO
ALTER TABLE [dbo].[BatchItems] CHECK CONSTRAINT [FK_BatchItems_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[BatchMeta]  WITH CHECK ADD  CONSTRAINT [FK_BatchMeta_Batches] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([Id])
GO
ALTER TABLE [dbo].[BatchMeta] CHECK CONSTRAINT [FK_BatchMeta_Batches]
GO
ALTER TABLE [dbo].[BatchMeta]  WITH CHECK ADD  CONSTRAINT [FK_BatchMeta_Dictionaries] FOREIGN KEY([DictionaryValueId])
REFERENCES [dbo].[BopDictionaries] ([Id])
GO
ALTER TABLE [dbo].[BatchMeta] CHECK CONSTRAINT [FK_BatchMeta_Dictionaries]
GO
ALTER TABLE [dbo].[BatchMeta]  WITH CHECK ADD  CONSTRAINT [FK_BatchMeta_DocumentClassFields] FOREIGN KEY([DocumentClassFieldId])
REFERENCES [dbo].[DocumentClassFields] ([Id])
GO
ALTER TABLE [dbo].[BatchMeta] CHECK CONSTRAINT [FK_BatchMeta_DocumentClassFields]
GO
ALTER TABLE [dbo].[BatchMeta]  WITH CHECK ADD  CONSTRAINT [FK_BatchMeta_DocumentVersion_DocumentVersionId] FOREIGN KEY([DocumentVersionId])
REFERENCES [dbo].[DocumentVersion] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchMeta] CHECK CONSTRAINT [FK_BatchMeta_DocumentVersion_DocumentVersionId]
GO
ALTER TABLE [dbo].[BatchMetaHistory]  WITH CHECK ADD  CONSTRAINT [FK_BatchMetaHistory_Batches] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([Id])
GO
ALTER TABLE [dbo].[BatchMetaHistory] CHECK CONSTRAINT [FK_BatchMetaHistory_Batches]
GO
ALTER TABLE [dbo].[BatchMetaHistory]  WITH CHECK ADD  CONSTRAINT [FK_BatchMetaHistory_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
GO
ALTER TABLE [dbo].[BatchMetaHistory] CHECK CONSTRAINT [FK_BatchMetaHistory_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications]  WITH CHECK ADD  CONSTRAINT [FK_BatchSourceDocumentsSpecifications_BatchSources] FOREIGN KEY([BatchSourceId])
REFERENCES [dbo].[BatchSources] ([Id])
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications] CHECK CONSTRAINT [FK_BatchSourceDocumentsSpecifications_BatchSources]
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications]  WITH CHECK ADD  CONSTRAINT [FK_BatchSourceDocumentsSpecifications_DocumentClasses] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
GO
ALTER TABLE [dbo].[BatchSourceDocumentsSpecifications] CHECK CONSTRAINT [FK_BatchSourceDocumentsSpecifications_DocumentClasses]
GO
ALTER TABLE [dbo].[BopDictionaries]  WITH CHECK ADD  CONSTRAINT [FK_Dictionaries_DictionaryTypes] FOREIGN KEY([DictionaryTypeId])
REFERENCES [dbo].[DictionaryTypes] ([Id])
GO
ALTER TABLE [dbo].[BopDictionaries] CHECK CONSTRAINT [FK_Dictionaries_DictionaryTypes]
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_ClientCompanyCustomFieldValues_BopDictionaries_DictionaryValueId] FOREIGN KEY([DictionaryValueId])
REFERENCES [dbo].[BopDictionaries] ([Id])
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues] CHECK CONSTRAINT [FK_ClientCompanyCustomFieldValues_BopDictionaries_DictionaryValueId]
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_ClientCompanyCustomFieldValues_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues] CHECK CONSTRAINT [FK_ClientCompanyCustomFieldValues_Clients_ClientId]
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_ClientCompanyCustomFieldValues_CompanyCustomFieldes_FieldId] FOREIGN KEY([FieldId])
REFERENCES [dbo].[CompanyCustomFieldes] ([Id])
GO
ALTER TABLE [dbo].[ClientCompanyCustomFieldValues] CHECK CONSTRAINT [FK_ClientCompanyCustomFieldValues_CompanyCustomFieldes_FieldId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Companies_CompanyId]
GO
ALTER TABLE [dbo].[ClientTag]  WITH CHECK ADD  CONSTRAINT [FK_ClientTag_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClientTag] CHECK CONSTRAINT [FK_ClientTag_Clients_ClientId]
GO
ALTER TABLE [dbo].[ClientTag]  WITH CHECK ADD  CONSTRAINT [FK_ClientTag_SystemUsers_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[SystemUsers] ([Id])
GO
ALTER TABLE [dbo].[ClientTag] CHECK CONSTRAINT [FK_ClientTag_SystemUsers_CreatedById]
GO
ALTER TABLE [dbo].[ColumnPreferences]  WITH CHECK ADD  CONSTRAINT [FK_ColumnPreferences_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColumnPreferences] CHECK CONSTRAINT [FK_ColumnPreferences_Screens_ScreenId]
GO
ALTER TABLE [dbo].[ColumnPreferences]  WITH CHECK ADD  CONSTRAINT [FK_ColumnPreferences_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ColumnPreferences] CHECK CONSTRAINT [FK_ColumnPreferences_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[CompanyAbbyTemplateFields]  WITH CHECK ADD  CONSTRAINT [FK__CompanyAb__Compa__3587F3E0] FOREIGN KEY([CompanyFlowId])
REFERENCES [dbo].[CompanyFlows] ([ID])
GO
ALTER TABLE [dbo].[CompanyAbbyTemplateFields] CHECK CONSTRAINT [FK__CompanyAb__Compa__3587F3E0]
GO
ALTER TABLE [dbo].[CompanyAbbyTemplates]  WITH CHECK ADD  CONSTRAINT [FK_CompanyAbbyTemplates_Companies] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[CompanyAbbyTemplates] CHECK CONSTRAINT [FK_CompanyAbbyTemplates_Companies]
GO
ALTER TABLE [dbo].[CompanyAbbyTemplates]  WITH CHECK ADD  CONSTRAINT [FK_CompanyAbbyTemplates_DocumentClasses] FOREIGN KEY([DocumentClassID])
REFERENCES [dbo].[DocumentClasses] ([Id])
GO
ALTER TABLE [dbo].[CompanyAbbyTemplates] CHECK CONSTRAINT [FK_CompanyAbbyTemplates_DocumentClasses]
GO
ALTER TABLE [dbo].[CompanyCustomFieldes]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCustomFieldes_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyCustomFieldes] CHECK CONSTRAINT [FK_CompanyCustomFieldes_Companies_CompanyId]
GO
ALTER TABLE [dbo].[CompanyCustomFieldes]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCustomFieldes_DictionaryTypes_DictionaryTypeId] FOREIGN KEY([DictionaryTypeId])
REFERENCES [dbo].[DictionaryTypes] ([Id])
GO
ALTER TABLE [dbo].[CompanyCustomFieldes] CHECK CONSTRAINT [FK_CompanyCustomFieldes_DictionaryTypes_DictionaryTypeId]
GO
ALTER TABLE [dbo].[CompanyCustomFieldes]  WITH CHECK ADD  CONSTRAINT [FK_CompanyCustomFieldes_DocumentClassFieldTypes_DocumentClassFieldTypeId] FOREIGN KEY([DocumentClassFieldTypeId])
REFERENCES [dbo].[DocumentClassFieldTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompanyCustomFieldes] CHECK CONSTRAINT [FK_CompanyCustomFieldes_DocumentClassFieldTypes_DocumentClassFieldTypeId]
GO
ALTER TABLE [dbo].[DMSOutLookAddInTempFiles]  WITH CHECK ADD  CONSTRAINT [FK_DMSOutLookAddInTempFiles_SystemUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DMSOutLookAddInTempFiles] CHECK CONSTRAINT [FK_DMSOutLookAddInTempFiles_SystemUsers_UserId]
GO
ALTER TABLE [dbo].[DocumentApprovalHistory]  WITH CHECK ADD  CONSTRAINT [FK_DocumentApprovalHistory_SystemRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentApprovalHistory] CHECK CONSTRAINT [FK_DocumentApprovalHistory_SystemRoles_RoleId]
GO
ALTER TABLE [dbo].[DocumentClasses]  WITH CHECK ADD  CONSTRAINT [FK_DocumentClasses_DocumentTypes] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentTypes] ([Id])
GO
ALTER TABLE [dbo].[DocumentClasses] CHECK CONSTRAINT [FK_DocumentClasses_DocumentTypes]
GO
ALTER TABLE [dbo].[DocumentClassFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentClassFields_DictionaryTypes] FOREIGN KEY([DictionaryTypeId])
REFERENCES [dbo].[DictionaryTypes] ([Id])
GO
ALTER TABLE [dbo].[DocumentClassFields] CHECK CONSTRAINT [FK_DocumentClassFields_DictionaryTypes]
GO
ALTER TABLE [dbo].[DocumentClassFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentClassFields_DocumentClasses] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
GO
ALTER TABLE [dbo].[DocumentClassFields] CHECK CONSTRAINT [FK_DocumentClassFields_DocumentClasses]
GO
ALTER TABLE [dbo].[DocumentClassFields]  WITH CHECK ADD  CONSTRAINT [FK_DocumentClassFields_DocumentClassFieldTypes] FOREIGN KEY([DocumentClassFieldTypeId])
REFERENCES [dbo].[DocumentClassFieldTypes] ([Id])
GO
ALTER TABLE [dbo].[DocumentClassFields] CHECK CONSTRAINT [FK_DocumentClassFields_DocumentClassFieldTypes]
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany]  WITH CHECK ADD  CONSTRAINT [FK_DocumentRejectionReasonCompany_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany] CHECK CONSTRAINT [FK_DocumentRejectionReasonCompany_Companies]
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany]  WITH CHECK ADD  CONSTRAINT [FK_DocumentRejectionReasonCompany_DocumentRejectionReasons] FOREIGN KEY([DocumentRejectionReasonId])
REFERENCES [dbo].[DocumentRejectionReasons] ([Id])
GO
ALTER TABLE [dbo].[DocumentRejectionReasonCompany] CHECK CONSTRAINT [FK_DocumentRejectionReasonCompany_DocumentRejectionReasons]
GO
ALTER TABLE [dbo].[DocumentsCheckedOut]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsCheckedOut_BatchItems_BatchItemId] FOREIGN KEY([BatchItemId])
REFERENCES [dbo].[BatchItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsCheckedOut] CHECK CONSTRAINT [FK_DocumentsCheckedOut_BatchItems_BatchItemId]
GO
ALTER TABLE [dbo].[DocumentsCheckedOut]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsCheckedOut_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsCheckedOut] CHECK CONSTRAINT [FK_DocumentsCheckedOut_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[DocumentsCheckedOutLogs]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsCheckedOutLogs_BatchItems_BatchItemId] FOREIGN KEY([BatchItemId])
REFERENCES [dbo].[BatchItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsCheckedOutLogs] CHECK CONSTRAINT [FK_DocumentsCheckedOutLogs_BatchItems_BatchItemId]
GO
ALTER TABLE [dbo].[DocumentsCheckedOutLogs]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsCheckedOutLogs_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsCheckedOutLogs] CHECK CONSTRAINT [FK_DocumentsCheckedOutLogs_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[DocumentsPerCompanies]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsPerCompanies_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsPerCompanies] CHECK CONSTRAINT [FK_DocumentsPerCompanies_Companies_CompanyId]
GO
ALTER TABLE [dbo].[DocumentsPerCompanies]  WITH CHECK ADD  CONSTRAINT [FK_DocumentsPerCompanies_DocumentClasses_DocumentClassId] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentsPerCompanies] CHECK CONSTRAINT [FK_DocumentsPerCompanies_DocumentClasses_DocumentClassId]
GO
ALTER TABLE [dbo].[DocumentTypeRoleAccess]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeRoleAccess_DocumentTypes_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentTypeRoleAccess] CHECK CONSTRAINT [FK_DocumentTypeRoleAccess_DocumentTypes_DocumentTypeId]
GO
ALTER TABLE [dbo].[DocumentTypeRoleAccess]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeRoleAccess_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentTypeRoleAccess] CHECK CONSTRAINT [FK_DocumentTypeRoleAccess_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[DocumentTypeRoles]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeRoles_DocumentTypes_DocumentTypeId] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentTypeRoles] CHECK CONSTRAINT [FK_DocumentTypeRoles_DocumentTypes_DocumentTypeId]
GO
ALTER TABLE [dbo].[DocumentTypeRoles]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeRoles_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentTypeRoles] CHECK CONSTRAINT [FK_DocumentTypeRoles_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses]  WITH CHECK ADD  CONSTRAINT [FK_OCREnginesDocumentClasses_DocumentClasses] FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[DocumentClasses] ([Id])
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses] CHECK CONSTRAINT [FK_OCREnginesDocumentClasses_DocumentClasses]
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses]  WITH CHECK ADD  CONSTRAINT [FK_OCREnginesDocumentClasses_OCREngines] FOREIGN KEY([OCREngineId])
REFERENCES [dbo].[OCREngines] ([ID])
GO
ALTER TABLE [dbo].[OCREnginesDocumentClasses] CHECK CONSTRAINT [FK_OCREnginesDocumentClasses_OCREngines]
GO
ALTER TABLE [dbo].[PasswordHistory]  WITH CHECK ADD  CONSTRAINT [FK_PasswordHistory_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PasswordHistory] CHECK CONSTRAINT [FK_PasswordHistory_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[RoleScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenColumns_ScreenColumns_ScreenColumnId] FOREIGN KEY([ScreenColumnId])
REFERENCES [dbo].[ScreenColumns] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenColumns] CHECK CONSTRAINT [FK_RoleScreenColumns_ScreenColumns_ScreenColumnId]
GO
ALTER TABLE [dbo].[RoleScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenColumns] CHECK CONSTRAINT [FK_RoleScreenColumns_SystemRoles_RoleId]
GO
ALTER TABLE [dbo].[RoleScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenElements_ScreenElements_ScreenElementId] FOREIGN KEY([ScreenElementId])
REFERENCES [dbo].[ScreenElements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenElements] CHECK CONSTRAINT [FK_RoleScreenElements_ScreenElements_ScreenElementId]
GO
ALTER TABLE [dbo].[RoleScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreenElements] CHECK CONSTRAINT [FK_RoleScreenElements_SystemRoles_RoleId]
GO
ALTER TABLE [dbo].[RoleScreens]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreens_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreens] CHECK CONSTRAINT [FK_RoleScreens_Screens_ScreenId]
GO
ALTER TABLE [dbo].[RoleScreens]  WITH CHECK ADD  CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleScreens] CHECK CONSTRAINT [FK_RoleScreens_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[ScreenColumns]  WITH CHECK ADD  CONSTRAINT [FK_ScreenColumns_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScreenColumns] CHECK CONSTRAINT [FK_ScreenColumns_Screens_ScreenId]
GO
ALTER TABLE [dbo].[ScreenElements]  WITH CHECK ADD  CONSTRAINT [FK_ScreenElements_Screens_ScreenId] FOREIGN KEY([ScreenId])
REFERENCES [dbo].[Screens] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScreenElements] CHECK CONSTRAINT [FK_ScreenElements_Screens_ScreenId]
GO
ALTER TABLE [dbo].[StationVariables]  WITH CHECK ADD  CONSTRAINT [FK_StationVariables_Stations] FOREIGN KEY([StationId])
REFERENCES [dbo].[Stations] ([Id])
GO
ALTER TABLE [dbo].[StationVariables] CHECK CONSTRAINT [FK_StationVariables_Stations]
GO
ALTER TABLE [dbo].[StationVariables]  WITH CHECK ADD  CONSTRAINT [FK_StationVariables_StationVariableTypes] FOREIGN KEY([StationVariableTypeId])
REFERENCES [dbo].[StationVariableTypes] ([Id])
GO
ALTER TABLE [dbo].[StationVariables] CHECK CONSTRAINT [FK_StationVariables_StationVariableTypes]
GO
ALTER TABLE [dbo].[SystemRoles]  WITH CHECK ADD  CONSTRAINT [FK_SystemRoles_company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRoles] CHECK CONSTRAINT [FK_SystemRoles_company]
GO
ALTER TABLE [dbo].[SystemUserCountries]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCountries_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCountries] CHECK CONSTRAINT [FK_SystemUserCountries_Countries_CountryId]
GO
ALTER TABLE [dbo].[SystemUserCountries]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserCountries_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserCountries] CHECK CONSTRAINT [FK_SystemUserCountries_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[SystemUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRole_SystemRoles_SystemRoleId] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserRole] CHECK CONSTRAINT [FK_SystemUserRole_SystemRoles_SystemRoleId]
GO
ALTER TABLE [dbo].[SystemUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserRole_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemUserRole] CHECK CONSTRAINT [FK_SystemUserRole_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[UserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_Companyies_Companyid] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO
ALTER TABLE [dbo].[UserCompanies] CHECK CONSTRAINT [FK_Companyies_Companyid]
GO
ALTER TABLE [dbo].[UserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
GO
ALTER TABLE [dbo].[UserCompanies] CHECK CONSTRAINT [FK_SystemUserId]
GO
ALTER TABLE [dbo].[UserPreferences]  WITH CHECK ADD  CONSTRAINT [FK_UserPreferences_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPreferences] CHECK CONSTRAINT [FK_UserPreferences_SystemUsers_SystemUserId]
GO
ALTER TABLE [dbo].[UserSessions]  WITH CHECK ADD  CONSTRAINT [FK_UserSessions_SystemUsers_SystemUserId] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSessions] CHECK CONSTRAINT [FK_UserSessions_SystemUsers_SystemUserId]
GO
/****** Object:  StoredProcedure [dbo].[batches_count]    Script Date: 7/7/2022 4:57:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[batches_count]
	@pCompanyId int = 0,
	@pFromDate char(10) = '2020-05-02',
	@pToDate char(10) = '2020-06-25'
AS
BEGIN
	SET NOCOUNT ON;

	SELECT count(*) [Count]
	, tbl.[CompanyName]
	, tbl.[CreatedDate]
	FROM (
		SELECT b.Id
		, c.[CompanyName]
		, convert(varchar, b.[CreatedDate], 101 /*[mm/dd/yyyy]*/) [CreatedDate]
		, convert(varchar, b.[CreatedDate], 111 /*[yyyy/mm/dd]*/) [CreatedDateForSort]
		FROM [Batches] b
		INNER JOIN [Companies] c ON b.[CompanyId] = c.[Id]
		WHERE (@pCompanyId = 0 OR b.CompanyId = @pCompanyId)
		AND (b.[CreatedDate] BETWEEN @pFromDate + ' 00:00:00.000' AND @pToDate + ' 23:59:59.999')
	) tbl
	GROUP BY tbl.[CompanyName]
	, tbl.[CreatedDate]
	, tbl.[CreatedDateForSort]
	ORDER BY tbl.[CreatedDateForSort]
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ApplyGDPR]    Script Date: 7/7/2022 4:57:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

				CREATE PROCEDURE [dbo].[sp_ApplyGDPR]

				AS
				declare @Count int
				declare @requestId nvarchar(50)
				
				create table #tempBatch (GDPRStatus VARCHAR(50),batchId int,requestId nvarchar(50),
							clientId int, clientName VARCHAR(50), appliedTime datetime)
							

				BEGIN

					SET NOCOUNT ON;
							Declare @BatchId int = 0;
							Declare @TempClientId int = 0;
							Declare @ClientId int = 0;
							Declare @ClientName VARCHAR(50)
							Declare @GdprStatus VARCHAR(50)
							Declare @AppliedTime datetime=getdate()
							
					  BEGIN TRY		
							
						set	@Count= (select count(*) from ClientsDataToBeDeleted)
						if (@Count>0)
						begin
						DECLARE batch_cursor CURSOR FOR
						SELECT RequestId
						FROM ClientsDataToBeDeleted
                     	end

						else
						begin
						set	@Count= (select count(*) from BatchesDataTobeDeleted)
						DECLARE batch_cursor CURSOR FOR
						SELECT RequestId
						FROM BatchesDataTobeDeleted
						end

						OPEN batch_cursor
						FETCH NEXT FROM batch_cursor
						INTO @requestId

						WHILE @@FETCH_STATUS = 0
						BEGIN

						IF not EXISTS(SELECT * from Batches where RequestId = @requestId)
						 BEGIN
							INSERT INTO #tempBatch VALUES ('NotApplied',0,@requestId,0,null,@AppliedTime) 
						 END
					    ELSE
						 BEGIN

							set @BatchId = (select Id from Batches where RequestId = @requestId)
							set @ClientId = (select CustomerId from Batches where RequestId = @requestId)
							set @ClientName = (select FirstName from Clients where Id = @ClientId)

							Update BatchMeta set FieldValue = '****' where BatchId = @BatchId

							update BatchItemFields set RegisteredFieldValue = '****', RegisteredFieldValue_old = '****' where batchItemId in  (select Id from BatchItems where BatchId = @BatchId)
			
							update Batches set AppliedGDPR = 1 where Id = @BatchId
			
				            IF not EXISTS (Select * from ServiceLastExcecution where ServiceName = 'GDPRService')
							Begin
								insert into ServiceLastExcecution(ServiceName , Time) values ('GDPRService' ,GETDATE())
							End
							Else
							Begin 
								update ServiceLastExcecution set Time = GETDATE() where ServiceName = 'GDPRService'
							end

			             	INSERT INTO #tempBatch VALUES ('Completed',@BatchId,@requestId,@ClientId,@ClientName,@AppliedTime) 

							FETCH NEXT FROM batch_cursor 
							INTO @requestId
					
						END
						END

						CLOSE batch_cursor;
						DEALLOCATE batch_cursor;
							
					Select * from #tempBatch

					END TRY
					BEGIN CATCH	
					return 1
					END CATCH

					return 0
					END
            
GO
