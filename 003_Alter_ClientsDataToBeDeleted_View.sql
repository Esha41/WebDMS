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