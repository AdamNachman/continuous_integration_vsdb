CREATE SERVICE [ServiceWithOwner]
	AUTHORIZATION [MySchema]
	ON QUEUE [dbo].[Queue1]
	( 
	    [Contract1]
	);