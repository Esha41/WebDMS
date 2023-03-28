using Microsoft.EntityFrameworkCore.Migrations;

namespace Intelli.DMS.Domain.Migrations
{
    public partial class DummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
USE [DMS]
GO
SET IDENTITY_INSERT [dbo].[BatchItemStatuses] ON 
GO
INSERT [dbo].[BatchItemStatuses] ([ID], [BatchItemStatus], [EnumValue]) VALUES (1, N'Default', N'Default')
GO
SET IDENTITY_INSERT [dbo].[BatchItemStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentTypes] ON 
GO
INSERT [dbo].[DocumentTypes] ([Id], [DocumentTypeName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (16, N'Εγκριση για margin απο Backoffice ', 1, 1647933036, 1647949154)
GO
INSERT [dbo].[DocumentTypes] ([Id], [DocumentTypeName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (17, N'Beneficial Owners', 1, 1647933053, 1647949257)
GO
INSERT [dbo].[DocumentTypes] ([Id], [DocumentTypeName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (18, N'CIP', 1, 1647933179, 1647949273)
GO
INSERT [dbo].[DocumentTypes] ([Id], [DocumentTypeName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (19, N'Fax - Mail', 1, 1647933189, 1647949286)
GO
SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentClasses] ON 
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (18, N'Document Class 1', N'Class value 1', N'Mapped value 1', 1, 16, 1647933579, 1, 1647933663)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (19, N'Document Class 2', N'Value 2', N'Mapped 2', 2, 17, 1647934435, 1, 1647934435)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (20, N'All Field Types', N'Value 3', N'Mapped 3', 3, 18, 1647934454, 1, 1647934734)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (23, N'Document Class 4', N'12', N'Document1', 1234, 18, 1647941306, 0, 1647941442)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (24, N'Document Class 5', N'12', N'Document1', 1234, 16, 1647944089, 0, 1647944194)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (25, N'Document Class 3u', N'Value 3', N'Mapped 3', 3, 19, 1647946793, 1, 1647958730)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (26, N'CNIC', N'CNIC', N'CNIC', 1, 18, 1647949595, 1, 1648194776)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (27, N'a6', N'r', N'r234', 1234, 17, 1648134867, 0, 1648134891)
GO
INSERT [dbo].[DocumentClasses] ([Id], [DocumentClassName], [EnumValue], [RecognitionMappedName], [GroupMandatoryDocument], [DocumentTypeId], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (33, N'Document Class 420', N'Enum Value 420', N'R Mapped Name 420', 420, 17, 1648191547, 0, 1648194728)
GO
SET IDENTITY_INSERT [dbo].[DocumentClasses] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (16, N'Client1', N'one', N'123456789', N'123456789', 1, 1647945661, 1647950248, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (17, N'Client2', N'Two', N'55454545', N'45454545', 1, 1647945698, 1647950200, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (18, N'Client3', N'Three', N'554774585', N'2254115', 1, 1647945765, 1647949001, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (19, N'Client7', N'Four', N'asc1', N'csa1', 1, 1648112230, 1648112230, 1, N'Dummy Reason')
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (20, N'Testscas', N'scsc', N'sacascas', N'sacsa', 0, 1648112633, 1648112633, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (21, N'Test8', N'Client8', N'88', N'88', 1, 1648112910, 1648112910, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (22, N'Test9', N'Client9', N'999', N'999', 0, 1648115915, 1648115915, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (23, N'Test111', N'1111', N'111', N'111', 0, 1648116316, 1648116316, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (24, N'Test1212', N'Client1212', N'1212', N'1212', 0, 1648116343, 1648116343, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (25, N'Test11111', N'11111111', N'111111', N'1111111111', 0, 1648117522, 1648117522, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (27, N'Test123', N'Client123', N'123', N'123', 1, 1648120034, 1648120034, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (28, N'Test1234', N'Client1234', N'1234', N'1234', 1, 1648120062, 1648120062, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (32, N'Test12', N'Client12', N'12', N'12', 1, 1648125998, 1648125998, 0, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (34, N'Client 01', N'Client 01', N'000', N'000', 1, 1648126144, 1648126144, 1, NULL)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [AFM], [CDI], [IsActive], [CreatedAt], [UpdatedAt], [IsValidForTransaction], [Reason]) VALUES (35, N'Client0', N'Test0', N'000000', N'000000', 0, 1648126169, 1648126169, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchSources] ON 
GO
INSERT [dbo].[BatchSources] ([Id], [BatchSource], [EnumValue], [BatchSourceCode], [Comments]) VALUES (1, N'Default', N'Default', N'Default', N'Default')
GO
SET IDENTITY_INSERT [dbo].[BatchSources] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchStatuses] ON 
GO
INSERT [dbo].[BatchStatuses] ([Id], [BatchStatus], [EnumValue], [Description]) VALUES (1, N'Default', N'Default', N'Default')
GO
SET IDENTITY_INSERT [dbo].[BatchStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (1, N'Company 1', N'temp.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, 1, 0, NULL, 0, NULL, 1, NULL, NULL, 1, N'9folder@mailinator.com', 1, 1, NULL, 1, 1, NULL, N'test.com', NULL, NULL, NULL, 1, 0, 1636458382, 5)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (2, N'Company 2', N'callback.agentportal.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, 0, NULL, 0, NULL, NULL, 1, N'9folder@mailinator.com', 1, 1, NULL, 1, 1, NULL, N'test.com', NULL, NULL, NULL, 1, 0, 1634724646, 1)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (3, N'Company 3', N'test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'company@gmail.com', 1, 1, NULL, 1, 1, NULL, N'sadasd', NULL, NULL, NULL, 0, 1632763592, 1632941278, 0)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (4, N'test company1', N'test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, 0, NULL, 0, NULL, NULL, 101, N'company@gmail.com', 1, 1, NULL, 1, 1, NULL, N'that.com', NULL, NULL, NULL, 1, 1632852732, 1634724690, 3)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (5, N'loader company', N'test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, N'asdasd@fmai.com', 0, 0, NULL, 0, 0, NULL, N'that.com', NULL, NULL, NULL, 0, 1632983552, 1632983552, 0)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (6, N'test company', N'test.com', N'12', N'dasdasd', N'dasdasd', N'asdasd', N'asdasdas', N'asdasd', N'dasdasd', 11, 5, N'asdasd', 1212, 1, 0, NULL, N'asdasd', N'asdasdas', 1, 1, 1, N'dsadas', 1, 12, 1, 21, 21, 123, N'admina1@hazelsuite.com', 1, 1, N'asdasd', 0, 0, 12, N'hello.com', N'dsadasd', 12, N'sdadasd', 1, 1633436685, 1634724524, 2)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (7, N'testCompany1', N'test.com', N'12', N'dasdasd', N'dsadsadasd', N'12asdda', N'adsasdasd', N'dasdasdas', N'asdasdasd', 1212, 121, N'sdada', 121, 1, 1, NULL, N'asdasd', N'asdasdasd', 1, 1, 1, N'asdasdasd', 1, 1221, 1, 1211, 121, 102, N'test123@gmail.com', 1, 1, N'asdasd', 1, 0, 1212, N'test.com', N'saddasd', 1212, N'asdasd', 1, 1633446150, 1634724718, 2)
GO
INSERT [dbo].[Companies] ([Id], [CompanyName], [CallBackURL], [HawkAppID], [HawkUser], [HawkSecret], [FtpHostName], [FtpUserName], [FtpPassword], [FtpDirectory], [RetriesWhenFailPublished], [GDPRDaysToBeKept], [Code], [FtpPort], [FtpUserSecureProtocol], [FtpActive], [FtpResponseHostName], [FtpResponseUserName], [FtpResponsePassword], [FtpResponsePort], [FtpResponseUserSecureProtocol], [FtpResponseActive], [FtpResponseDirectory], [ResponseWithFtp], [SimilarityThreshold], [Enabled], [SLAMinutes], [SLABatchQuantity], [SLAImportance], [Email], [IsSignedCompany], [SendRejectionReasonAsCode], [Logo], [SendLink], [SupportsCalls], [MaxCallTIme], [VideoCallBackUrl], [AgentController], [CustomerRetries], [SMSProvider], [IsActive], [CreatedAt], [UpdatedAt], [UsersPerCompany]) VALUES (8, N'newCompanyStagging', N'test.com', N'121231231', N'dasdasd', N'123123123123', N'asdasd', N'2eqweasdasd', N'asdasddasadsasd', N'asdasdasdasdasd', 211212, 0, N'asdqw12132', NULL, 1, 1, NULL, N'asdasdasdasd', N'asdasdadsasd', 12123123, 1, 1, N'asdasdasdasd', 1, 2112312312, 1, NULL, 123123123, 12, N'stagingtestComapny@mailinator.com', 1, 1, N'asdasddasads', 1, 1, NULL, N'asdasdasd', N'asdadsasd', NULL, N'sdaasddasasdasd', 1, 1643953174, 1643953174, 3)
GO
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Batches] ON 
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (3, N'e59ba380-a950-456d-bc0f-5b3c2991b8dd', NULL, CAST(N'2022-03-19T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647677926, 1647677926, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (4, N'3f516d81-ae6b-4de4-a4e7-bb73628a9f9c', NULL, CAST(N'2022-03-19T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647679148, 1647679148, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (5, N'7ebc25b6-5fb4-4f39-bb6e-bad32f225719', NULL, CAST(N'2022-03-19T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647679253, 1647679253, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (6, N'cc5c83b3-eda3-41bb-bd43-dbb16bbe83d2', NULL, CAST(N'2022-03-19T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647679817, 1647679817, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (7, N'2f34398c-fcab-40fc-bcb9-2ed0a672f97b', NULL, CAST(N'2022-03-19T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647682589, 1647682589, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (10, N'cc980257-0374-4aa1-8e69-29c77b121994', NULL, CAST(N'2022-03-21T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647896635, 1647896635, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (14, N'c4c5c13d-d939-4c23-8ab1-5dccfed49662', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647931735, 1647931735, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (15, N'05b59b06-802a-4a80-bc1b-70a7c3b3d59d', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647932378, 1647932378, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (16, N'57e004e3-a517-4478-80ed-25c088fed582', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647938869, 1647938869, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (17, N'4129f146-eb5a-4867-a722-f0a61c8d1754', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647938896, 1647938896, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (18, N'0237a49b-3d46-47a4-9f94-d140aa3c675e', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647942635, 1647942635, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (19, N'0973dc83-bb8a-46cc-a811-acb7227f1669', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647943478, 1647943478, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (20, N'bafec660-45c2-47e3-917f-ea259a8a6b45', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647943717, 1647943717, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (21, N'70010ffa-00e7-4b49-accb-08d6b440ea52', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647945408, 1647945408, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (22, N'3744c2b6-2be0-4e67-896e-ebde7251aa34', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647947362, 1647947362, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (23, N'142fdfad-04c6-448b-892d-808ad3434a8b', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647949865, 1647949865, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (24, N'ea4d993a-c992-4e72-9cef-fe84aa66fe16', NULL, CAST(N'2022-03-22T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1647954967, 1647954967, NULL)
GO
INSERT [dbo].[Batches] ([Id], [RequestId], [BusinessUnitId], [CreatedDate], [BatchStatusId], [LockedBy], [VerifiedStartDate], [VerifiedEndDate], [BatchSourceId], [PublishedDate], [RetriesCount], [MandatoryAlerts], [ValidationAlerts], [CompanyId], [CurrentOTP], [OTPValidUntil], [AppliedGDPR], [RecognizedDate], [StartProcessDate], [InternalRequestId], [LockedByNavigationId], [IsActive], [CreatedAt], [UpdatedAt], [CustomerId]) VALUES (25, N'c88f87cf-d304-4b85-beee-cb7f347fb439', NULL, CAST(N'2022-03-25T00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1, 1648216828, 1648216828, NULL)
GO
SET IDENTITY_INSERT [dbo].[Batches] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchItems] ON 
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, 16, 1, 20, CAST(N'2022-03-22T08:47:49.433' AS DateTime), NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, 17, 1, 20, CAST(N'2022-03-22T08:48:16.667' AS DateTime), NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, 18, 1, 19, CAST(N'2022-03-22T09:50:35.187' AS DateTime), NULL, NULL, 1, 1647942635, 1647942635)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, 19, 1, 19, CAST(N'2022-03-22T10:04:38.047' AS DateTime), NULL, NULL, 1, 1647943478, 1647943478)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, 20, 1, 18, CAST(N'2022-03-22T10:08:37.227' AS DateTime), NULL, NULL, 1, 1647943717, 1647943717)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (14, 21, 1, 20, CAST(N'2022-03-22T10:36:48.827' AS DateTime), NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, 22, 1, 20, CAST(N'2022-03-22T11:09:22.707' AS DateTime), NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (16, 23, 1, 26, CAST(N'2022-03-22T11:51:05.560' AS DateTime), NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (17, 24, 1, 26, CAST(N'2022-03-22T13:16:07.097' AS DateTime), NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItems] ([Id], [BatchId], [BatchItemStatusId], [DocumentClassId], [OccuredAt], [ParentId], [AspNetUserId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (18, 25, 1, 26, CAST(N'2022-03-25T14:00:29.083' AS DateTime), NULL, NULL, 1, 1648216829, 1648216829)
GO
SET IDENTITY_INSERT [dbo].[BatchItems] OFF
GO
SET IDENTITY_INSERT [dbo].[DictionaryTypes] ON 
GO
INSERT [dbo].[DictionaryTypes] ([Id], [DictionaryType], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'Gender', 0, 1, 0)
GO
INSERT [dbo].[DictionaryTypes] ([Id], [DictionaryType], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'Marital Status', 0, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[DictionaryTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[BopDictionaries] ON 
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, 1, N'Male', NULL, N'Male', NULL, 0, 0)
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, 1, N'Female', NULL, N'Female', NULL, 0, 0)
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, 1, N'Other', NULL, N'Other', NULL, 0, 0)
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, 2, N'Single', NULL, N'Single', NULL, 0, 0)
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, 2, N'Married', NULL, N'Married', NULL, 0, 0)
GO
INSERT [dbo].[BopDictionaries] ([Id], [DictionaryTypeId], [Value], [Description], [Code], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, 2, N'Widowed', NULL, N'Widowed', NULL, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[BopDictionaries] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentClassFieldTypes] ON 
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (1, N'SingleLineText', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (2, N'MultiLineText', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (3, N'Integer', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (4, N'Decimal', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (5, N'Boolean', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (6, N'Date', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (7, N'Time', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (8, N'DateTime', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (9, N'Dictionary', 0, 1, 0)
GO
INSERT [dbo].[DocumentClassFieldTypes] ([Id], [Type], [CreatedAt], [IsActive], [UpdatedAt]) VALUES (10, N'Expiration Date', 0, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[DocumentClassFieldTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentClassFields] ON 
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (64, 18, 1, N'Value 1', N'Surname', 0, 1, NULL, 1, N'Mapped 1', N'Corrective 1', 1647934123, 1, 1647934294, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (65, 18, 1, N'Value 1', N'Surname', 0, 1, NULL, 1, N'Mapped 1', N'Corrective 1', 1647934346, 0, 1647941174, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (66, 19, 1, N'Value 2', N'FatherName', 0, 2, NULL, 1, N'Mapped 2', N'Corrective 2', 1647934669, 1, 1647934686, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (67, 20, 1, N'Value 1', N'Surname', 1, 1, NULL, 1, N'Mapped 1', N'Corrective 1', 1647934760, 1, 1647934760, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (68, 20, 6, N'Value 2', N'BirthDate', 1, 2, NULL, 0, N'Mapped 2', N'Corrective 2', 1647935508, 1, 1647936218, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (69, 20, 2, N'Value 3', N'BirthPlace', 1, 3, NULL, 1, N'Mapped 3', N'Corrective 3', 1647935574, 1, 1647936297, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (70, 20, 10, N'Value 4', N'Issue Date', 0, 4, NULL, 1, N'Mapped 4', N'Corrective 4', 1647936162, 1, 1647936162, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (71, 20, 9, N'Value 5', N'Gender', 0, 5, 1, 1, N'Mapped 5', N'Corrective 5', 1647936202, 1, 1647938774, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (73, 25, 1, N'3', N'SurName', 0, 3, NULL, 1, N'Mapped 3', N'Corrective 3', 1647947037, 1, 1647947037, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (74, 26, 1, N'Name', N'Name', 0, 1, NULL, 1, N'Name', N'Name', 1647949652, 1, 1648194776, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (75, 26, 1, N'Father Name', N'Father Name', 0, 1, NULL, 1, N'Father Name', N'Father Name', 1647949681, 1, 1647949681, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (76, 26, 6, N'DOB', N'DOB', 0, 2, NULL, 1, N'DOB', N'DOB', 1647949699, 1, 1647949699, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (77, 26, 9, N'Gender', N'Gender', 0, 3, 1, 0, N'Gender', N'Gender', 1647949744, 1, 1647949744, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (78, 26, 2, N'Address', N'Address', 0, 4, NULL, 1, N'Address', N'Address', 1647954740, 1, 1647954740, NULL, NULL)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (79, 33, 8, N'r435', N'd', 1, 45, NULL, 1, N'b', N'a', 1648191549, 1, 1648191795, 45, 3)
GO
INSERT [dbo].[DocumentClassFields] ([Id], [DocumentClassId], [DocumentClassFieldTypeId], [EnumValue], [UILabel], [PublishEnabled], [UISort], [DictionaryTypeId], [IsMandatory], [MappedName], [CorrectiveActionMappedName], [CreatedAt], [IsActive], [UpdatedAt], [MinLength], [MaxLength]) VALUES (80, 33, 5, N'r', N'd', 1, 45, NULL, 1, N'b', N'a', 1648191748, 1, 1648191795, 7, 5)
GO
SET IDENTITY_INSERT [dbo].[DocumentClassFields] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchItemFields] ON 
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (25, 9, 67, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (26, 9, 68, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (27, 9, 69, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (28, 9, 70, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (29, 9, 71, NULL, 1, NULL, NULL, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (30, 10, 67, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (31, 10, 68, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (32, 10, 69, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (33, 10, 70, NULL, 1, N'', NULL, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (34, 10, 71, NULL, 1, NULL, NULL, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (35, 11, 66, NULL, 1, N'ddd', NULL, NULL, NULL, 1, 1647942635, 1647942635)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (36, 12, 66, NULL, 1, N'ddd', NULL, NULL, NULL, 1, 1647943478, 1647943478)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (37, 13, 64, NULL, 1, N'srdfsd', NULL, NULL, NULL, 1, 1647943717, 1647943717)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (38, 14, 67, NULL, 1, N'dfa', NULL, NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (39, 14, 68, NULL, 1, N'2022-03-23', NULL, NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (40, 14, 69, NULL, 1, N'af', NULL, NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (41, 14, 70, NULL, 1, N'2022-03-23', NULL, NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (42, 14, 71, NULL, 1, N'2', NULL, NULL, NULL, 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (43, 15, 67, NULL, 1, N'fd', NULL, NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (44, 15, 68, NULL, 1, N'', NULL, NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (45, 15, 69, NULL, 1, N'ff', NULL, NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (46, 15, 70, NULL, 1, N'2022-03-17', NULL, NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (47, 15, 71, NULL, 1, N'2', NULL, NULL, NULL, 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (48, 16, 74, NULL, 1, N'Abdul Basit', NULL, NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (49, 16, 75, NULL, 1, N'Abdul Ghaffar', NULL, NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (50, 16, 76, NULL, 1, N'2000-01-01', NULL, NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (51, 16, 77, NULL, 1, NULL, NULL, NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (52, 17, 74, NULL, 1, N'Abdul Basit', NULL, NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (53, 17, 75, NULL, 1, N'Abdul Ghaffar', NULL, NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (54, 17, 76, NULL, 1, N'2020-06-09', NULL, NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (55, 17, 77, NULL, 1, N'1', NULL, NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (56, 17, 78, NULL, 1, N'Lahore, Pakistan', NULL, NULL, NULL, 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (57, 18, 74, NULL, 1, N'Volton', NULL, NULL, NULL, 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (58, 18, 75, NULL, 1, N'Awais', NULL, NULL, NULL, 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (59, 18, 76, NULL, 1, N'2022-03-16', NULL, NULL, NULL, 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (60, 18, 77, NULL, 1, N'1', NULL, NULL, NULL, 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchItemFields] ([Id], [BatchItemId], [DocumentClassFieldId], [DictionaryValueId], [IsLast], [RegisteredFieldValue], [DictionaryValueId_old], [RegisteredFieldValue_old], [IsUpdated], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (61, 18, 78, NULL, 1, N'sdcsdcsdcsdcsdc', NULL, NULL, NULL, 1, 1648216829, 1648216829)
GO
SET IDENTITY_INSERT [dbo].[BatchItemFields] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchItemPages] ON 
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, 9, N'Tue Mar 22 2022-DocumentClass', 1, N'Tue Mar 22 2022-DocumentClass.pdf', 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, 10, N'Tue Mar 22 2022-DocumentClass (1)', 1, N'Tue Mar 22 2022-DocumentClass (1).pdf', 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, 11, N'48', 1, N'48.jpg', 1, 1647942635, 1647942635)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, 12, N'sample2', 1, N'sample2.jpeg', 1, 1647943478, 1647943478)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, 13, N'48', 1, N'48.jpg', 1, 1647943717, 1647943717)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, 14, N'sample2', 1, N'sample2.jpeg', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, 15, N'sample5', 1, N'sample5.jpeg', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, 16, N'CNIC', 1, N'CNIC.jpg', 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, 17, N'CNIC', 1, N'CNIC.jpg', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchItemPages] ([Id], [BatchItemId], [FileName], [Number], [OriginalName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, 18, N'Screenshot (1)', 1, N'Screenshot (1).png', 1, 1648216829, 1648216829)
GO
SET IDENTITY_INSERT [dbo].[BatchItemPages] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentCategories] ON 
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Catagory 1', 1, 1646991362, 1646991362)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Catagory 2', 1, 1647261496, 1647261496)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Category 3', 0, 1647345816, 1647345880)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Category 33333', 1, 1647352085, 1647352085)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'TestCategoryu', 1, 1647520422, 1647520451)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'TestABCu', 1, 1647522491, 1647522521)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'TestCategoryi', 1, 1647522714, 1647522714)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'TestCategoryb', 1, 1647522936, 1647522936)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'TestABC1', 1, 1647523055, 1647523055)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'TestABC2', 1, 1647524979, 1647524979)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, N'TestCategory1', 1, 1647525054, 1647525054)
GO
INSERT [dbo].[DocumentCategories] ([Id], [DocumentCategoryName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, N'TestCategory12', 1, 1647526246, 1647526246)
GO
SET IDENTITY_INSERT [dbo].[DocumentCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentSubCategories] ON 
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Sub Catagory', 1, 1, 1646991400, 1646991400)
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Sub Catagory 2', 2, 1, 1647261599, 1647261599)
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Sub Category 3u', 2, 0, 1647346208, 1647346227)
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Sub Category 2', 2, 1, 1647349206, 1647349206)
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Sub Category 3', 2, 1, 1647349829, 1647349829)
GO
INSERT [dbo].[DocumentSubCategories] ([Id], [DocumentSubCategoryName], [DocumentCategoryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Sub Category 45u', 2, 0, 1647436055, 1647436089)
GO
SET IDENTITY_INSERT [dbo].[DocumentSubCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Screens] ON 
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Audit', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Reporting', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'Roles', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'Users', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'Company', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (6, N'Preferences', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'UserCountries', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'UserRoles', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'DocumentsPerCompany', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'Configurations', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, N'DocumentType', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, N'DocumentCategory', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, N'DocumentSubCategory', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (14, N'DocumentClass', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, N'DocumentUpload', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (16, N'DocumentCategorization', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (17, N'ClientRepository', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (18, N'DocumentReview', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (19, N'GDPRCompliance', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (20, N'ExportLogs', 1, 0, 0)
GO
INSERT [dbo].[Screens] ([Id], [ScreenName], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (21, N'Client', 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Screens] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemRoles] ON 
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1, N'Super Admin', 1, 1604403101, 1647857717, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (2, N'Supervisor', 1, 1604649235, 1646649011, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (3, N'Reporting', 1, 1631278988, 1638787649, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (4, N'Agent4', 1, 1631279229, 1632231795, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (5, N'To Delete', 1, 1631279319, 1631279319, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (6, N'testRole6', 1, 1632123357, 1632123357, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (7, N'testRole7', 1, 1632124160, 1632124160, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (8, N'test change', 1, 1632127089, 1632143081, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (9, N'front role test9', 1, 1632127108, 1632245618, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (10, N'front role test10', 1, 1632127791, 1636720872, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (11, N'role test', 1, 1632128222, 1632234069, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (12, N'Agent12', 1, 1632130768, 1632906291, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (13, N'front role test13', 1, 1632133974, 1632233841, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (14, N'hello123', 1, 1632143294, 1632205154, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (15, N'Auth Test Role', 1, 1632143314, 1633506658, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (16, N'test action edit', 1, 1632205347, 1632205359, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (17, N'add', 1, 1632205369, 1632305075, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (18, N'test form', 1, 1632263212, 1632662958, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (19, N'Default Role', 1, 1632301396, 1632808323, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (20, N'To Delete 1', 1, 1632383401, 1632485431, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (21, N'test21', 1, 1632482145, 1636720831, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (22, N'test role22', 1, 1632485446, 1632485471, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (23, N'test23', 1, 1632485684, 1632983800, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (24, N'TestRole24', 1, 1632915424, 1632915424, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (25, N'new role', 1, 1632923687, 1632940729, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (26, N'test role26', 1, 1632991092, 1632991092, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (27, N'Agent27', 1, 1633431194, 1633431194, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (28, N'test role28', 1, 1633431199, 1633431199, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (32, N'Last role', 1, 1633457370, 1633457370, 1, 0)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (37, N'Tester', 1, 1635802013, 1643952637, 1, 2)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1037, N'Test Role Company 1', 1, 1643897592, 1644405256, 1, 2)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1038, N'stagingRoleAdmin', 1, 1643953072, 1643954744, 8, 1)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1039, N'stagingReportRole', 1, 1643953398, 1643953398, 8, 2)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1041, N'Test Role 2', 1, 1644388200, 1646647780, 1, 1)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1042, N'Test Role Company 2', 1, 1644405126, 1644405126, 2, 1)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1043, N'Test Role Company 11', 1, 1644405325, 1644405325, 2, 3)
GO
INSERT [dbo].[SystemRoles] ([Id], [Name], [IsActive], [CreatedAt], [UpdatedAt], [CompanyId], [Priority]) VALUES (1045, N'Test Role Company 1', 0, 1646647751, 1648064814, 7, 1)
GO
SET IDENTITY_INSERT [dbo].[SystemRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[ScreenElements] ON 
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (19, N'Filter', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (20, N'Export', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (21, N'ExportGridData', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (22, N'ExportAllData', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (23, N'Sort', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (24, N'Pagination', 1, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (25, N'Filter', 2, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (26, N'AddNew', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (27, N'Edit', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (28, N'Delete', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (29, N'Filter', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (30, N'Export', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (31, N'ExportGridData', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (32, N'ExportAllData', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (33, N'Sort', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (34, N'Pagination', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (35, N'RoleNameInput', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (36, N'ScreenPrivilegesSelect', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (37, N'RoleNameSubmit', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (38, N'RolePrivilegesSubmit', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (39, N'ElementSubmitButton', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (40, N'RolesElementSelect', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (41, N'AddNew', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (42, N'Edit', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (43, N'Delete', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (44, N'Filter', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (45, N'Export', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (46, N'ExportGridData', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (47, N'ExportAllData', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (48, N'Sort', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (49, N'Pagination', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (50, N'UserNameInput', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (51, N'UserCompanySelect', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (52, N'UserEmail', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (53, N'UserSubmitButton', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (54, N'UserRolesSelect', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (55, N'UserCountriesSelect', 4, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (56, N'AddNew', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (57, N'Edit', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (58, N'Delete', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (59, N'Filter', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (60, N'Export', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (61, N'ExportGridData', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (62, N'ExportAllData', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (63, N'Sort', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (64, N'Pagination', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (65, N'GridPageSize', 6, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (66, N'CompanyNameInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (67, N'CallBackUrlInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (68, N'Slaimportance', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (69, N'CompanyEmailInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (70, N'IsSignedCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (71, N'SendRejectetionCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (72, N'SendLinkCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (73, N'SupportCallCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (74, N'VideoCallBackInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (75, N'IsActiveCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (76, N'CompanySubmit', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (78, N'HawkAppIdInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (79, N'HawkUserInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (80, N'HawkSecretInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (81, N'FtpHostNameInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (82, N'FtpUserNameInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (83, N'FtpPasswordInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (84, N'FtpDirectoryInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (85, N'RetriesWhenFailPublishedInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (86, N'GdprdaysToBeKeptInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (87, N'CodeInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (88, N'FtpPortInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (89, N'FTpUserSecureProtocolCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (90, N'FtpActiveCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (91, N'FtpResponseHostNameInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (92, N'FtpResponseUserNameInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (93, N'FtpResponsePasswordInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (94, N'FtpResponsePortInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (95, N'FtpResponseUserSecureProtocolInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (96, N'FtpResponseActiveCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (97, N'FtpResponseDirectoryInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (98, N'FtpResponseCheckbox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (99, N'SimilarityThresholdInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (100, N'EnableCheckBox', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (101, N'SlaminutesInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (102, N'SlabBatchQuantityInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (103, N'LogoInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (104, N'MaxCallInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (105, N'AgentControllerInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (106, N'CustomerRetriesInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (107, N'SmsProviderInput', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (108, N'PasswordRequireNonAlphanumericCheckBox', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (109, N'PasswordRequireLowercaseCheckBox', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (110, N'PasswordRequireUppercaseCheckBox', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (111, N'PasswordRequireDigitCheckBox', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (112, N'PasswordRequiredLengthCheckInput', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (113, N'RestrictLastUsedPasswordsInput', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (114, N'ForcePasswordChangeDaysInput', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (115, N'PasswordPolicySubmit', 10, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (116, N'PreferencesSubmit', 6, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (117, N'UserPerCompany', 5, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1117, N'CompanySelect', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1118, N'PrioritySelect', 3, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1119, N'AddNew', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1120, N'Edit', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1121, N'Delete', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1122, N'Filter', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1123, N'Export', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1124, N'ExportGridData', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1125, N'ExportAllData', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1126, N'Sort', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1127, N'Pagination', 11, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1128, N'AddNew', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1129, N'Edit', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1130, N'Delete', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1131, N'Filter', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1132, N'Export', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1133, N'ExportGridData', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1134, N'ExportAllData', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1135, N'Sort', 14, 1, 0, 0)
GO
INSERT [dbo].[ScreenElements] ([Id], [ScreenElementName], [ScreenId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1136, N'Pagination', 14, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[ScreenElements] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleScreenElements] ON 
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (208, 15, 33, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (209, 15, 26, 2, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (210, 15, 27, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (211, 15, 28, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (212, 15, 29, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (213, 15, 30, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (214, 15, 31, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (215, 15, 32, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (216, 15, 40, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (217, 15, 34, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (218, 15, 35, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (219, 15, 36, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (220, 15, 37, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (221, 15, 38, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (222, 15, 39, 0, 1, 1632732167, 1632732167)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (365, 15, 53, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (366, 15, 52, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (367, 15, 51, 0, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (368, 15, 50, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (369, 15, 49, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (370, 15, 48, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (371, 15, 47, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (372, 15, 46, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (373, 15, 45, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (374, 15, 44, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (375, 15, 43, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (376, 15, 42, 2, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (377, 15, 41, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (378, 15, 54, 2, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (379, 15, 55, 1, 1, 1632747241, 1632747241)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (398, 15, 65, 0, 1, 1632809412, 1632809412)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (431, 23, 66, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (432, 23, 73, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (433, 23, 72, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (434, 23, 71, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (435, 23, 70, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (436, 23, 69, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (437, 23, 68, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (438, 23, 67, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (439, 23, 76, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (440, 23, 64, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (441, 23, 63, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (442, 23, 62, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (443, 23, 61, 1, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (444, 23, 60, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (445, 23, 59, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (446, 23, 58, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (447, 23, 57, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (448, 23, 56, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (449, 23, 74, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (450, 23, 75, 0, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (451, 15, 19, 0, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (452, 15, 20, 0, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (453, 15, 21, 0, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (454, 15, 22, 0, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (455, 15, 23, 0, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (456, 15, 24, 1, 1, 1632984630, 1632984630)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (457, 1, 19, 0, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (458, 1, 20, 2, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (459, 1, 21, 2, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (460, 1, 22, 2, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (461, 1, 23, 2, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (462, 1, 24, 2, 1, 1632987028, 1632987028)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1275, 15, 105, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1276, 15, 79, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1277, 15, 78, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1278, 15, 76, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1279, 15, 75, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1280, 15, 74, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1281, 15, 73, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1282, 15, 72, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1283, 15, 71, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1284, 15, 70, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1285, 15, 69, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1286, 15, 80, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1287, 15, 68, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1288, 15, 66, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1289, 15, 64, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1290, 15, 63, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1291, 15, 62, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1292, 15, 61, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1293, 15, 60, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1294, 15, 59, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1295, 15, 58, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1296, 15, 57, 2, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1297, 15, 56, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1298, 15, 67, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1299, 15, 106, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1300, 15, 81, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1301, 15, 83, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1302, 15, 104, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1303, 15, 103, 2, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1304, 15, 102, 0, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1305, 15, 101, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1306, 15, 100, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1307, 15, 99, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1308, 15, 98, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1309, 15, 97, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1310, 15, 96, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1311, 15, 82, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1312, 15, 95, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1313, 15, 93, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1314, 15, 92, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1315, 15, 91, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1316, 15, 90, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1317, 15, 89, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1318, 15, 88, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1319, 15, 87, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1320, 15, 86, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1321, 15, 85, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1322, 15, 84, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1323, 15, 94, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1324, 15, 107, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1325, 3, 105, 2, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1326, 3, 79, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1327, 3, 78, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1328, 3, 76, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1329, 3, 75, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1330, 3, 74, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1331, 3, 73, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1332, 3, 72, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1333, 3, 71, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1334, 3, 70, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1335, 3, 69, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1336, 3, 80, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1337, 3, 68, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1338, 3, 66, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1339, 3, 64, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1340, 3, 63, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1341, 3, 62, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1342, 3, 61, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1343, 3, 60, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1344, 3, 59, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1345, 3, 58, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1346, 3, 57, 2, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1347, 3, 56, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1348, 3, 67, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1349, 3, 106, 2, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1350, 3, 81, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1351, 3, 83, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1352, 3, 104, 2, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1353, 3, 103, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1354, 3, 102, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1355, 3, 101, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1356, 3, 100, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1357, 3, 99, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1358, 3, 98, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1359, 3, 97, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1360, 3, 96, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1361, 3, 82, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1362, 3, 95, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1363, 3, 93, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1364, 3, 92, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1365, 3, 91, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1366, 3, 90, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1367, 3, 89, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1368, 3, 88, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1369, 3, 87, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1370, 3, 86, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1371, 3, 85, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1372, 3, 84, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1373, 3, 94, 0, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1374, 3, 107, 2, 1, 1633506989, 1633506989)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1382, 1, 115, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1383, 1, 113, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1384, 1, 112, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1385, 1, 111, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1386, 1, 110, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1387, 1, 109, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1388, 1, 108, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1389, 1, 114, 1, 1, 1633593618, 1633593618)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1390, 1, 65, 1, 1, 1633593797, 1633593797)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1391, 1, 116, 1, 1, 1633593797, 1633593797)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1392, 37, 65, 2, 1, 1635802054, 1635802054)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1393, 37, 116, 2, 1, 1635802054, 1635802054)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1394, 1, 1119, 2, 1, 123, 123)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1395, 1, 1120, 2, 1, 123, 123)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1396, 1, 1121, 2, 1, 123, 123)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1397, 1045, 19, 0, 1, 1647430515, 1647430515)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1398, 1045, 20, 0, 1, 1647430515, 1647430515)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1399, 1045, 21, 0, 1, 1647430515, 1647430515)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1400, 1045, 22, 0, 1, 1647430515, 1647430515)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1401, 1045, 23, 0, 1, 1647430515, 1647430515)
GO
INSERT [dbo].[RoleScreenElements] ([Id], [RoleId], [ScreenElementId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1402, 1045, 24, 0, 1, 1647430515, 1647430515)
GO
SET IDENTITY_INSERT [dbo].[RoleScreenElements] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] ON 
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Portal Admin', N'portaladmin@mailinator.com', N'', 1, 0, 1632830250)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, N'test user 2', N'test@gmail.com', NULL, 0, 1632337130, 1632376229)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, N'test user 33', N'admin@hazelsuite.com', NULL, 0, 1632339040, 1632339060)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, N'toast test', N'hellotoast@gmail.com', NULL, 0, 1632339312, 1635839421)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, N'test user 1', N'alpha@mailinator.com', NULL, 1, 1632376342, 1632376342)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, N'To Delete', N'todelete@mailinator.com', NULL, 0, 1632376385, 1632376421)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, N'test user 1', N'asdasda@gmail.com', NULL, 0, 1632486957, 1632486957)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, N'User 1', N'user1@mailinator.com', NULL, 0, 0, 1632853603)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (11, N'User 2', N'user2@mailinator.com', NULL, 1, 0, 1636720804)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, N'morfis', N'msallis@intelli-corp.com', NULL, 1, 1632910494, 1638787580)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, N'test name', N'test12@gmail.com', NULL, 0, 1632916981, 1632916981)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (14, N'add user test', N'test43@gmail.com', NULL, 0, 1632917816, 1632917830)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, N'test user', N'admin@test.com', NULL, 0, 1632917865, 1632917865)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (18, N'testloader', N'testLoader1@gmail.com', NULL, 0, 1632981907, 1632981907)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (19, N'jkahdjkahs', N'xifeyih772@carpetd.com', NULL, 0, 1632983190, 1633002217)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (20, N'Aftab Ahmad', N'aftab.ahmad@atomicity-interactive.com', NULL, 1, 1633326984, 1643952780)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (26, N'validationTester', N'viraj42730@oemmeo.com', NULL, 1, 1633372847, 1633372847)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (27, N'validationTester', N'testUserM@mailinator.com', NULL, 1, 1633376565, 1643983669)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (30, N'Aftab Ahmad', N'aftab.era@mailinator.com', NULL, 1, 1633500551, 1644404978)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (31, N'New User 10', N'newuser1@mailinator.com', NULL, 1, 1633516863, 1636720776)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (32, N'test_user_agent', N'test_user_agent@mailinator.com', NULL, 1, 1633518506, 1633688250)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (33, N'Aftab Ahmad', N'aftab.era@gmail.com', NULL, 1, 1633688285, 1633688305)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (34, N'New User', N'agent_portal@mailinator.com', NULL, 1, 1633712581, 1633712719)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (35, N'dstestinguser', N'dstefanidis@intelli-corp.com', NULL, 1, 1640710087, 1640710160)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (36, N'emolyvas', N'emolyvas@intelli-corp.com', NULL, 1, 1640763407, 1640769561)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (37, N'ekalyvas', N'ekalyvas@intellitest.gr', NULL, 0, 1640763789, 1640763789)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1035, N'newTestUser', N'stagingtest@mailinator.com', NULL, 1, 1643953022, 1643953357)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1036, N'stagingReport', N'stagingReport@mailinator.com', NULL, 1, 1643953471, 1643953471)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1037, N'testinguserstagging', N'testuserStag@mailinator.com', NULL, 1, 1643953923, 1643953923)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1038, N'Test User', N'aftab.era1@mailinator.com', NULL, 1, 1644405357, 1644405357)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1040, N'Test User', N'demouser@mailinator.com', NULL, 1, 1646647201, 1646648893)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1041, N'khawaja faizan', N'khawaja.faizan@atomicity-interactive.com', NULL, 1, 1646995679, 1646995988)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1042, N'awais akmal', N'awais.akmal@mailinator.com', NULL, 1, 1647495830, 1647496407)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1043, N'Super Admin', N'superadmin@mailinator.com', N'', 1, 0, 0)
GO
INSERT [dbo].[SystemUsers] ([Id], [FullName], [Email], [Jmbg], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1044, N'Test_User1', N'test_user1@mailinator.com', NULL, 1, 1647840533, 1647840551)
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0488c7b9-21d5-4aad-a69d-7d9d0a45fc3d', 35, N'dstefanidis@intelli-corp.com', N'DSTEFANIDIS@INTELLI-CORP.COM', N'dstefanidis@intelli-corp.com', N'DSTEFANIDIS@INTELLI-CORP.COM', 1, N'AQAAAAEAACcQAAAAEHRMR3fHt4XRrb1If/8eG3ZDhMq8yUevcnAJByOzFjnD7kweIBONgPUM9AJRTOPtjw==', N'PWGGWFJI52RWIYIF6DZB7XKPBUNE5ESS', N'2b91e1ae-73f6-407f-ad99-d8e49b8f9a33', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0dc13f2b-4591-422b-b6ff-5cd3a77748af', 9, N'asdasda@gmail.com', N'ASDASDA@GMAIL.COM', N'asdasda@gmail.com', N'ASDASDA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAED2N+/KrCrl73jcxprJgtpTUy2YqYZdbqaDm0md/3M30pmVZGJxMradTsNx1FXIwGg==', N'3B4JK22OXBJYO22XRIIQCYA7DC56BDFV', N'3a40d495-11cb-4e8f-8047-731ba39a20e3', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0e0a1082-017f-4231-82f7-97b72d32061b', 30, N'aftab.era@mailinator.com', N'AFTAB.ERA@MAILINATOR.COM', N'aftab.era@mailinator.com', N'AFTAB.ERA@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEEPK6oNY/rNhY0+YcMPf8vwSH7AhGB/Qi4QN6fVxFtsGGWjS4fyUKSU284mZi6hMRA==', N'O4HLJLRHFOFFC7HRYHPQFDK7GG4GXRET', N'96b72057-b9bd-452b-9812-453f577ed0bf', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1115b46e-76ce-4228-808c-b98d264398f7', 20, N'aftab.ahmad@atomicity-interactive.com', N'AFTAB.AHMAD@ATOMICITY-INTERACTIVE.COM', N'aftab.ahmad@atomicity-interactive.com', N'AFTAB.AHMAD@ATOMICITY-INTERACTIVE.COM', 1, N'AQAAAAEAACcQAAAAEGELITXoaOKws8vbel6xL0scqjyBuxDTEDPV1E8tbGKv5w2rh26q0uXTlhR5cJ8NmA==', N'GDFRFOBSCULSKV64J4QIUYUS7JGEDGKT', N'a2baa0ea-9276-4850-b9d3-804b02db362b', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1cb0e6f3-a93c-4016-af6c-973e143fab9e', 1035, N'stagingtest@mailinator.com', N'STAGINGTEST@MAILINATOR.COM', N'stagingtest@mailinator.com', N'STAGINGTEST@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEMOA37oLDjhY62RZtikHFdMOZYa4HWUQdWweuibQv9Fs9tVWgJyxFGZYq1oGssLXbA==', N'IL2YD2NPOMB6W3IWBQXPUGHY6HMG4RNW', N'613ed473-6ff5-4eff-b798-bf6ab46b04df', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1ccd7b77-7159-47da-827b-2da52d04e5aa', 32, N'test_user_agent@mailinator.com', N'TEST_USER_AGENT@MAILINATOR.COM', N'test_user_agent@mailinator.com', N'TEST_USER_AGENT@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKF8F9EAPy5wwC90Z/4wuJk4RRPCsegRHvm4T03FPsL998E+WqLyDlHAimhND6Y0AQ==', N'UWSVQSICMSA3FCPAABFOAL6CCY3SI4LP', N'8d48063a-cb14-48b9-96ce-7be0fe900c4f', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1e591e75-83d1-4073-a8df-1ffd85b66567', 1043, N'superadmin@mailinator.com', N'SUPERADMIN@MAILINATOR.COM', N'superadmin@mailinator.com', N'SUPERADMIN@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'aec70b38-ba76-4848-a42c-5e853589dce1', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'21db6f90-6eff-4b8a-b9a6-66104fd4cd30', 5, N'hellotoast@gmail.com', N'HELLOTOAST@GMAIL.COM', N'hellotoast@gmail.com', N'HELLOTOAST@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEIY9grTD5uJQdJRMf69TDGciy4pc+AxnAazdorW/5Q9KnfmZVoNf8Vsf3FIjoN1Iiw==', N'5IFEKQS77URTW3EPSMMFAPRUEK3LYYOW', N'bdd3ae64-5d0b-47fd-a5e8-136dbdf0aed0', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'267a9add-2de2-48df-919d-6ad1d0f94307', 1042, N'awais.akmal@mailinator.com', N'AWAIS.AKMAL@MAILINATOR.COM', N'awais.akmal@mailinator.com', N'AWAIS.AKMAL@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'7XBFRAS4FPS27SX2GVWKZO7PBSP4W67C', N'8e884b29-9fee-4d2e-8dba-507db625f917', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2e98da04-ef64-4618-b2cb-5c4eeef89a50', 1038, N'aftab.era1@mailinator.com', N'AFTAB.ERA1@MAILINATOR.COM', N'aftab.era1@mailinator.com', N'AFTAB.ERA1@MAILINATOR.COM', 0, N'AQAAAAEAACcQAAAAELxk6oPoiZdNuLLFZblNp1JP4tyT/WKOua1w4evOnk4N/G0PjXH9GwCGo13LhkRmBw==', N'KXOV3AF7LN6NZGY66DBVRLX2QSG2JGRM', N'de6d1ee6-ecf0-4188-a21d-d5e516aa91e3', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3531fe2d-4810-4657-af2a-2b375f80a9c9', 14, N'test43@gmail.com', N'TEST43@GMAIL.COM', N'test43@gmail.com', N'TEST43@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEHuztyPlcGubNp4vvkYcqtrhyFG2EpaERYz5xpPbY3QqSIQYBah2MsC0Buj2F89XEw==', N'YOAAKADYKPRULX2BYXIQYSR224QCXG5T', N'e9d13e35-8aa3-446e-bc3c-b6a2c2784498', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3c94a3f8-e7e7-435f-adb4-083de22b614b', 8, N'todelete@mailinator.com', N'TODELETE@MAILINATOR.COM', N'todelete@mailinator.com', N'TODELETE@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEMrNQ9Jkp8djf2VEurLdYFat4BpHlgxJKIkcyOnEsf4mrqdB5QwomUQ/ty6m6lQwYA==', N'RTFECNWKAHZAD56WCTN6HE7QS4E75EGX', N'ec1f9dd3-2dba-411f-9ed4-f7af7a336249', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'497abd1b-b4be-4197-9c35-b6a44097ec40', 12, N'msallis@intelli-corp.com', N'MSALLIS@INTELLI-CORP.COM', N'msallis@intelli-corp.com', N'MSALLIS@INTELLI-CORP.COM', 1, N'AQAAAAEAACcQAAAAEHKdWGaPD3DcvGyWAz/+msclXBHbs06aPPlsW2wIvUa9qwXpf6V8nzJboTtPQro5bw==', N'FXP4FI77YJZZUQRT67NFYHXZY6YTWIMW', N'e730c4a1-b3eb-4753-b01a-aab896ace835', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'573a25ff-fae9-44c3-b8b0-9a57a11fcf57', 15, N'admin@test.com', N'ADMIN@TEST.COM', N'admin@test.com', N'ADMIN@TEST.COM', 1, N'AQAAAAEAACcQAAAAEP0ONsA87MVNSZ15GpppwwlmCYQFXpmqmvsgfA9LBbMSxe5DY5jpyugYKy0j0nCAWg==', N'FFDAY6LQWSKGVWHW4D2B6NU2BDRG6STL', N'5e372a3a-e752-450e-8e46-ac139536f4ec', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'58e9b846-9c7c-4919-9184-f372233b6d5a', 1037, N'testuserStag@mailinator.com', N'TESTUSERSTAG@MAILINATOR.COM', N'testuserStag@mailinator.com', N'TESTUSERSTAG@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEFpd+jBogZW/Iw7RUYrGg9CymZPvJCvu1srjBZ+9pzZNkDejErPJ1jGMub/PxjeTaA==', N'IYTHNGCFK7CANLZYO2S74YPG2HC7XXVX', N'587a02d4-dd7f-428b-9405-f5929f29328d', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5e83051f-1a68-4405-8b65-4cb2072f39f0', 27, N'testUserM@mailinator.com', N'TESTUSERM@MAILINATOR.COM', N'testUserM@mailinator.com', N'TESTUSERM@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEK8FzUndW7l0GnQOQrp2EP/53ePDkI+6Y9atxCUGFsI3N1+93Bk20ANWiP8HOJY60w==', N'IUVTXTISQZPKXJOOSREJDQPRARKKU4CW', N'792e97f4-7084-4652-bf4c-d253d2340134', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'60167649-304f-49a6-9962-e78cb8be7e3a', 13, N'test12@gmail.com', N'TEST12@GMAIL.COM', N'test12@gmail.com', N'TEST12@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGX4GjUlZ33RJ525HEG6q6Zkyn7l2NpgIWR8+AAI4NBHd+1P1T1PJaIlu1ED5cPIlQ==', N'NH43YCFGVLA7KVT6EN7FN2HILXWRESK6', N'9d28be4b-9cc6-4a64-a5b9-9afbb944624e', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7423be38-82c1-4dd4-81b2-ca3b7bcd583b', 4, N'admin@hazelsuite.com', N'ADMIN@HAZELSUITE.COM', N'admin@hazelsuite.com', N'ADMIN@HAZELSUITE.COM', 1, N'AQAAAAEAACcQAAAAENp5lTR5QZe1NdRmlLZSS6otJbnIE1LTGjhl34Si0MDBDrsPu94Xzb0LH/b73ABi1g==', N'SEAPCVARFW7Z3SSBBOHZ72IH6U6RZAZP', N'b5515d01-9408-46b7-a224-c9a6adc66dfe', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7a442ad8-e27a-4f65-b021-71883b1e7da7', 1, N'portaladmin@mailinator.com', N'PORTALADMIN@MAILINATOR.COM', N'portaladmin@mailinator.com', N'PORTALADMIN@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'aec70b38-ba76-4848-a42c-5e853589dce1', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'85c9e68d-f518-4d19-9a0c-a14b47ae560d', 1044, N'test_user1@mailinator.com', N'TEST_USER1@MAILINATOR.COM', N'test_user1@mailinator.com', N'TEST_USER1@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEM+V4P6CcQ9wO8C9DW+qbmAHgT83hUkfAinlPtb6cSXDiSKwy9oYKuWsuZr63vhXhw==', N'VAVONBTVCIJRUH4VTY4YTEHY3JEV4N7T', N'47c2fc3d-c4ee-4fbb-92ce-486355a446ed', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'87aea97e-c0ea-40d6-b77f-47007a1de5dd', 37, N'ekalyvas@intellitest.gr', N'EKALYVAS@INTELLITEST.GR', N'ekalyvas@intellitest.gr', N'EKALYVAS@INTELLITEST.GR', 0, N'AQAAAAEAACcQAAAAEDEp6O/2yVKOtguQaL8XtLFYNKBVaJPXpLzF3nMlkMs1XMsm0FsdW7+5yVG2Fyzpzg==', N'LV6KFIEM2S3U4SUOY53NH62EYICUIABV', N'657226d6-6d22-4091-8732-a98c50a0ad6f', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'89d9a505-24d8-48c6-a154-8183ff551c76', 1041, N'khawaja.faizan@atomicity-interactive.com', N'KHAWAJA.FAIZAN@ATOMICITY-INTERACTIVE.COM', N'khawaja.faizan@atomicity-interactive.com', N'KHAWAJA.FAIZAN@ATOMICITY-INTERACTIVE.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'aec70b38-ba76-4848-a42c-5e853589dce1', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8e4c43a0-a3bb-4429-a2ab-d5a67a494172', 36, N'emolyvas@intelli-corp.com', N'EMOLYVAS@INTELLI-CORP.COM', N'emolyvas@intelli-corp.com', N'EMOLYVAS@INTELLI-CORP.COM', 1, N'AQAAAAEAACcQAAAAEMCVtdiuJB2sjjHiIdujxCCLHhXLnZvutDwAg/rK0qSX7XufJQQB6Wzc0js3lkcxXw==', N'XPMSNQQPID7VE6X6WNRIU33RVBJ44CKZ', N'6c3a2888-ce51-4c07-9261-9e5d6b527b73', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8e9886ab-30a7-44fe-827a-35fc7488e37b', 19, N'xifeyih772@carpetd.com', N'XIFEYIH772@CARPETD.COM', N'xifeyih772@carpetd.com', N'XIFEYIH772@CARPETD.COM', 1, N'AQAAAAEAACcQAAAAEIVj1fFlmEki4XZ4xq+mElqcg+ydYsTJKSodmmk0tNOlzBl45g4LNgA4Wss9fCJsNQ==', N'ZUX52VOZQ27HV2X4JEEZVQS2XWXJH3HX', N'662562e3-99cd-48aa-a30d-df95e8f97060', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8ea127eb-0429-4d9b-9cdf-acd0e24b3375', 31, N'newuser1@mailinator.com', N'NEWUSER1@MAILINATOR.COM', N'newuser1@mailinator.com', N'NEWUSER1@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEO7TYbQ98V4JzLbueKEoBgOWzxmO6ATwoJfWJgHC78jGlMmPFmC3tdBXDHOVdXhe7Q==', N'4W7JAOZVRCJCOWU4D3RPIRSUDOR7LFVN', N'71db8ada-5935-4e62-a777-1278e5ec02a2', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9e166bc1-0779-4d0d-9792-be5263af4f90', 11, N'user2@mailinator.com', N'USER2@MAILINATOR.COM', N'user2@mailinator.com', N'USER2@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAECRzOgK2CfKiM2Eop+pNB+i6r1KrHwQl77xWM1MXJIVzle6ePijEGY1lY3VGiqut4w==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'466cddbf-2488-418b-863c-d8e4f5f2267a', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'af59ef65-ae9b-40a8-a46c-a2430c9c1fe0', 26, N'viraj42730@oemmeo.com', N'VIRAJ42730@OEMMEO.COM', N'viraj42730@oemmeo.com', N'VIRAJ42730@OEMMEO.COM', 1, N'AQAAAAEAACcQAAAAEN+lFya93LK9bHkUVWIq0frDY2wzEiVWJ0MLGyL0Vnxi1cL49uv6BhuofSDruK80eg==', N'WCGPYP64GCLE6WURUHR4QFXGZERBV4DU', N'fc692e00-02b8-4c21-b042-14fb9dc6fb7d', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c2a40dbe-9e7e-49c1-91aa-1f6d6dc3ea5a', 10, N'user1@mailinator.com', N'USER1@MAILINATOR.COM', N'user1@mailinator.com', N'USER1@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAECRzOgK2CfKiM2Eop+pNB+i6r1KrHwQl77xWM1MXJIVzle6ePijEGY1lY3VGiqut4w==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'a4d21fa7-ae23-4f7b-97a5-71e55a235751', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd02bb59b-34b1-4b74-bc32-709b1fb88832', 3, N'test@gmail.com', N'TEST@GMAIL.COM', N'test@gmail.com', N'TEST@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJd5LlAln22S3BDBNblGQaongpxXx+xoJN0EMHLpyZ2YfwrPGuR7d7mqr5UKJ6gSYw==', N'PKMYWDLVHDP42PB3JEC6O6CCWIL76HMQ', N'cdd6e396-f311-45b5-9c31-7a961697a117', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'da831c23-8362-4708-ac06-81b08b9517ca', 34, N'agent_portal@mailinator.com', N'AGENT_PORTAL@MAILINATOR.COM', N'agent_portal@mailinator.com', N'AGENT_PORTAL@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEBGPKaQiiYgHKAKTvKcd9izkvaQ9LLSggFUqo1xDAKl79ABgZOG+KGHWipbGv0iZiw==', N'JQ5OU2BVH2QY2KK7DVC3NYJ6KNB3P4JY', N'725dc352-8256-49fe-b735-a11f551b8cb4', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e71c6010-4c54-4c94-a97b-24d0fbb2a7f1', 7, N'alpha@mailinator.com', N'ALPHA@MAILINATOR.COM', N'alpha@mailinator.com', N'ALPHA@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEBSt0HIMjHpjgjpv/7QfF9C73LCuWyCLMuwwKCUZoZQR7fpYXJnALxCAMEzbVG4Yeg==', N'QCYSJ2ZMVXZ4VISM5N3KO4ORELKQT7DG', N'cad667c6-eba8-47ce-a7be-308d3ba858ab', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f0e22e23-f211-4c36-9598-00fd96d7d40a', 1036, N'stagingReport@mailinator.com', N'STAGINGREPORT@MAILINATOR.COM', N'stagingReport@mailinator.com', N'STAGINGREPORT@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEBumlNkeO1ptmavncf6N7MADTmbmbdPKB2zhwYNxLg4BhrHnjOZFsxHri/yLnOWLWw==', N'HKLAFMQURKRCO7EDT5ZVTI64IYFAULUV', N'e33b006d-a24e-4ea8-bcf5-67386102b5d5', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f60ccf92-55d3-44b9-b1ef-3cb74630cdd4', 18, N'testLoader1@gmail.com', N'TESTLOADER1@GMAIL.COM', N'testLoader1@gmail.com', N'TESTLOADER1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAENM7E6CrrS0jn0HT3ALcF7IYE1IRRbKYCh7gbB3p6HSEQZztqPyY4aD9rA3dxrjVOw==', N'TAEF2GEXF6ESV4XSZEYACYYTPVT5OMXB', N'3c426d2d-a607-4474-9cd2-035be1c34fc9', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fe543aef-a215-4bbc-bdf5-7204c4da47cd', 33, N'aftab.era@gmail.com', N'AFTAB.ERA@GMAIL.COM', N'aftab.era@gmail.com', N'AFTAB.ERA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEKOJScgwelqY33SDtu3JZQId2dw94tuyi+FqJ2E6JbweYuQtH1Nknp6O3DfqDEmDYw==', N'U7IA3KPURJV4FNS2AGY35XQ7TPCNENA3', N'aec70b38-ba76-4848-a42c-5e853589dce1', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [SystemUserId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fed43569-9dc0-49b5-8b74-007ea24b58ca', 1040, N'demouser@mailinator.com', N'DEMOUSER@MAILINATOR.COM', N'demouser@mailinator.com', N'DEMOUSER@MAILINATOR.COM', 1, N'AQAAAAEAACcQAAAAEKS5DIz5a60MDqZZ7EQJnbtRGfKzi3Mwv5uMN7eeZEl+R62htE1zrFBHOzkyWINzHg==', N'2XASZQQZJP4FNPJCNMXT67PUZMCTGF4M', N'2ba35f1b-0dd6-464c-81bf-f2b8df0db93b', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[BatchMeta] ON 
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (73, 16, 67, NULL, N'', 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (74, 16, 68, NULL, N'', 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (75, 16, 69, NULL, N'', 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (76, 16, 70, NULL, N'', 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (77, 16, 71, NULL, NULL, 1, 1647938869, 1647938869)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (78, 17, 67, NULL, N'', 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (79, 17, 68, NULL, N'', 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (80, 17, 69, NULL, N'', 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (81, 17, 70, NULL, N'', 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (82, 17, 71, NULL, NULL, 1, 1647938896, 1647938896)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (83, 18, 66, NULL, N'ddd', 1, 1647942635, 1647942635)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (84, 19, 66, NULL, N'ddd', 1, 1647943478, 1647943478)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (85, 20, 64, NULL, N'srdfsd', 1, 1647943717, 1647943717)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (86, 21, 67, NULL, N'dfa', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (87, 21, 68, NULL, N'2022-03-23', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (88, 21, 69, NULL, N'af', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (89, 21, 70, NULL, N'2022-03-23', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (90, 21, 71, NULL, N'2', 1, 1647945408, 1647945408)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (91, 22, 67, NULL, N'fd', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (92, 22, 68, NULL, N'', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (93, 22, 69, NULL, N'ff', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (94, 22, 70, NULL, N'2022-03-17', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (95, 22, 71, NULL, N'2', 1, 1647947362, 1647947362)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (96, 23, 74, NULL, N'Abdul Basit', 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (97, 23, 75, NULL, N'Abdul Ghaffar', 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (98, 23, 76, NULL, N'2000-01-01', 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (99, 23, 77, NULL, NULL, 1, 1647949865, 1647949865)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (100, 24, 74, NULL, N'Abdul Basit', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (101, 24, 75, NULL, N'Abdul Ghaffar', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (102, 24, 76, NULL, N'2020-06-09', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (103, 24, 77, NULL, N'1', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (104, 24, 78, NULL, N'Lahore, Pakistan', 1, 1647954967, 1647954967)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (105, 25, 74, NULL, N'Volton', 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (106, 25, 75, NULL, N'Awais', 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (107, 25, 76, NULL, N'2022-03-16', 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (108, 25, 77, NULL, N'1', 1, 1648216829, 1648216829)
GO
INSERT [dbo].[BatchMeta] ([Id], [BatchId], [DocumentClassFieldId], [DictionaryValueId], [FieldValue], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (109, 25, 78, NULL, N'sdcsdcsdcsdcsdc', 1, 1648216829, 1648216829)
GO
SET IDENTITY_INSERT [dbo].[BatchMeta] OFF
GO
SET IDENTITY_INSERT [dbo].[UserCompanies] ON 
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, 11, 7, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, 12, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (3, 13, 2, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (4, 15, 2, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (5, 18, 4, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (7, 26, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, 31, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (12, 34, 6, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (13, 35, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (14, 36, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, 37, 1, NULL, 0, 0)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (20, 20, 1, 1, 1643952780, 1643952780)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (21, 20, 6, 1, 1643952780, 1643952780)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (24, 1035, 8, 1, 1643953357, 1643953357)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (25, 1036, 8, 1, 1643953471, 1643953471)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (26, 1037, 8, 1, 1643953923, 1643953923)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (27, 27, 1, 1, 1643983669, 1643983669)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (37, 30, 2, 1, 1644404978, 1644404978)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (38, 1038, 2, 1, 1644405357, 1644405357)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (40, 1040, 1, 1, 1646648893, 1646648893)
GO
INSERT [dbo].[UserCompanies] ([Id], [SystemUserId], [CompanyId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (41, 1044, 1, 1, 1647840551, 1647840551)
GO
SET IDENTITY_INSERT [dbo].[UserCompanies] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 
GO
INSERT [dbo].[Countries] ([Id], [CountryName], [Description], [Code2D], [Code3D], [MobileCode], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, N'Greece', N'', N'GR', N'GRC', N'+30', 1, 0, 0)
GO
INSERT [dbo].[Countries] ([Id], [CountryName], [Description], [Code2D], [Code3D], [MobileCode], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2, N'Italy', N'', N'IT', N'ITA', N'+39', 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUserCountries] ON 
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, 13, 2, 1, 1632916981, 1632916981)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, 15, 2, 1, 1632917865, 1632917865)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (22, 19, 1, 1, 1633002217, 1633002217)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (29, 32, 1, 1, 1633688250, 1633688250)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (30, 32, 2, 1, 1633688250, 1633688250)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (33, 33, 1, 1, 1633688305, 1633688305)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (34, 33, 2, 1, 1633688305, 1633688305)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (37, 31, 1, 1, 1636720776, 1636720776)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (38, 11, 1, 1, 1636720804, 1636720804)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (39, 11, 2, 1, 1636720804, 1636720804)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (40, 12, 1, 1, 1638787580, 1638787580)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (41, 12, 2, 1, 1638787580, 1638787580)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1038, 35, 2, 1, 1640710160, 1640710160)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1039, 35, 1, 1, 1640710160, 1640710160)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1041, 37, 2, 1, 1640763790, 1640763790)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1043, 36, 1, 1, 1640769561, 1640769561)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2037, 20, 1, 1, 1643952780, 1643952780)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2040, 1035, 2, 1, 1643953357, 1643953357)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2041, 1036, 2, 1, 1643953471, 1643953471)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2042, 1037, 1, 1, 1643953923, 1643953923)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2043, 1037, 2, 1, 1643953923, 1643953923)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2056, 30, 1, 1, 1644404978, 1644404978)
GO
INSERT [dbo].[SystemUserCountries] ([Id], [SystemUserId], [CountryId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2057, 30, 2, 1, 1644404978, 1644404978)
GO
SET IDENTITY_INSERT [dbo].[SystemUserCountries] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleScreens] ON 
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (8, 13, 1, 0, 1, 1632233841, 1632233841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (9, 11, 1, 0, 1, 1632234069, 1632234069)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (10, 10, 1, 0, 1, 1632234126, 1632234126)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (15, 12, 1, 0, 1, 1632234991, 1632234991)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (23, 9, 1, 0, 1, 1632245618, 1632245618)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (109, 19, 1, 1, 1, 1632301499, 1632301499)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (110, 19, 2, 0, 1, 1632301499, 1632301499)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (112, 19, 4, 0, 1, 1632301499, 1632301499)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (113, 19, 5, 0, 1, 1632301499, 1632301499)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (114, 19, 6, 0, 1, 1632301499, 1632301499)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (116, 19, 3, 1, 1, 1632301538, 1632301538)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (117, 17, 1, 0, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (118, 17, 2, 0, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (119, 17, 3, 0, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (120, 17, 4, 0, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (121, 17, 5, 0, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (122, 17, 6, 1, 1, 1632305075, 1632305075)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (143, 20, 3, 1, 1, 1632389531, 1632389531)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (144, 22, 1, 1, 1, 1632485461, 1632485461)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (145, 22, 2, 1, 1, 1632485471, 1632485471)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (154, 18, 2, 2, 1, 1632650841, 1632650841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (155, 18, 3, 2, 1, 1632650841, 1632650841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (156, 18, 4, 2, 1, 1632650841, 1632650841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (157, 18, 5, 1, 1, 1632650841, 1632650841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (158, 18, 6, 0, 1, 1632650841, 1632650841)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (159, 18, 1, 1, 1, 1632662958, 1632662958)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (375, 23, 5, 1, 1, 1632946958, 1632946958)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (529, 15, 1, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (530, 15, 8, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (531, 15, 7, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (532, 15, 6, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (534, 15, 4, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (535, 15, 3, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (536, 15, 2, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (537, 15, 9, 0, 1, 1633505033, 1633505033)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (544, 15, 5, 1, 1, 1633506658, 1633506658)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (680, 37, 1, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (681, 37, 2, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (682, 37, 3, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (683, 37, 4, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (684, 37, 5, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (685, 37, 6, 1, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (686, 37, 7, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (687, 37, 8, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (688, 37, 9, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (689, 37, 10, 0, 1, 1638462859, 1638462859)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (690, 3, 10, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (691, 3, 8, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (692, 3, 7, 2, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (693, 3, 6, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (694, 3, 5, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (695, 3, 4, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (696, 3, 3, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (697, 3, 2, 2, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (698, 3, 9, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (699, 3, 1, 0, 1, 1638787649, 1638787649)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1739, 1037, 1, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1740, 1037, 2, 2, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1741, 1037, 3, 2, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1742, 1037, 4, 2, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1743, 1037, 5, 2, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1744, 1037, 6, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1745, 1037, 7, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1746, 1037, 8, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1747, 1037, 9, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1748, 1037, 10, 0, 1, 1644388230, 1644388230)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1749, 1041, 1, 2, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1750, 1041, 2, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1751, 1041, 3, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1752, 1041, 4, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1753, 1041, 5, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1754, 1041, 6, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1755, 1041, 7, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1756, 1041, 8, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1757, 1041, 9, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1758, 1041, 10, 0, 1, 1646647780, 1646647780)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1759, 2, 1, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1760, 2, 2, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1761, 2, 3, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1762, 2, 4, 2, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1763, 2, 5, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1764, 2, 6, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1765, 2, 7, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1766, 2, 8, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1767, 2, 9, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1768, 2, 10, 0, 1, 1646649011, 1646649011)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1904, 1, 1, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1905, 1, 20, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1906, 1, 2, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1907, 1, 3, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1908, 1, 4, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1909, 1, 5, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1910, 1, 6, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1911, 1, 7, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1912, 1, 8, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1913, 1, 9, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1914, 1, 21, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1915, 1, 10, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1916, 1, 12, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1917, 1, 13, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1918, 1, 14, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1919, 1, 15, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1920, 1, 16, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1921, 1, 17, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1922, 1, 18, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1923, 1, 19, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1924, 1, 11, 2, 1, 1647857717, 1647857717)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1925, 1045, 21, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1926, 1045, 20, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1927, 1045, 19, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1928, 1045, 18, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1929, 1045, 17, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1930, 1045, 16, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1931, 1045, 15, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1932, 1045, 14, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1933, 1045, 13, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1934, 1045, 12, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1935, 1045, 10, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1936, 1045, 9, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1937, 1045, 8, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1938, 1045, 7, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1939, 1045, 6, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1940, 1045, 5, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1941, 1045, 4, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1942, 1045, 3, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1943, 1045, 2, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1944, 1045, 11, 0, 1, 1647959394, 1647959394)
GO
INSERT [dbo].[RoleScreens] ([Id], [SystemRoleId], [ScreenId], [Privilege], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1945, 1045, 1, 1, 1, 1647959394, 1647959394)
GO
SET IDENTITY_INSERT [dbo].[RoleScreens] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUserRole] ON 
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (0, 1, 1, 1, 0, 0)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (47, 13, 12, 1, 1632916981, 1632916981)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (48, 15, 1, 1, 1632917865, 1632917865)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (49, 15, 15, 1, 1632917865, 1632917865)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (60, 19, 3, 1, 1633002217, 1633002217)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (61, 19, 15, 1, 1633002217, 1633002217)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (73, 31, 3, 1, 1636720776, 1636720776)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (74, 11, 15, 1, 1636720804, 1636720804)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (75, 12, 3, 1, 1638787580, 1638787580)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1074, 35, 3, 1, 1640710160, 1640710160)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1076, 37, 12, 1, 1640763790, 1640763790)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1078, 36, 1, 1, 1640769561, 1640769561)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2075, 1035, 1038, 1, 1643953357, 1643953357)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2076, 1036, 1039, 1, 1643953471, 1643953471)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2077, 1037, 1038, 1, 1643953923, 1643953923)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2078, 27, 2, 1, 1643983669, 1643983669)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2089, 30, 1037, 1, 1644404978, 1644404978)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2090, 1038, 1043, 1, 1644405357, 1644405357)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2091, 1040, 2, 1, 1646648893, 1646648893)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2093, 1041, 1, 1, 1646995988, 1646995988)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2095, 1042, 1, 1, 1647496407, 1647496407)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2096, 33, 1, 1, 1, 1)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2097, 1043, 1, 1, 0, 0)
GO
INSERT [dbo].[SystemUserRole] ([Id], [SystemUserId], [SystemRoleId], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (2098, 1044, 1, 1, 1647840551, 1647840551)
GO
SET IDENTITY_INSERT [dbo].[SystemUserRole] OFF
GO
SET IDENTITY_INSERT [dbo].[PasswordHistory] ON 
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1, 3, 1632981841, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2, 19, 1633092976, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (3, 19, 1633174751, N'6158C8539A9A8D3DFD06A54EC838643110B025BFB14E1CEC75CD22314FA9574A')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (4, 19, 1633174939, N'C5BCC2116CF6BC445DDA3B9443000F616960D009CED761EC88BF5C07A9B6D330')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (5, 19, 1633179079, N'3C969D1DFF472FD0AC19028B4A31B064F869764FF1F8FF88DF403A00BAC7EBB4')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (6, 19, 1633179950, N'8350BE2BE282619220F5017EFC7A49DD7E24D11A63ADAE6211D942472840DB58')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (7, 20, 1633327306, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (8, 20, 1633327619, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (9, 20, 1633327807, N'6158C8539A9A8D3DFD06A54EC838643110B025BFB14E1CEC75CD22314FA9574A')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (10, 26, 1633372922, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (11, 26, 1633372964, N'6C8DB230CF5DC7FA901E0B275D60A01273713A5E8250198E06D03BC1C86CD88D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (12, 26, 1633373077, N'C5BCC2116CF6BC445DDA3B9443000F616960D009CED761EC88BF5C07A9B6D330')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (13, 26, 1633373352, N'9635EF51043F503CB8B24ECBA13A218A37BE42D51D6E19DC5E60247B9E67E7B1')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (14, 26, 1633415622, N'597EDBBC725E814B7A1CA818D4E9D7A2E41A80CE056A2CB152D9BC439E90A879')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (15, 26, 1633415646, N'955A1C7306A402775889D31E74237341F635AE34DAAF973E698692748F076AF9')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (16, 1, 1633418492, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (17, 1, 1633419894, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (18, 1, 1633420054, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (19, 30, 1633500679, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (20, 30, 1633500890, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (21, 30, 1633500997, N'6158C8539A9A8D3DFD06A54EC838643110B025BFB14E1CEC75CD22314FA9574A')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (22, 1, 1633506413, N'6158C8539A9A8D3DFD06A54EC838643110B025BFB14E1CEC75CD22314FA9574A')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (23, 1, 1633506456, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (24, 30, 1633507087, N'327B2AC4A2ADD6171DD74403E9CAAD5AA2FEE1796CA3A451934D2B82CAE27848')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (25, 31, 1633516974, N'3EB3FE66B31E3B4D10FA70B5CAD49C7112294AF6AE4E476A1C405155D45AA121')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (26, 32, 1633518563, N'6273FD1780A709387F9F59B2ACBDD0848333051BD7DEC15BF9C3BD9EA9DF80A8')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (27, 32, 1633519032, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (28, 1, 1633590401, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (29, 1, 1633590441, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (30, 30, 1633694088, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (31, 20, 1633694242, N'327B15672622EC0CCD93B26C7330DD61FBE44835F721ECEB765FDAF2C605EF2B')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (32, 20, 1633711875, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (33, 34, 1633712813, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (34, 31, 1634724907, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (35, 27, 1634725004, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (36, 12, 1638787534, N'448705B5ECF51AD4A001E0E708F23B5CE0D21CA921A7FFF6327F021D076656DB')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1034, 35, 1640710206, N'673DD8826D886B1DB11E073771F24A98DD97435CD2743BFBC28B5AE3F2E539AF')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1035, 36, 1640763491, N'E3A321625408D6070F03223AF33CF68236527320C46F193EFDA27FED9FB84EC0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1036, 36, 1640764766, N'C2EE67027E93894E8F49788572D8952212E7687FD2E39552C2294D39E987AF23')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1037, 36, 1640765106, N'75AF067C06EC4C03B4BE8748AA2AB37FF1C52623C5149A51BC3F5619049A26D8')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1038, 35, 1641977053, N'FF12E47EC0A70896932B8E5959C45F95212F3968A026DDD87F7C422A577415F8')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1039, 1, 1642583655, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (1040, 20, 1643028007, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2034, 1036, 1643953634, N'B0AC06BFBEF480AE556A43845B9A313931A2EBE31D0ED532B8D619E72911404D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2035, 1037, 1643954001, N'C417478690C82736B4968E043ED7FC59A38212469FB35D19FF20C40868D6274D')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2036, 30, 1644388836, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2037, 1040, 1646648408, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
INSERT [dbo].[PasswordHistory] ([Id], [SystemUserId], [ChangedAt], [PasswordHash]) VALUES (2038, 1044, 1647841943, N'F293D539D659C9369D66F9FDAF2D1E1BE8651CAA41D6631F7D0FC6874BBD84D0')
GO
SET IDENTITY_INSERT [dbo].[PasswordHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[UserPreferences] ON 
GO
INSERT [dbo].[UserPreferences] ([Id], [Language], [IsActive], [CreatedAt], [UpdatedAt], [GridPageSize], [SystemUserId]) VALUES (1, N'en-US', 1, 1632378656, 1648121395, 10, 1)
GO
INSERT [dbo].[UserPreferences] ([Id], [Language], [IsActive], [CreatedAt], [UpdatedAt], [GridPageSize], [SystemUserId]) VALUES (2, N'en-US', 1, 1632650666, 1632732727, 10, 11)
GO
INSERT [dbo].[UserPreferences] ([Id], [Language], [IsActive], [CreatedAt], [UpdatedAt], [GridPageSize], [SystemUserId]) VALUES (3, N'en-US', 1, 1640763903, 1640763903, 20, 36)
GO
SET IDENTITY_INSERT [dbo].[UserPreferences] OFF
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1, N'002c9838-54d2-469b-81af-89759a4180df')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (33, N'13b3bf38-6eb2-4baa-bebd-e69319e35b66')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (34, N'000d611d-b6c0-495a-89ed-60ae8cb2be63')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1035, N'c4678543-34ae-4941-bc48-2b6ffb51a94e')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1036, N'57d5fb90-1f74-48f2-91c0-7d63501f34e1')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1041, N'7f384e74-727d-4f56-91ef-461447345ee4')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1042, N'0564a5bf-151b-4f6c-b0d7-5cfdcd2fe553')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1043, N'fb5a91c2-b961-4a5d-ad2f-15332e49de72')
GO
INSERT [dbo].[UserSessions] ([SystemUserId], [SessionId]) VALUES (1044, N'74f67670-2c79-47f0-ac2a-bdebf20d88ba')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309034533_InitialMigration', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309062102_InitialData', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309080355_addDocumentTypeScreen', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309081400_AddRoleSreenOfDocumentTypeScreen', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309083654_AddScreenElementsOfDocumentTypeScreen', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309084254_AddRoleScreenElementsOfDocumentTypeScreen', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309091645_RemoveSystemUserViewTable', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309091739_RemoveSystemUserViewTableFromDB', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309093548_AddDocumentCategoryTable', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309094021_AddDocumentSubCategoryTable', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309112223_SeedDocumentCategoryAndSubCategoryScreens', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220309135127_SeedScreensToSuperAdmin', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310092627_AutoIncrementAddedInDocumentType', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310105713_SeedDocumentClassScreenAndAddRole', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310110501_AddScreenElementOfDocumentClassScreen', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310112018_SeedScreensToSuperAdminUpdate', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220310121404_AddColumnsInDocumentClass', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315111516_DictionaryModelChanges', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315141047_EnableIdentityInsert', N'5.0.9')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220315141245_RemovedValueGeneratedNeverForDocumentClassField', N'5.0.9')
GO
SET IDENTITY_INSERT [dbo].[Configurations] ON 
GO
INSERT [dbo].[Configurations] ([Id], [PasswordRequireNonAlphanumeric], [PasswordRequireLowercase], [PasswordRequireUppercase], [PasswordRequireDigit], [PasswordRequiredLength], [RestrictLastUsedPasswords], [ForcePasswordChangeDays], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (1, 1, 1, 1, 1, 8, 3, 99, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Configurations] OFF
GO

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
