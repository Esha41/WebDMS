alter table Companies
Add  ActiveDirectoryDomainName nvarchar(50) not null DEFAULT 'AD.Com'

INSERT INTO [dbo].[ScreenElements] ([ScreenElementName],[ScreenId],[IsActive],[CreatedAt],[UpdatedAt]) VALUES ('ActiveDirectoryDomainNameInput',5,1,DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()),DATEDIFF(s, '1970-01-01 00:00:00', GETDATE()))

update Companies Set Username='user' where Username IS NULL
alter table Companies
alter COLUMN Username nvarchar(50) not null 

update Companies Set Password='9KgTDfW90XDQ5codx2olOg==' where Password IS NULL
alter table Companies
alter COLUMN Password nvarchar(50) not null 