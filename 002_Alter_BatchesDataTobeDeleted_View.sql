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