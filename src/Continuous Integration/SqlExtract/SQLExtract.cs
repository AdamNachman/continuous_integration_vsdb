// <copyright file="Extractor.cs" company="Adam Nachman">
// Copyright (c) 2009 All Right Reserved Adam Nachman
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Adam Nachman</author>
namespace AdamNachman.Build.SqlExtract
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml.Serialization;
    using AdamNachman.Build.SqlExtract.Enums;
    using AdamNachman.Build.SqlExtract.Parsers;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The extractor class that parses the sql files and generates a deploy script
    /// </summary>
    public class Extractor
    {
        #region Private Variables

        private string extractFromServer = string.Empty;
        private string extractFromDatabase = string.Empty;
        private string extractTargetFile = string.Empty;
        private string extractCommands = string.Empty;
        private string appendDatabaseName = string.Empty;
        private bool appendDatabaseNameToScripts = false;
        private Database database;
        private Dictionary<SqlFileTypes, ProcessDatabase> scriptProcessors;
        private string permissionsTemplate = string.Empty;
        private string permissionsTargetFile = string.Empty;
        private string sqlUsername = string.Empty;
        private string sqlPassword = string.Empty;
        private string extractUsername = string.Empty;
        private string extractPassword = string.Empty;

        /// <summary>
        /// The name of the sql instance to use
        /// </summary>
        private string sqlInstanceName;

        /// <summary>
        /// The project GUID for the test deploy databaseName
        /// </summary>
        private string projectGUID;

        /// <summary>
        /// The build path
        /// </summary>
        private string buildPath;

        /// <summary>
        /// The list of sql files to process
        /// </summary>
        private List<string> sqlScripts;

        /// <summary>
        /// The list of custom files to process
        /// </summary>
        private List<string> customFiles;

        /// <summary>
        /// This is the string representation of the type used to check for existence on an existing schema/manifest/etc ...
        /// </summary>
        private string existenceCheckerProvider = string.Empty;

        /// <summary>
        /// This is the configuration required for an "existenceChecker" object. TODO - move the "existence checker" to a standard IOC pattern, and initialize from config.
        /// </summary>
        private string existenceCheckerConfiguration = string.Empty;

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Sets the SQL Server instance where build can check dependencies.
        /// </summary>
        public string SqlInstance
        {
            set
            {
                this.sqlInstanceName = value;
            }
        }

        /// <summary>
        /// Gets or sets the sql user that will be used to connect to the database. Leave blank to use windows authentication
        /// </summary>
        public string SqlUsername
        {
            get
            {
                return this.sqlUsername;
            }
            set
            {
                this.sqlUsername = value;
            }
        }

        /// <summary>
        /// Gets or sets the password that will be used to connect to the database
        /// </summary>
        public string SqlPassword
        {
            get
            {
                return this.sqlPassword;
            }
            set
            {
                this.sqlPassword = value;
            }
        }

        /// <summary>
        /// Sets the project GUID for the test deploy databaseName
        /// </summary>
        public string ProjectGUID
        {
            set
            {
                this.projectGUID = value;
            }
        }

        /// <summary>
        /// Sets the path of the output files.
        /// </summary>
        public string BuildPath
        {
            set
            {
                this.buildPath = value;
            }
        }

        /// <summary>
        /// Sets the schema scripts to process
        /// </summary>
        public List<string> SchemaScripts
        {
            set
            {
                this.sqlScripts = value;
            }
        }

        /// <summary>
        /// Sets the static files to include in the build output
        /// </summary>
        public List<string> StaticFiles
        {
            set
            {
                this.customFiles = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the drop files should be separate from the parent file
        /// </summary>
        public bool SplitDropToSeperateFile { get; set; }

        /// <summary>
        /// Sets a name to prepend to output scripts
        /// </summary>
        public string AppendDatabaseNameToScripts
        {
            set
            {
                this.appendDatabaseName = value.Trim();
                this.appendDatabaseNameToScripts = !string.IsNullOrEmpty(this.appendDatabaseName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not static data should be extracted
        /// </summary>
        public bool ExtractData { get; set; }

        /// <summary>
        /// Sets the server from which static data should be extracted
        /// </summary>
        public string ExtractFromServer
        {
            set
            {
                this.extractFromServer = value;
            }
        }

        /// <summary>
        /// Gets or sets the sql user that will be used to connect to the reference data  database. Leave blank to use windows authentication
        /// </summary>
        public string ExtractUsername
        {
            get
            {
                return this.extractUsername;
            }
            set
            {
                this.extractUsername = value;
            }
        }

        /// <summary>
        /// Gets or sets the password that will be used to connect to the reference data database
        /// </summary>
        public string ExtractPassword
        {
            get
            {
                return this.extractPassword;
            }
            set
            {
                this.extractPassword = value;
            }
        }

        /// <summary>
        /// Sets the database from which static data should be extracted
        /// </summary>
        public string ExtractFromDatabase
        {
            set
            {
                this.extractFromDatabase = value;
            }
        }

        /// <summary>
        /// Sets the file to where the output of the command extractor is to be saved
        /// </summary>
        public string ExtractTargetFile
        {
            set
            {
                this.extractTargetFile = value;
            }
        }

        /// <summary>
        /// Sets the file enumerating the SQL commands to be executed 
        /// </summary>
        public string ExtractCommandsFile
        {
            set
            {
                this.extractCommands = value;
            }
        }

        /// <summary>
        /// Sets the permissions template file containing the configuration for scripting permissions
        /// </summary>
        public string PermissionsTemplate
        {
            set
            {
                this.permissionsTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not permissions are to be scripted
        /// </summary>
        public bool ScriptPermissions { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether or not permissions are to be scripted
        /// </summary>
        public bool DropConstraints { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not drop scripts should be generated for synonyms
        /// </summary>
        /// <value><c>true</c> if drop scripts should be generated; otherwise, <c>false</c>.</value>
        public bool GenerateDropForSynonyms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to generate drop for default constraints.
        /// </summary>
        /// <value><c>true</c> if generate drop for default constraints; otherwise, <c>false</c>.</value>
        public bool GenerateDropForDefaultConstraints { get; set; }

        /// <summary>
        /// Sets the output file for the generated permissions
        /// </summary>
        public string PermissionsTargetFile
        {
            set
            {
                this.permissionsTargetFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the IExistenceChecker type string that will be created to perform an existence check on an object, as
        /// per the "AlterIfExists" property description
        /// </summary>
        public string ExistenceCheckerProvider
        {
            get
            {
                return this.existenceCheckerProvider;
            }
            set
            {
                this.existenceCheckerProvider = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the SqlExtract class is to use the ExistenceCheckerProvider object to determine whether or not an
        /// object already exists in a "source" schema (or manifest), and if so, do not create a drop statement, but
        /// instead modify the script to perform an ALTER
        /// <para></para>
        /// Currently, this check is only performed on stored procedures, functions and views.
        /// </summary>
        public bool AlterIfExists { get; set; }

        /// <summary>
        /// Gets or sets the configuration parameter used for the IExistenceChecker. 
        /// </summary>
        public string ExistenceCheckerConfiguration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to treat warnings as errors.
        /// </summary>
        public bool TreatWarningsAsErrors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not a warning should be raised on unmatched types
        /// </summary>
        /// <value><c>true</c> a warning will be raised on unmatched types; otherwise a message will simply be logged.</value>
        public bool WarnOnUnmatchedTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [extract assemblies].
        /// </summary>
        /// <value><c>true</c> if [extract assemblies]; otherwise, <c>false</c>.</value>
        public bool ExtractAssemblies { get; set; }

        /// <summary>
        /// Gets or sets the extract assemblies config.
        /// </summary>
        /// <value>The extract assemblies config.</value>
        public string ExtractAssembliesConfig { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="buildLogger">The logger instance to be used for all logging</param>
        /// <returns>False if unsuccessful</returns>
        public bool Execute(SqlDeployment.Interfaces.ILogger buildLogger)
        {
            Assembly thisDLL = Assembly.GetExecutingAssembly();
            AssemblyName an = thisDLL.GetName();

            Utilities.Logger = buildLogger;
            Utilities.Logger.LogInformation("Extract Database task version: {0}...", an.Version);

            ServerConnection connection = null;
            Server sqlServer;
            string db;

            db = string.Format("SQL_Extract_{0}", this.projectGUID);

            try
            {
                Utilities.Logger.LogInformation(string.Format("Attempting to connect to {0}...", this.sqlInstanceName));
                this.InitializeExistenceChecker();
                connection = new ServerConnection(this.sqlInstanceName);

                if (string.IsNullOrEmpty(this.sqlUsername))
                {
                    connection.LoginSecure = true;
                }
                else
                {
                    connection.LoginSecure = false;
                    connection.Login = this.sqlUsername;
                    connection.Password = this.sqlPassword;
                }
                connection.Connect();

                sqlServer = new Server(connection);
                this.CreateDatabase(sqlServer, db);
                this.ExtractClr();
                this.ExtractDatabase();
                this.ExtractReferenceData();
                this.GeneratePermissions();
                this.CopyCustomFiles();

                Utilities.Logger.LogInformation("Extract complete...");

                this.DropDatabase();

                Utilities.Logger.LogInformation("Dropping build database...");
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }

                return false;
            }
            finally
            {
                connection.Disconnect();
            }

            return true;
        }

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// Determines the object type and other info from the script file
        /// </summary>
        /// <param name="script">The script to be parsed</param>
        /// <param name="fileName">The name of the file</param>
        internal void DeriveObjectInformation(string script, string fileName)
        {
            ParseResult result = ScriptMatcher.ParseScript(script);

            if (!result.Matched)
            {
                string message = string.Format("Unable to derive the base object information from  {0}. Please check the defintion.", fileName);
                if (this.TreatWarningsAsErrors && this.WarnOnUnmatchedTypes)
                {
                    throw new Exception(message);
                }
                else
                {
                    if (this.WarnOnUnmatchedTypes)
                    {
                        Utilities.Logger.LogWarning(message);
                    }
                    else
                    {
                        Utilities.Logger.LogInformation(message);
                    }
                }
            }
            else
            {
                this.AddObject(script, result.SchemaName, result.TypeName, result.Name, result.Parent);
            }
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Extracts the schema from files
        /// </summary>
        private void ExtractDatabase()
        {
            this.SetupDatabaseExtract();

            Utilities.Logger.LogInformation("Loading source files...");
            foreach (string fileName in this.sqlScripts)
            {
                if ((fileName.Contains("\\Storage\\Files") || fileName.Contains(".sqlpermissions") || fileName.Contains(".rolememberships.sql")) && !fileName.Contains(".Filegroups") && !fileName.Contains(".fulltext"))
                {
                    continue;
                }
                else
                {
                    string script = File.ReadAllText(fileName).Trim();

                    Utilities.Logger.LogInformation("Processing " + fileName);

                    this.DeriveObjectInformation(script, fileName);
                }
            }

            foreach (ProcessDatabase es in this.scriptProcessors.Values)
            {
                es.TreatWarningsAsErrors = this.TreatWarningsAsErrors;
                es.CheckDependencies();
            }

            foreach (ProcessDatabase es in this.scriptProcessors.Values)
            {
                string fileName = Path.Combine(this.buildPath, es.FileName);

                using (FileStream fs = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(es.RetrieveScript());
                        bw.Close();
                    }

                    fs.Close();
                }

                if (this.SplitDropToSeperateFile)
                {
                    if (es.DropFileName.Length > 0)
                    {
                        fileName = Path.Combine(this.buildPath, es.DropFileName);
                        using (FileStream fs = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                bw.Write(es.RetrieveDropScript());
                                bw.Close();
                            }

                            fs.Close();
                        }
                    }
                }
            }

            foreach (string fileName in this.customFiles)
            {
                FileInfo f = new FileInfo(fileName);
                File.Copy(fileName, Path.Combine(this.buildPath, f.Name), true);
            }

            foreach (ProcessDatabase es in this.scriptProcessors.Values)
            {
                es.CheckIfSkipped();
            }
        }

        /// <summary>
        /// Configures the database extract
        /// </summary>
        private void SetupDatabaseExtract()
        {
            Utilities.Logger.LogInformation("Initializing database extract...");
            this.scriptProcessors = new Dictionary<SqlFileTypes, ProcessDatabase>();

            this.scriptProcessors.Add(SqlFileTypes.Filegroups, new ProcessFilegroups(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.PartitionFunctions, new ProcessPartitionFunctions(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.PartitionSchemes, new ProcessPartitionScheme(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.FullTextCatalogs, new ProcessFullTextCatalogs(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Schemas, new ProcessSchema(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.ClrAssemblies, new ProcessClrAssemblies(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));

            ProcessTables et = new ProcessTables(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.Tables, et);

            this.scriptProcessors.Add(SqlFileTypes.Routes, new ProcessRoutes(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Messages, new ProcessMessages(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Contracts, new ProcessContracts(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.BrokerQueue, new ProcessQueues(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Services, new ProcessServices(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));

            ProcessFunctions ef = new ProcessFunctions(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.Functions, ef);

            ProcessViews ev = new ProcessViews(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.Views, ev);

            ProcessClusteredConstraints ecc = new ProcessClusteredConstraints(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.ClusteredConstraints, ecc);
            ecc.Tables = et;
            ecc.Views = ev;

            ProcessConstraints ec = new ProcessConstraints(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.Constraints, ec);
            ec.Tables = et;
            ec.Views = ev;

            this.scriptProcessors.Add(SqlFileTypes.FullTextIndexes, new ProcessFullTextIndexes(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            ProcessRoles er = new ProcessRoles(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName);
            this.scriptProcessors.Add(SqlFileTypes.Roles, er);

            this.scriptProcessors.Add(SqlFileTypes.Circular, new ProcessCircular(this.database, ef, ev, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Procedures, new ProcessProcedures(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));
            this.scriptProcessors.Add(SqlFileTypes.Synonyms, new ProcessSynonyms(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName, this.GenerateDropForSynonyms));
            this.scriptProcessors.Add(SqlFileTypes.Triggers, new ProcessTriggers(this.database, this.appendDatabaseNameToScripts, this.appendDatabaseName));

            foreach (ProcessDatabase es in this.scriptProcessors.Values)
            {
                es.SplitDropToSeperateFile = this.SplitDropToSeperateFile;
            }
        }

        /// <summary>
        /// Creates the build database
        /// </summary>
        /// <param name="sqlServer">The server instance against which the build database must be deployed</param>
        /// <param name="databaseName">The name of the new database</param>
        private void CreateDatabase(Server sqlServer, string databaseName)
        {
            if (sqlServer.Databases.Contains(databaseName))
            {
                // assume the previous extract crashed without clean up -> drop the databaseName and allow clean create:
                this.database = sqlServer.Databases[databaseName];

                this.DropDatabase();
            }

            Utilities.Logger.LogInformation("Creating temporary build database {0}...", databaseName);
            this.database = new Database(sqlServer, databaseName);

            FileGroup fg = new FileGroup(this.database, "PRIMARY");
            DataFile df = new DataFile(fg, string.Concat("DB_Build", databaseName, "_Data"), Path.Combine(sqlServer.Information.MasterDBPath, databaseName + ".mdf"));
            df.Size = 20480;
            df.GrowthType = FileGrowthType.KB;
            df.Growth = 20480;
            fg.Files.Add(df);
            this.database.FileGroups.Add(fg);

            LogFile lf = new LogFile(this.database, string.Concat("DB_Build", databaseName, "_Log"), Path.Combine(sqlServer.Information.MasterDBPath, databaseName + ".ldf"));
            lf.Size = 10240;
            lf.GrowthType = FileGrowthType.KB;
            lf.Growth = 10240;
            this.database.LogFiles.Add(lf);

            this.database.Create();
        }

        /// <summary>
        /// Drops the build database
        /// </summary>
        private void DropDatabase()
        {
            if (this.database != null)
            {
                if (this.database.ActiveConnections > 1)
                {
                    throw new Exception("Unable to drop the build database. Please ensure that there are no active connections!");
                }

                this.database.Drop();
            }
        }

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="script">The ddl of the object</param>
        /// <param name="schema">The schema name</param>
        /// <param name="typeName">The type of object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="parent">The objects parent</param>
        private void AddObject(string script, string schema, string typeName, string name, string parent)
        {
            bool preExists = Utilities.ExistenceChecker.DoesDBObjectExists(schema, name);
            switch (typeName)
            {
                case "CHECK":
                    this.scriptProcessors[SqlFileTypes.Constraints].AddObject(new CheckConstraint(schema, parent, name, script));
                    break;

                case "FOREIGN KEY":
                    this.scriptProcessors[SqlFileTypes.Constraints].AddObject(new ForeignKey(schema, parent, name, script));
                    break;

                case "FUNCTION":
                    this.scriptProcessors[SqlFileTypes.Functions].AddObject(new UDFunction(schema, name, script, preExists));
                    break;

                case "INDEX":
                case "UNIQUE NONCLUSTERED INDEX":
                case "UNIQUE CLUSTERED INDEX":
                case "CLUSTERED INDEX":
                case "NONCLUSTERED INDEX":
                    this.AddClusterableConstraint(new Index(schema, parent, name, script));
                    break;

                case "PRIMARY KEY":
                    this.AddClusterableConstraint(new PrimaryKey(schema, parent, name, script));
                    break;

                case "UNIQUE":
                case "UNIQUE INDEX":
                    this.AddClusterableConstraint(new UniqueConstraint(schema, parent, name, script));
                    break;

                case "PROC":
                case "PROCEDURE":
                    this.scriptProcessors[SqlFileTypes.Procedures].AddObject(new StoredProcedure(schema, name, script, preExists));
                    break;

                case "TABLE":
                    this.scriptProcessors[SqlFileTypes.Tables].AddObject(new Table(schema, name, script));
                    break;

                case "TRIGGER":
                    this.scriptProcessors[SqlFileTypes.Triggers].AddObject(new Trigger(schema, parent, name, script));
                    break;

                case "VIEW":
                    this.scriptProcessors[SqlFileTypes.Views].AddObject(new View(schema, name, script, preExists));
                    break;

                case "DEFAULT":
                    this.scriptProcessors[SqlFileTypes.Constraints].AddObject(new DefaultConstraint(schema, parent, name, script, this.GenerateDropForDefaultConstraints));
                    break;

                case "SCHEMA":
                    this.scriptProcessors[SqlFileTypes.Schemas].AddObject(new DatabaseSchema(name, script));
                    break;

                case "ROLE":
                    this.scriptProcessors[SqlFileTypes.Roles].AddObject(new Role(name, script));
                    break;

                case "FILEGROUP":
                    this.scriptProcessors[SqlFileTypes.Filegroups].AddObject(new Filegroup(name, script));
                    break;

                case "ROUTE":
                    this.scriptProcessors[SqlFileTypes.Routes].AddObject(new Route(name, script));
                    break;

                case "MESSAGE":
                case "MESSAGE TYPE":
                    this.scriptProcessors[SqlFileTypes.Messages].AddObject(new Message(name, script));
                    break;

                case "CONTRACT":
                    this.scriptProcessors[SqlFileTypes.Contracts].AddObject(new Contract(name, script));
                    break;

                case "QUEUE":
                    this.scriptProcessors[SqlFileTypes.BrokerQueue].AddObject(new BrokerQueue(name, script));
                    break;

                case "SERVICE":
                    this.scriptProcessors[SqlFileTypes.Services].AddObject(new Service(name, script));
                    break;

                case "FULLTEXT CATALOG":
                    this.scriptProcessors[SqlFileTypes.FullTextCatalogs].AddObject(new FullTextCatalog(name, script));
                    break;

                case "FULLTEXT INDEX":
                    this.scriptProcessors[SqlFileTypes.FullTextIndexes].AddObject(new FullTextIndex(name, script));
                    break;

                case "PARTITION FUNCTION":
                    this.scriptProcessors[SqlFileTypes.PartitionFunctions].AddObject(new PartitionFunction(name, script));
                    break;

                case "PARTITION SCHEME":
                    this.scriptProcessors[SqlFileTypes.PartitionSchemes].AddObject(new PartitionScheme(name, script));
                    break;

                case "ASSEMBLY":
                    this.scriptProcessors[SqlFileTypes.ClrAssemblies].AddObject(new ClrAssembly(name, script));
                    break;

                case "SYNONYM":
                    this.scriptProcessors[SqlFileTypes.Synonyms].AddObject(new DatabaseSynonym(name, script));
                    break;

                default:
                    Utilities.Logger.LogWarning("Unhandled type {0} found for object {1}", typeName, name);
                    break;
            }
        }

        /// <summary>
        /// Adds a clusterable constraint
        /// </summary>
        /// <param name="obj">The constraint to add</param>
        private void AddClusterableConstraint(ClusteredConstraint obj)
        {
            if (obj.Clustered)
            {
                this.scriptProcessors[SqlFileTypes.ClusteredConstraints].AddObject(obj);
            }
            else
            {
                this.scriptProcessors[SqlFileTypes.Constraints].AddObject(obj);
            }
        }

        /// <summary>
        /// Loads the data extraction procedure into the static data source database
        /// </summary>
        /// <param name="extractDatabase">The database against which the scripts will be executed</param>
        /// <returns>False if unsuccessful</returns>
        private bool PrepareDatabaseExtract(Database extractDatabase)
        {
            try
            {
                Utilities.Logger.LogInformation("Creating data extraction procedure on target database ...");
                string sqlText = string.Empty;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("AdamNachman.Build.SqlExtract.Resources.CreateMergeProc.sql")))
                {
                    sqlText = sr.ReadToEnd();
                    sr.Close();
                }

                extractDatabase.ExecuteNonQuery(sqlText);
                if (!string.IsNullOrEmpty(this.extractUsername))
                {
                    sqlText = string.Format("GRANT EXECUTE ON [dbo].[spGenerateMergeData] TO [{0}]", extractDatabase.UserName);
                    extractDatabase.ExecuteNonQuery(sqlText);
                }
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Extracts reference data from the static source database
        /// </summary>
        private void ExtractReferenceData()
        {
            if (this.ExtractData)
            {
                Utilities.Logger.LogInformation("Extracting merge data from source database");

                ServerConnection connection = null;
                Server sqlServer = null;
                Database sourceDatabase = null;

                try
                {
                    Utilities.Logger.LogInformation(string.Format("Attempting to connect to {0}...", this.extractFromServer));

                    connection = new ServerConnection(this.extractFromServer);

                    if (string.IsNullOrEmpty(this.extractUsername))
                    {
                        connection.LoginSecure = true;
                    }
                    else
                    {
                        connection.LoginSecure = false;
                        connection.Login = this.extractUsername;
                        connection.Password = this.extractPassword;
                    }
                    connection.Connect();

                    sqlServer = new Server(connection);
                    sourceDatabase = sqlServer.Databases[this.extractFromDatabase];

                    this.PrepareDatabaseExtract(sourceDatabase);
                    this.ExecuteExtractCommands(sourceDatabase);
                }
                catch (Exception ex)
                {
                    if (Utilities.Logger.LogErrorFromException(ex))
                    {
                        throw;
                    }
                }
                finally
                {
                    this.CleanupTarget(sourceDatabase);
                    connection.Disconnect();
                }
            }
        }

        /// <summary>
        /// Cleas up the extraction scripts from the source database
        /// </summary>
        /// <param name="extractDatabase">The database against which the commans will be executed</param>
        private void CleanupTarget(Database extractDatabase)
        {
            try
            {
                Utilities.Logger.LogInformation("Cleaning up data extraction scripts ...");
                string sqlText = string.Empty;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("AdamNachman.Build.SqlExtract.Resources.CleanupMergeProc.sql")))
                {
                    sqlText = sr.ReadToEnd();
                    sr.Close();
                }

                extractDatabase.ExecuteNonQuery(sqlText);
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes the extract commands againts the database containing static data
        /// </summary>
        /// <param name="extractDatabase">The database against which the commans should be executed</param>
        private void ExecuteExtractCommands(Database extractDatabase)
        {
            string commandLine = string.Empty;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(this.extractCommands))
            {
                while (sr.Peek() >= 0)
                {
                    commandLine = sr.ReadLine();
                    if (!string.IsNullOrEmpty(commandLine.Trim()))
                    {
                        DataSet ds = extractDatabase.ExecuteWithResults(commandLine);
                        if (ds != null)
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    sb.AppendLine(dr[0].ToString());
                                }
                            }
                        }
                    }
                }

                sr.Close();
            }

            using (StreamWriter sw = new StreamWriter(this.extractTargetFile))
            {
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Generates permissions if the flag is set to true, does nothing when false
        /// </summary>
        private void GeneratePermissions()
        {
            if (this.ScriptPermissions)
            {
                try
                {
                    this.PreparePermissionProcedures();
                    this.ExecutePermissionsProcedures();
                }
                catch (Exception ex)
                {
                    if (Utilities.Logger.LogErrorFromException(ex))
                    {
                        throw;
                    }
                }
                finally
                {
                    this.CleanupPermissionsProcedures();
                }
            }
        }

        /// <summary>
        /// Loads the permissions procedure into the build database
        /// </summary>
        private void PreparePermissionProcedures()
        {
            try
            {
                Utilities.Logger.LogInformation("Creating permission extraction procedure on target database ...");
                string sqlText = string.Empty;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("AdamNachman.Build.SqlExtract.Resources.CreatePermissionsProc.sql")))
                {
                    sqlText = sr.ReadToEnd();
                    sr.Close();
                }

                this.database.ExecuteNonQuery(sqlText);
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the permission pattern from the permissions Template
        /// </summary>
        /// <returns>The permissions pattern to apply when generating permissions</returns>
        private PermissionPattern GetPermissions()
        {
            PermissionPattern p = null;
            if (File.Exists(this.permissionsTemplate))
            {
                using (StreamReader sr = new StreamReader(this.permissionsTemplate))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(PermissionPattern));
                    p = (PermissionPattern)xs.Deserialize(sr.BaseStream);
                    sr.Close();
                }
            }

            return p;
        }

        /// <summary>
        /// Gets the assemblies from the config file.
        /// </summary>
        /// <returns>null if there are no assemblies</returns>
        private AssembliesToExtract GetAssemblies()
        {
            AssembliesToExtract asm = null;
            if (File.Exists(this.ExtractAssembliesConfig))
            {
                using (StreamReader sr = new StreamReader(this.ExtractAssembliesConfig))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(AssembliesToExtract));
                    asm = (AssembliesToExtract)xs.Deserialize(sr.BaseStream);
                    sr.Close();
                }
            }

            return asm;
        }

        /// <summary>
        /// Execures the permissions procedure for each template entry
        /// </summary>
        private void ExecutePermissionsProcedures()
        {
            string commandLine = string.Empty;
            StringBuilder sb = new StringBuilder();

            Utilities.Logger.LogInformation("Executing permissions procedures ...");
            using (StreamReader sr = new StreamReader(this.extractCommands))
            {
                PermissionPattern permissions = this.GetPermissions();
                foreach (PermissionMap map in permissions.PermissionMaps)
                {
                    string objectTypes = string.Concat("'", string.Join("'',''", map.ObjectTypes.Split(new char[] { ',' })), "'");

                    if (string.IsNullOrEmpty(map.SchemaName))
                    {
                        commandLine = string.Format("exec spGeneratePermissions @Principal='{0}',@Permissions='{1}',@ObjectTypes=''{2}''", map.PrincipalName, map.GrantType, objectTypes);
                    }
                    else
                    {
                        commandLine = string.Format("exec spGeneratePermissions @Principal='{0}',@Permissions='{1}',@ObjectTypes=''{2}'',@SchemaName='{3}'", map.PrincipalName, map.GrantType, objectTypes, map.SchemaName);
                    }

                    if (!string.IsNullOrEmpty(commandLine.Trim()))
                    {
                        DataSet ds = this.database.ExecuteWithResults(commandLine);
                        if (ds != null)
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    sb.AppendLine(dr[0].ToString());
                                }
                            }
                        }
                    }
                }

                sr.Close();
            }

            using (StreamWriter sw = new StreamWriter(this.permissionsTargetFile))
            {
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Removes the autogenerated permissions procedure from the build database
        /// </summary>
        private void CleanupPermissionsProcedures()
        {
            try
            {
                Utilities.Logger.LogInformation("Cleaning up permission extraction procedure on target database ...");
                string sqlText = string.Empty;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("AdamNachman.Build.SqlExtract.Resources.CleanupPermissionsProc.sql")))
                {
                    sqlText = sr.ReadToEnd();
                    sr.Close();
                }

                this.database.ExecuteNonQuery(sqlText);
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }

                return;
            }
        }

        /// <summary>
        /// Initializes the ExistenceChecker class
        /// </summary>
        /// <returns>True if the value is set to true</returns>
        private bool InitializeExistenceChecker()
        {
            if (this.AlterIfExists)
            {
                Utilities.Logger.LogInformation("Initializing existence checker");
                Type t = null;
                t = Type.GetType(this.existenceCheckerProvider);

                IExistenceChecker checker = (IExistenceChecker)Activator.CreateInstance(t);
                checker.Logger = Utilities.Logger;
                checker.ConfigurationString = this.ExistenceCheckerConfiguration;
                checker.Initialize();
                Utilities.ExistenceChecker = checker;

                return true;
            }
            else
            {
                Utilities.ExistenceChecker = new ExistenceCheckerDefault();

                return true;
            }
        }

        /// <summary>
        /// Copies the custom files.
        /// </summary>
        private void CopyCustomFiles()
        {
            foreach (string fileName in this.customFiles)
            {
                FileInfo f = new FileInfo(fileName);
                File.Copy(fileName, Path.Combine(this.buildPath, f.Name), true);
            }
        }

        /// <summary>
        /// Extracts the Clr assemblies to file.
        /// </summary>
        private void ExtractClr()
        {
            if (this.ExtractAssemblies && !string.IsNullOrEmpty(this.ExtractAssembliesConfig))
            {
                Utilities.Logger.LogInformation("Extracting assembly bits ...");

                AssembliesToExtract asms = this.GetAssemblies();
                foreach (AssemblyToExtract asm in asms.ConfiguredAssemblies)
                {
                    Utilities.Logger.LogInformation(string.Format("Extracting assembly {0} from {1}  ...", asm.Name, asm.FilePath));
                    Utilities.ExtractClrToFile(asm);
                }
            }
        }

        #endregion Private Methods
    }
}
