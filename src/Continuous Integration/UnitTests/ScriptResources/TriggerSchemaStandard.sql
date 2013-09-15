CREATE TRIGGER [MySchema].[TriggerSchemaStandard]
    ON [MySchema].[TriggerTable]
    FOR DELETE, INSERT, UPDATE 
    AS 
    BEGIN
    	SET NOCOUNT ON;
    END