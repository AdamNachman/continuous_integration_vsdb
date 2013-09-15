// <copyright file="ProcessFunctions.cs" company="Adam Nachman">
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
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The ProcessFunctions class
    /// </summary>
    internal class ProcessFunctions : ProcessDatabase<UDFunction>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessFunctions class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessFunctions(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessFunctions class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessFunctions(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
            : base(targetDatabase, appendDatabaseNameToScripts, appendDatabaseName)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the name of the output file
        /// </summary>
        internal override string FileName
        {
            get
            {
                return string.Concat(this.GetFilePrefix(), "Functions.sql");
            }
        }

        /// <summary>
        /// Gets the name of the drop file
        /// </summary>
        internal override string DropFileName
        {
            get
            {
                return string.Concat(this.GetFilePrefix(), "DropFunctions.sql");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(UDFunction newObject)
        {
            base.AddObject(newObject);

            if (!newObject.PreExists)
            {
                string deleteMe = string.Format("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{1}].[{0}]') AND type IN (N'FN', N'FS', N'FT', N'IF', N'TF'))\r\n  DROP FUNCTION [{1}].[{0}]\r\nGO", newObject.Name, newObject.Schema);
                this.AddDropScript(deleteMe);
            }
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting functions...");
            base.CheckDependencies();
        }

        #endregion Methods
    }
}