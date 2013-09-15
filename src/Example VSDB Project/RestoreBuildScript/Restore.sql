ALTER DATABASE [my_sample_database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

RESTORE DATABASE [my_sample_database]
FROM DISK='C:\Backups\my_sample_database.bak' WITH REPLACE,
MOVE 'my_sample_database_Data' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\my_sample_database.mdf',
MOVE 'my_sample_database_Log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\my_sample_database.ldf' ,RECOVERY;


ALTER DATABASE [my_sample_database] SET MULTI_USER;