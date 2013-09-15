GRANT EXECUTE ON [dbo].[fncOne] TO [MyDatabaseRole]
GO
GRANT EXECUTE ON [MySchema].[fncThree] TO [MyDatabaseRole]
GO
GRANT EXECUTE ON [dbo].[sprOne] TO [MyDatabaseRole]
GO
GRANT EXECUTE ON [dbo].[sprTwo] TO [MyDatabaseRole]
GO
GRANT EXECUTE ON [MySchema].[sprOne] TO [MyDatabaseRole]
GO
GRANT EXECUTE ON [dbo].[sprThree] TO [MyDatabaseRole]
GO
GRANT SELECT ON [dbo].[fncTwo] TO [MyDatabaseRole]
GO
GRANT SELECT ON [dbo].[ViewOne] TO [MyDatabaseRole]
GO
GRANT SELECT ON [dbo].[ViewTwo] TO [MyDatabaseRole]
GO
GRANT SELECT,UPDATE,INSERT,DELETE ON [MySchema].[TableFour] TO [MyOtherDatabaseRole]
GO
GRANT SELECT,UPDATE,INSERT,DELETE ON [MySchema].[TableFive] TO [MyOtherDatabaseRole]
GO
GRANT SELECT,UPDATE,INSERT,DELETE ON [MySchema].[TableSix] TO [MyOtherDatabaseRole]
GO
GRANT SELECT,UPDATE,INSERT,DELETE ON [MySchema].[TriggerTable] TO [MyOtherDatabaseRole]
GO
