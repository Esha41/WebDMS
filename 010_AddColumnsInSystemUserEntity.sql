ALTER TABLE SystemUsers
ADD [IsActiveDirectoryUser] [bit] NOT NULL default '0';

ALTER TABLE SystemUsers
ADD [OutlookEmail] [nvarchar](256) NOT NULL default 'inteli.code.com';

INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('UserOutLookEmail',4,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))
