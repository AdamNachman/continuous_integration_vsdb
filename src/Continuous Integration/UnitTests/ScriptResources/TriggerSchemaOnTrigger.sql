CREATE TRIGGER [MySchema].[TriggerSchemaOnTrigger]
    ON [TriggerTable]
    FOR DELETE, INSERT, UPDATE 
    AS 
    BEGIN
    	SET NOCOUNT ON;
    END