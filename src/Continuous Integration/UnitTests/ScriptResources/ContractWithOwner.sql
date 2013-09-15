CREATE CONTRACT [ContractWithOwner]
	AUTHORIZATION [MySchema]
(
	[MessageType1] SENT BY INITIATOR,
	[MessageType2] SENT BY TARGET
)