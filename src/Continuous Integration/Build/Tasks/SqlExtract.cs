// <copyright file="SqlExtract.cs" company="Adam Nachman">
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
    using System.Collections.Generic;
    using AdamNachman.Build.SqlExtract;
    using Microsoft.Build.Framework;

    /// <summary>
    /// A class used to wrap the sql extractor in an MSBuild task
    /// </summary>
    public class SQLExtract : Microsoft.Build.Utilities.Task
    {
        #region Private Variables

        private string sqlInstanceName;
        private string projectGUID;
        private string buildPath;
        private List<string> sqlScripts;
        private List<string> customFiles;
        private bool dropToSeperateFile = false;
        private bool extractData = false;
        private string extractFromServer = string.Empty;
        private string extractFromDatabase = string.Empty;
        private string extractTargetFile = string.Empty;
        private string extractCommands = string.Empty;
        private string appendDatabaseName = string.Empty;
        private bool appendDatabaseNameToScripts = false;
        private bool scriptPermissions = false;
        private string permissionsTemplate = string.Empty;
        private string permissionsTargetFile = string.Empty;
        private bool alterIfExists = false;
        private string existenceCheckerProvider = string.Empty;
        private string existenceCheckerConfiguration = string.Empty;
        private string sqlUsername = string.Empty;
        private string sqlPassword = string.Empty;
        private string extractUsername = string.Empty;
        private string extractPassword = string.Empty;
        private bool treatWarningsAsErrors = false;
        private bool extractAssemblies = false;
        private string extractAssembliesConfigFile = string.Empty;
        private bool generateDropsForSynonyms = false;
        private bool generateDropsForDefaultConstraints = false;

        /// <summary>
        /// If <c>true</c> a warning will be raised on unmatched types; otherwise a message will simply be logged
        /// </summary>
        private bool warnOnUnmatchedTypes = true;

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Sets the SQL Server instance where build can check dependencies.
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
        /// Sets the project GUID for the test deploy databaseName
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem ProjectGUID
        {
            set
            {
                string temp = value.ItemSpec;
                this.projectGUID = temp.Substring(1, temp.Length - 2);
            }
        }

        /// <summary>
        /// Sets the path of the output files.
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem BuildPath
        {
            set
            {
                this.buildPath = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the schema scripts to process
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem[] SchemaScripts
        {
            set
            {
                this.sqlScripts = Utilities.TaskItemToList(value);
            }
        }

        /// <summary>
        /// Sets the static files to include in the build output
        /// </summary>
        [Required]
        public Microsoft.Build.Framework.ITaskItem[] StaticFiles
        {
            set
            {
                this.customFiles = Utilities.TaskItemToList(value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not the drop files should be separate from the parent file
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem SplitDropToSeperateFile
        {
            set
            {
                this.dropToSeperateFile = (value.ItemSpec == "0") ? false : true;
            }
        }

        /// <summary>
        /// Sets a name to prepend to output scripts
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem AppendDatabaseNameToScripts
        {
            set
            {
                this.appendDatabaseName = value.ItemSpec.Trim();
                this.appendDatabaseNameToScripts = !string.IsNullOrEmpty(this.appendDatabaseName);
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not static data should be extracted
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractData
        {
            set
            {
                this.extractData = (value.ItemSpec == "0") ? false : true;
            }
        }

        /// <summary>
        /// Sets the server from which static data should be extracted
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractFromServer
        {
            set
            {
                this.extractFromServer = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the database from which static data should be extracted
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractFromDatabase
        {
            set
            {
                this.extractFromDatabase = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the file to where the output of the command extractor is to be saved
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractTargetFile
        {
            set
            {
                this.extractTargetFile = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the file enumerating the SQL commands to be executed 
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractCommandsFile
        {
            set
            {
                this.extractCommands = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the permissions template file containing the configuration for scripting permissions
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem PermissionsTemplate
        {
            set
            {
                this.permissionsTemplate = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not permissions are to be scripted
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ScriptPermissions
        {
            set
            {
                this.scriptPermissions = (value.ItemSpec == "0") ? false : true;
            }
        }

        /// <summary>
        /// Sets the output file for the generated permissions
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem PermissionsTargetFile
        {
            set
            {
                this.permissionsTargetFile = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the flag instruct the SqlExtract class whether or not to use the ExistenceCheckerProvider object to determine whether or not an
        /// object already exists in a "source" schema (or manifest), and if so, do not create a drop statement, but
        /// instead modify the script to perform an ALTER
        /// <para></para>
        /// Currently, this check is only performed on stored procedures, functions and views.
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem AlterIfExists
        {
            set
            {
                this.alterIfExists = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }

        /// <summary>
        /// Sets the IExistenceChecker type string that will be created to perform an existence check on an object, as
        /// per the "AlterIfExists" property description
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExistenceCheckerProvider
        {
            set
            {
                this.existenceCheckerProvider = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the configuration parameter used for the IExistenceChecker. 
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExistenceCheckerConfiguration
        {
            set
            {
                this.existenceCheckerConfiguration = value.ItemSpec;
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
        /// Sets the sql user that will be used to connect to the database
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractUsername
        {
            set
            {
                this.extractUsername = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets the password that will be used to connect to the database
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem ExtractPassword
        {
            set
            {
                this.extractPassword = value.ItemSpec;
            }
        }

        /// <summary>
        /// Sets whether or not to the treat warnings as errors.
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem TreatWarningsAsErrors
        {
            set
            {
                this.treatWarningsAsErrors = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }

        /// <summary>
        /// Sets whether or not a warning should be raised on unmatched types
        /// </summary>
        public Microsoft.Build.Framework.ITaskItem WarnOnUnmatchedTypes
        {
            set
            {
                this.warnOnUnmatchedTypes = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [extract assemblies].
        /// </summary>
        /// <value><c>true</c> if [extract assemblies]; otherwise, <c>false</c>.</value>
        public Microsoft.Build.Framework.ITaskItem ExtractAssemblies
        {
            set
            {
                this.extractAssemblies = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }


        /// <summary>
        /// Sets a value indicating whether or not drop scripts should be generated for synonyms.
        /// </summary>
        /// <value><c>true</c> if drop scripts should be generated; otherwise, <c>false</c>.</value>
        public Microsoft.Build.Framework.ITaskItem GenerateDropForSynonyms
        {
            set
            {
                this.generateDropsForSynonyms = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not drop scripts should be generated for default constraints.
        /// </summary>
        /// <value><c>true</c> if drop scripts should be generated; otherwise, <c>false</c>.</value>
        public Microsoft.Build.Framework.ITaskItem GenerateDropsForDefaultConstraints
        {
            set
            {
                this.generateDropsForDefaultConstraints = (value.ItemSpec == "0" || string.IsNullOrEmpty(value.ItemSpec)) ? false : true;
            }
        }

        /// <summary>
        /// Sets the extract assemblies config.
        /// </summary>
        /// <value>The extract assemblies config.</value>
        public Microsoft.Build.Framework.ITaskItem ExtractAssembliesConfig
        {
            set
            {
                this.extractAssembliesConfigFile = value.ItemSpec;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>True if successful</returns>
        public override bool Execute()
        {
            bool retVal = false;
            try
            {
                // Create instance of the extractor
                Extractor extractor = new Extractor();

                // set the properties
                extractor.AppendDatabaseNameToScripts = this.appendDatabaseName;
                extractor.BuildPath = this.buildPath;
                extractor.ExtractCommandsFile = this.extractCommands;
                extractor.ExtractData = this.extractData;
                extractor.ExtractFromDatabase = this.extractFromDatabase;
                extractor.ExtractFromServer = this.extractFromServer;
                extractor.ExtractTargetFile = this.extractTargetFile;
                extractor.PermissionsTargetFile = this.permissionsTargetFile;
                extractor.PermissionsTemplate = this.permissionsTemplate;
                extractor.ProjectGUID = this.projectGUID;
                extractor.SchemaScripts = this.sqlScripts;
                extractor.ScriptPermissions = this.scriptPermissions;
                extractor.SplitDropToSeperateFile = this.dropToSeperateFile;
                extractor.SqlInstance = this.sqlInstanceName;
                extractor.StaticFiles = this.customFiles;
                extractor.AlterIfExists = this.alterIfExists;
                extractor.ExistenceCheckerProvider = this.existenceCheckerProvider;
                extractor.ExistenceCheckerConfiguration = this.existenceCheckerConfiguration;
                extractor.SqlUsername = this.sqlUsername;
                extractor.SqlPassword = this.sqlPassword;
                extractor.ExtractUsername = this.extractUsername;
                extractor.ExtractPassword = this.extractPassword;
                extractor.TreatWarningsAsErrors = this.treatWarningsAsErrors;
                extractor.WarnOnUnmatchedTypes = this.warnOnUnmatchedTypes;
                extractor.ExtractAssemblies = this.extractAssemblies;
                extractor.ExtractAssembliesConfig = this.extractAssembliesConfigFile;
                extractor.GenerateDropForSynonyms = this.generateDropsForSynonyms;
                extractor.GenerateDropForDefaultConstraints = this.generateDropsForDefaultConstraints;

                // set the logger
                Utilities.Logger.Init(this);

                retVal = extractor.Execute(Utilities.Logger);
            }
            catch (Exception ex)
            {
                if (Utilities.Logger.LogErrorFromException(ex))
                {
                    throw;
                }
            }

            return retVal;
        }

        #endregion Public Methods
    }
}
