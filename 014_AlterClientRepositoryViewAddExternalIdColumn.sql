ALTER  View [dbo].[ClientRepositoryView] As
									SELECT Repo.[Id] 
										,Repo.[AppliedGDPR]
										,cus.[Id] as ClientId
										,cus.[FirstName]+' '+cus.[LastName] as ClientName
										,cus.ExternalId
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