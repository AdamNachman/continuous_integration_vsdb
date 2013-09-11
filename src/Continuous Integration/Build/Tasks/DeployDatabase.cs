// <copyright file="DeployDatabase.cs" company="Adam Nachman">
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
namespace SqlDeployment.Build
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;
    using Microsoft.Build.Framework;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// A MSBuild Task that handles the deployment of a database
    /// </summary>
    public class DeployDatabase : Microsoft.Build.Utilities.Task
    {
        #region Private Variables

        /// <summary>
        /// The SQL instance against which the task will be executed
        /// </summary>
        private string sqlInstanceName = string.Empty;

        /// <summary>
        /// The sql user that will be used to connect to the database
        /// </summary>
        private string sqlUsername = string.Empty;

        /// <summary>
        /// The sql password that will be used to connect to the database
        /// </summary>
        private string sqlPassword = string.Empty;
        
        /// <summary>
        /// The SQL database against which the task will be executed
        /// </summary>
        private string targetDatabase = string.Empty;

        /// <summary>
        /// The path of the config file on disk
        /// </summary>
        private string configPath = string.Empty;

        /// <summary>
        /// The path of the compound output file
        /// </summary>
        private string compoundFile = string.Empty;

        /// <summary>
        /// Indicates whether or not a compound file should be created from the sequenced execution files
        /// </summary>
        private bool outputCompoundFile = false;

        /// <summary>
        /// Indicates whether or not a using statement should be created at the beginning of the output file
        /// </summary>
        private bool prependUsingDatabaseName = false;
        
        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DeployDatabase class
        /// </summary>
        public DeployDatabase()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Sets the name of the SQL Server instance to which the code will be deployed
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem SqlInstance
        {
            set
            {
                this.sqlInstanceName = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the sql user that will be used to connect to the database
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem SqlUsername
        {
            set
            {
                this.sqlUsername = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the password that will be used to connect to the database
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem SqlPassword
        {
            set
            {
                this.sqlPassword = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the name of the database to which the code will be deployed
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem DatabaseName
        {
            set
            {
                this.targetDatabase = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the path of the config file that controls the dataase deployment
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem ConfigPath
        {
            set
            {
                this.configPath = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the path of the output file in the event that a single coumpound file is required
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem OutputCompoundFilePath
        {
            set
            {
                this.compoundFile = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the value that indicates whether or not a single output file is required
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem OutputCompoundFile
        {
            set
            {
                this.outputCompoundFile = (value.ItemSpec == "0") ? false : true;
            }
        }

        /// <summary>
        /// Sets the value that indicates whether or Indicates whether or not a using statement should be created at the beginning of the output file
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem PrependUsingDatabaseName
        {
            set
            {
                this.prependUsingDatabaseName = (value.ItemSpec == "0") ? false : true;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Executes the scripts
        /// </summary>
        /// <returns>False if unsuccessful</returns>
        public override bool Execute()
        {
            ServerConnection connection = null;
            Server sqlServer = null;
            Database database = null;
            Assembly thisDLL = Assembly.GetExecutingAssembly();
            AssemblyName an = thisDLL.GetName();
            Utilities.Logger.Init(this);
            Utilities.Logger.LogInformation("Database deployment task version : {0}...", an.Version);
            
            try
            {
                Utilities.Logger.LogInformation(string.Format("Attempting to connect to {0}...", this.sqlInstanceName));
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

                database = this.CreateDatabase(sqlServer, this.targetDatabase);

                ScriptExecutorConfiguration config = this.GetDeploymentConfig(this.configPath);

                ScriptExecutor executor = new ScriptExecutor(sqlServer, database, config);
                executor.OutputCompoundFile = this.outputCompoundFile;
                executor.OutputCompoundFilePath = this.compoundFile;
                executor.PrependUsingDatabaseName = this.prependUsingDatabaseName;

                executor.Progress += new ScriptExecutor.ScriptExecutorEventHandler(this.OnProgress);

                executor.Execute();

                Utilities.Logger.LogInformation(string.Format("Deployment/upgrade of {0} on {1} complete ...", this.sqlInstanceName, this.targetDatabase));
            }
            catch (FailedOperationException ex)
            {
                SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                if (Utilities.Logger.LogErrorFromException(sqlEx))
                {
                    throw;
                }

                return false;
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

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Retrieves the deployment config from file
        /// </summary>
        /// <param name="path">The path of the file</param>
        /// <returns>null if the file does not exist</returns>
        private ScriptExecutorConfiguration GetDeploymentConfig(string path)
        {
            ScriptExecutorConfiguration retval = null;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ScriptExecutorConfiguration));
                    retval = (ScriptExecutorConfiguration)xs.Deserialize(sr.BaseStream);
                    sr.Close();
                }
            }

            return retval;
        }

        /// <summary>
        /// Creates a database
        /// </summary>
        /// <param name="sqlServer">The server instance where the database should be created</param>
        /// <param name="targetDatabase">The name of the database to create</param>
        /// <returns>The Database if successful</returns>
        private Database CreateDatabase(Server sqlServer, string targetDatabase)
        {
            Database database;

            if (sqlServer.Databases.Contains(targetDatabase))
            {
                database = sqlServer.Databases[targetDatabase];
            }
            else
            {
                Utilities.Logger.LogInformation("Creating new database {0}...", targetDatabase);
                database = new Database(sqlServer, targetDatabase);
                database.CompatibilityLevel = CompatibilityLevel.Version90;

                FileGroup fg = new FileGroup(database, "PRIMARY");
                DataFile df = new DataFile(fg, string.Concat(targetDatabase, "_Data"), Path.Combine(sqlServer.Information.MasterDBPath, targetDatabase + ".mdf"));
                df.Size = 20480;
                df.GrowthType = FileGrowthType.KB;
                df.Growth = 20480;
                fg.Files.Add(df);
                database.FileGroups.Add(fg);

                LogFile lf = new LogFile(database, string.Concat(targetDatabase, "_Log"), Path.Combine(sqlServer.Information.MasterDBPath, targetDatabase + ".ldf"));
                lf.Size = 10240;
                lf.GrowthType = FileGrowthType.KB;
                lf.Growth = 10240;
                database.LogFiles.Add(lf);

                database.Create();

                Utilities.Logger.LogInformation("Database {0} creation complete ...", targetDatabase);
            }

            return database;
        }

        /// <summary>
        /// Handles the progress messages from the underlying executor class
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The ScriptExecutorEventArgs arguments</param>
        private void OnProgress(object sender, ScriptExecutorEventArgs[] e)
        {
            if (e != null && e.Length > 0)
            {
                int length = e.Length;
                for (int i = 0; i < length; i++)
                {
                    switch (e[i].EventCode)
                    {
                        case ScriptExecutorEventArgs.EventMessageCode.ObjectCount:
                        case ScriptExecutorEventArgs.EventMessageCode.Progress:
                            Utilities.Logger.LogInformation(e[i].EventMessage.Replace("|", string.Empty));
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ExecutionComplete:
                            Utilities.Logger.LogInformation("Deployment Complete");
                            break;
                        case ScriptExecutorEventArgs.EventMessageCode.ScriptExecution:
                            Utilities.Logger.LogInformation(e[i].EventMessage.Replace(".sql", string.Empty));
                            break;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}