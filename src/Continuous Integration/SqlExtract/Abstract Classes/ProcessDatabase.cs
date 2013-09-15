// <copyright file="ProcessDatabase.cs" company="Adam Nachman">
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
    using System.Data.SqlClient;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The abstract ProcessDatabase class
    /// </summary>
    internal abstract class ProcessDatabase
    {
        #region Private Variables

        /// <summary>
        /// The name to append
        /// </summary>
        private string appendDatabaseName = string.Empty;

        /// <summary>
        /// Indicates whether or not the database name will be appended to the script output
        /// </summary>
        private bool appendDatabaseNameToScripts = false;

        /// <summary>
        /// The database against which the processor will execute
        /// </summary>
        private Database database;

        /// <summary>
        /// The collection of database objects associated with this processor
        /// </summary>
        private Dictionary<string, IDBObject> databaseObjects;

        /// <summary>
        /// The string builder used to concatenate the scripts
        /// </summary>
        private StringBuilder scriptBuilder;

        /// <summary>
        /// The counter used to indicate the number of objects in the script
        /// </summary>
        private int raiseCount;

        /// <summary>
        /// Indicates whether or not the drop scripts should be split to a separate file
        /// </summary>
        private bool dropToSeperateFile;

        /// <summary>
        /// The string builder used to concatenate the drop scripts
        /// </summary>
        private StringBuilder dropScript;

        /// <summary>
        /// The count of dropped objects
        /// </summary>
        private int dropCount;

        /// <summary>
        /// indicates that warnings should be treated as errors
        /// </summary>
        private bool treatWarningsAsErrors = false;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessDatabase class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessDatabase(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessDatabase class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessDatabase(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
        {
            this.appendDatabaseNameToScripts = appendDatabaseNameToScripts;
            this.appendDatabaseName = appendDatabaseName;
            this.database = targetDatabase;
            this.databaseObjects = new Dictionary<string, IDBObject>();
            this.scriptBuilder = new StringBuilder();
            this.raiseCount = 0;
            this.dropToSeperateFile = false;
            this.dropScript = new StringBuilder();
            this.dropCount = 0;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [treat warnings as errors].
        /// </summary>
        /// <value> <c>true</c> if [treat warnings as errors]; otherwise, <c>false</c>.</value>
        public bool TreatWarningsAsErrors
        {
            get
            {
                return this.treatWarningsAsErrors;
            }
            set
            {
                this.treatWarningsAsErrors = value;
            }
        }

        #endregion Public Properties

        #region Internal Properties

        #region Abstract Properties

        /// <summary>
        /// Gets the name of the output file
        /// </summary>
        internal abstract string FileName
        {
            get;
        }

        /// <summary>
        /// Gets the name of the drop script file 
        /// </summary>
        internal virtual string DropFileName
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion Abstract Properties

        /// <summary>
        /// Gets a collection of pending (unprocessed) database objects associated with this processor
        /// </summary>
        internal IEnumerable<IDBObject> PendingDBObjects
        {
            get
            {
                foreach (IDBObject so in this.databaseObjects.Values)
                {
                    if (!so.Complete)
                    {
                        yield return so;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a collection of database objects associated with this processor
        /// </summary>
        internal IEnumerable<IDBObject> DatabaseObjects
        {
            get
            {
                foreach (IDBObject databaseObject in this.databaseObjects.Values)
                {
                    yield return databaseObject;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the drop scripts should be split to a separate file
        /// </summary>
        internal bool SplitDropToSeperateFile
        {
            get
            {
                return this.dropToSeperateFile;
            }

            set
            {
                this.dropToSeperateFile = value;
            }
        }

        #endregion Internal Properties

        #region Protected Properties

        /// <summary>
        /// Gets the number of the total error message to raise
        /// </summary>
        protected virtual string RaiserrorTotalNumber
        {
            get
            {
                return "100000";
            }
        }

        /// <summary>
        /// Gets the number of the progress error message to raise
        /// </summary>
        protected virtual string RaiserrorProgressNumber
        {
            get
            {
                return "100010";
            }
        }

        /// <summary>
        /// Gets the interval at which to raise the error to show progress
        /// </summary>
        protected virtual int RaiserrorInterval
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets or sets the name to append
        /// </summary>
        protected virtual string AppendDatabaseName
        {
            get
            {
                return this.appendDatabaseName;
            }

            set
            {
                this.appendDatabaseName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the database name will be appended to the script output
        /// </summary>
        protected virtual bool AppendDatabaseNameToScripts
        {
            get
            {
                return this.appendDatabaseNameToScripts;
            }

            set
            {
                this.appendDatabaseNameToScripts = value;
            }
        }

        #endregion Protected Properties

        #region Internal Methods

        /// <summary>
        /// Removes pending object from the pending collection
        /// </summary>
        internal void RemovePendingDBObject()
        {
            string[] keys = new string[this.databaseObjects.Count];
            this.databaseObjects.Keys.CopyTo(keys, 0);
            foreach (string s in keys)
            {
                IDBObject so = this.databaseObjects[s];
                if (!so.Complete)
                {
                    this.databaseObjects.Remove(s);
                }
            }
        }

        /// <summary>
        /// Retrieves the complete script for the processor
        /// </summary>
        /// <returns>A byte array</returns>
        internal byte[] RetrieveScript()
        {
            string x = string.Empty;
            if (this.scriptBuilder.Length > 0)
            {
                StringBuilder s = new StringBuilder();
                s.AppendLine("SET NOCOUNT ON");
                s.AppendLine();
                s.AppendLine(string.Format("EXEC sp_addmessage @msgnum = {0}, @severity =  1, @msgtext = 'Number of objects == |{1}|', @lang = 'us_english', @replace = 'REPLACE'", this.RaiserrorTotalNumber, this.raiseCount));
                s.AppendLine(string.Format("EXEC sp_addmessage @msgnum = {0}, @severity =  1, @msgtext = 'Processed object |%d| of {1}', @lang = 'us_english', @replace = 'REPLACE'", this.RaiserrorProgressNumber, this.raiseCount));
                s.AppendLine("GO");
                s.AppendLine(string.Format("RAISERROR ({0}, 1, 1)", this.RaiserrorTotalNumber));
                s.AppendLine("GO");
                s.Append(this.scriptBuilder.ToString());

                x = s.ToString();
            }

            return UTF8Encoding.UTF8.GetBytes(x);
        }

        /// <summary>
        /// Returns the drop script assocated with the process class
        /// </summary>
        /// <returns>A byte array</returns>
        internal byte[] RetrieveDropScript()
        {
            string x = string.Empty;
            if (this.scriptBuilder.Length > 0)
            {
                StringBuilder s = new StringBuilder();
                s.AppendLine("SET NOCOUNT ON");
                s.AppendLine();
                s.AppendLine(string.Format("EXEC sp_addmessage @msgnum = {0}, @severity =  1, @msgtext = 'Number of objects == |{1}|', @lang = 'us_english', @replace = 'REPLACE'", this.RaiserrorTotalNumber, this.dropCount));
                s.AppendLine(string.Format("EXEC sp_addmessage @msgnum = {0}, @severity =  1, @msgtext = 'Processed object |%d| of {1}', @lang = 'us_english', @replace = 'REPLACE'", this.RaiserrorProgressNumber, this.dropCount));
                s.AppendLine("GO");
                s.AppendLine(string.Format("RAISERROR ({0}, 1, 1)", this.RaiserrorTotalNumber));
                s.AppendLine("GO");
                s.Append(this.dropScript.ToString());

                x = s.ToString();
            }

            return UTF8Encoding.UTF8.GetBytes(x);
        }

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal virtual void AddObject(IDBObject newObject)
        {
            if (this.databaseObjects.ContainsKey(string.Concat(newObject.Schema, newObject.Name)))
            {
                throw new Exception(string.Format("Duplicate object found: {0}", newObject.Name));
            }

            this.databaseObjects.Add(string.Concat(newObject.Schema, newObject.Name), newObject);
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal virtual void CheckDependencies()
        {
            int lastCount = this.databaseObjects.Count;
            int count = 0;
            bool tryAgain = true;

            while (tryAgain)
            {
                count = 0;
                tryAgain = false;
                foreach (IDBObject pendingDBObject in this.PendingDBObjects)
                {
                    if (!this.ExecuteScript(string.Concat(pendingDBObject.Schema, pendingDBObject.Name)))
                    {
                        tryAgain = true;
                        count++;
                    }
                }

                if (lastCount == count)
                {
                    break;
                }
                else
                {
                    lastCount = count;
                }
            }
        }

        /// <summary>
        /// Checks to see if an object should be skipped
        /// </summary>
        internal void CheckIfSkipped()
        {
            foreach (IDBObject so in this.PendingDBObjects)
            {
                if (this.treatWarningsAsErrors)
                {
                    throw new Exception(string.Format("=======> Skipping object creation for {0} as it depends on {1}", so.Name, so.DependsOn));
                }
                else
                {
                    Utilities.Logger.LogWarning("=======> Skipping object creation for {0} as it depends on {1}", so.Name, so.DependsOn);
                }
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// Adds a script to the script builder
        /// </summary>
        /// <param name="sqlScript">The script to add</param>
        protected void AddSQLScript(string sqlScript)
        {
            this.scriptBuilder.AppendLine(sqlScript);
        }

        /// <summary>
        /// Executes the script
        /// </summary>
        /// <param name="objectName">The name of the object to execute</param>
        /// <returns>false if unsuccessful</returns>
        protected bool ExecuteScript(string objectName)
        {
            IDBObject databaseObject;
            if (this.databaseObjects.TryGetValue(objectName, out databaseObject))
            {
                if (databaseObject.Complete)
                {
                    return true;
                }

                databaseObject.DependsOn = string.Empty;

                try
                {
                    this.database.ExecuteNonQuery(databaseObject.Ddl.Replace(SystemConstants.DatabaseReplacementToken, this.database.Name));
                }
                catch (FailedOperationException ex)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;

                    Regex re;
                    switch (sqlEx.Number)
                    {
                        case 208:
                            re = new Regex("Invalid object name '(\\w+)'", RegexOptions.IgnoreCase);
                            break;

                        case 4121:
                            re = new Regex("User-defined function or aggregate \"dbo.(\\w+)\"", RegexOptions.IgnoreCase);
                            break;

                        case 6528:
                            re = new Regex("Assembly '([a-zA-Z0-9_.]+)' was not found", RegexOptions.IgnoreCase);
                            break;
                        case 7601:
                            // TODO: Parse the object name and populate the dependancy
                            re = new Regex("Cannot use a CONTAINS or FREETEXT predicate on table or indexed view '\\w.+' because it is not full-text indexed.", RegexOptions.IgnoreCase);
                            break;
                        default:
                            re = new Regex("Unknown");
                            break;
                    }

                    Match m = re.Match(sqlEx.Message);
                    if (m.Success)
                    {
                        databaseObject.DependsOn = m.Groups[1].Value;
                    }
                    else
                    {
                        databaseObject.DependsOn = sqlEx.Message;
                    }

                    return false;
                }

                this.scriptBuilder.AppendLine();
                this.scriptBuilder.AppendLine(databaseObject.CompleteDdl);
                this.RaiseProgress();
                databaseObject.MarkComplete();
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Raises a progress message in the script
        /// </summary>
        protected void RaiseProgress()
        {
            this.RaiseProgress(this.scriptBuilder, ref this.raiseCount);
        }

        /// <summary>
        /// Returns the prefix of the file
        /// </summary>
        /// <returns>An empty string if the flag to append the name is false</returns>
        protected string GetFilePrefix()
        {
            return this.appendDatabaseNameToScripts ? this.appendDatabaseName : string.Empty;
        }

        /// <summary>
        /// Gets an object from the internal collection
        /// </summary>
        /// <param name="objectName">The name of the object to retrieve</param>
        /// <returns>null of the object cannot be retrieved</returns>
        protected IDBObject GetDatabaseObject(string objectName)
        {
            IDBObject x;
            this.databaseObjects.TryGetValue(objectName, out x);
            return x;
        }

        /// <summary>
        /// Adds a drop script to the processor
        /// </summary>
        /// <param name="script">The script to add</param>
        protected void AddDropScript(string script)
        {
            if (this.dropToSeperateFile)
            {
                this.dropScript.AppendLine(script);
                this.RaiseProgress(this.dropScript, ref this.dropCount);
            }
            else
            {
                this.AddSQLScript(script);
                this.RaiseProgress(this.scriptBuilder, ref this.raiseCount);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Appends the progress message to the script
        /// </summary>
        /// <param name="script">The script</param>
        /// <param name="counter">The current counter</param>
        private void RaiseProgress(StringBuilder script, ref int counter)
        {
            counter++;
            if (counter % this.RaiserrorInterval == 0)
            {
                script.AppendFormat("RAISERROR ({0}, 1, 1, {1})\nGO\n", this.RaiserrorProgressNumber, counter);
            }
            else
            {
                script.AppendFormat("--RAISERROR ({0}, 1, 1, {1})\nGO\n", this.RaiserrorProgressNumber, counter);
            }
        }

        #endregion Private Methods
    }
}
