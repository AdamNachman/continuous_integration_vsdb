// <copyright file="ScriptExecutor.cs" company="Adam Nachman">
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
namespace SqlDeployment
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    public class ScriptExecutor
    {
        #region Constants

        private const string DatabaseReplacementToken = "$(DatabaseName)";

        #endregion Constants

        #region Private Variables

        private Microsoft.SqlServer.Management.Smo.Server server = null;
        private Microsoft.SqlServer.Management.Smo.Database database = null;
        private ScriptExecutorConfiguration config = null;
        private volatile object syncRoot = new object();
        private bool stopping = false;
        private string compoundFile;
        private bool outputCompoundFile = false;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScriptExecutor class
        /// </summary>
        /// <param name="server">The server to which the executor must connect</param>
        /// <param name="database">The database against which the scripts must be executed</param>
        /// <param name="config">The configuratoin</param>
        public ScriptExecutor(Server server, Database database, ScriptExecutorConfiguration config)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server", "Server cannot be null");
            }

            if (database == null)
            {
                throw new ArgumentNullException("database", "Database cannot be null");
            }

            if (config == null)
            {
                throw new ArgumentNullException("config", "Config cannot be null");
            }

            if (config.ExecutionSequence.Count < 1)
            {
                throw new ArgumentOutOfRangeException("config", "Connfig.ExecutionSequence cannot be empty. At least one script needs to be defined");
            }

            this.server = server;
            this.database = database;
            this.config = config;
        }

        #endregion Constructors

        #region Public Delegates

        public delegate void ScriptExecutorEventHandler(object sender, ScriptExecutorEventArgs[] e);

        #endregion Public Delegates

        #region Public Events

        public event ScriptExecutorEventHandler Progress;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Sets the path of the output file in the event that a single coumpound file is required
        /// </summary>
        public string OutputCompoundFilePath
        {
            set
            {
                this.compoundFile = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not a single output file is required
        /// </summary>
        public bool OutputCompoundFile
        {
            set
            {
                this.outputCompoundFile = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not a using statement should be created at the beginning of the output file
        /// </summary>
        /// <value><c>true</c> if a using statement should be created at the beginning of the output file; otherwise, <c>false</c>.
        /// </value>
        public bool PrependUsingDatabaseName { get; set; }

        #endregion Public Properies

        #region Public Methods

        /// <summary>
        /// Executes the scripts
        /// </summary>
        /// <returns>false if usuccessful</returns>
        public bool Execute()
        {
            if (this.outputCompoundFile)
            {
                FileBuilder.SetPath(this.compoundFile);
                FileBuilder.Initialize();
            }

            this.ValidateConfiguration();
            this.server.ConnectionContext.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(this.ConnectionContext_InfoMessage);

            lock (this.syncRoot)
            {
                this.stopping = false;
                var sortedList = from item in this.config.ExecutionSequence orderby item.Key select item;

                if (this.stopping)
                {
                    this.RaiseStoppedEvent();
                    return false;
                }

                if (this.PrependUsingDatabaseName && this.outputCompoundFile)
                {
                    FileBuilder.AddText(string.Format("use [{0}]", this.database.Name));
                    FileBuilder.AddText("GO");
                }

                foreach (KeyValuePair<int, string> kvp in sortedList)
                {
                    if (this.stopping)
                    {
                        this.RaiseStoppedEvent();
                        return false;
                    }

                    this.OnProgress(new ScriptExecutorEventArgs[] { new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.ScriptExecution, kvp.Value) });
                    this.ExecuteScriptFile(kvp.Value);
                }

                this.OnProgress(new ScriptExecutorEventArgs[] { new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.ExecutionComplete, string.Empty) });

                if (this.outputCompoundFile)
                {
                    FileBuilder.Flush();
                }
                return true;
            }
        }

        public void Stop()
        {
            this.stopping = true;
            this.OnProgress(new ScriptExecutorEventArgs[] { new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.ExecutionStoppingByRequest, "Stopping Execution") });
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void OnProgress(ScriptExecutorEventArgs[] e)
        {
            if (this.Progress != null)
            {
                this.Progress(this, e);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void RaiseStoppedEvent()
        {
            this.OnProgress(new ScriptExecutorEventArgs[] { new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.ExecutionStoppedByRequest, "Script execution stopped") });
        }

        /// <summary>
        /// Executes the script file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void ExecuteScriptFile(string filename)
        {
            string sqlCommand = string.Empty;
            using (StreamReader streamReader = new StreamReader(filename))
            {
                sqlCommand = streamReader.ReadToEnd();
                streamReader.Close();
            }

            this.database.ExecuteNonQuery(sqlCommand.Replace(DatabaseReplacementToken, this.database.Name), ExecutionTypes.Default);
            if (this.outputCompoundFile)
            {
                FileBuilder.AddText(sqlCommand.Replace(DatabaseReplacementToken, this.database.Name));
            }
        }

        #endregion Private Methods

        #region Private Event Handlers

        private void ValidateConfiguration()
        {
            if (!string.IsNullOrEmpty(this.config.TargetServer) && this.server.Name.ToUpperInvariant() != this.config.TargetServer.ToUpperInvariant())
            {
                throw new Exception(string.Format("You may only deploy this update against the {0} server, and not {1}", this.config.TargetServer, this.server.Name));
            }

            if (!string.IsNullOrEmpty(this.config.TargetDatabase) && this.database.Name.ToUpperInvariant() != this.config.TargetDatabase.ToUpperInvariant())
            {
                throw new Exception(string.Format("You may only deploy this update against the {0} database, and not {1}", this.config.TargetDatabase, this.database.Name));
            }
        }

        private void ConnectionContext_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            if (e.Errors != null && e.Errors.Count > 0)
            {
                int count = e.Errors.Count;
                ScriptExecutorEventArgs[] args = new ScriptExecutorEventArgs[e.Errors.Count];
                for (int i = 0; i < count; i++)
                {
                    switch (e.Errors[i].Number)
                    {
                        case 100000:
                            args[i] = new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.ObjectCount, e.Errors[i].Message);
                            break;
                        case 100010:
                            args[i] = new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.Progress, e.Errors[i].Message);
                            break;
                        default:
                            args[i] = new ScriptExecutorEventArgs(ScriptExecutorEventArgs.EventMessageCode.SqlInfoMessage, e.Errors[i].Message);
                            break;
                    }
                }

                this.OnProgress(args);
            }
            else
            {
                Debug.Print("Huh " + e.Message);
            }
        }

        #endregion Private Event Handlers
    }
}
