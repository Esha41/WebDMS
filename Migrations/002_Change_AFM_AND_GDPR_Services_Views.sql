--Note -- Run these Query One by One in seperate Query Window 

--Step 1 --
update c Set Afm=c.Id  from Clients c
ALTER TABLE Clients ALTER  COLUMN Afm int;

--Step 2 --
ALTER View [dbo].[BatchesDataTobeDeleted] As
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
				)
			    )
--Step 3 --
ALTER View [dbo].[ClientsDataToBeDeleted] As
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