﻿/*
Do not change the database name.
It will be properly coded for build and deployment
This is using sqlcmd variable substitution
*/
ALTER DATABASE [$(DatabaseName)]
    ADD FILEGROUP [MyFilegroup]