CREATE TRIGGER [TriggerNoSchema]
    ON [TriggerTable]
    FOR DELETE, INSERT, UPDATE 
    AS 
    BEGIN
    	SET NOCOUNT ON;
    END
