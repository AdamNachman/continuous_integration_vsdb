// <copyright file="ProcessClusteredConstraints.cs" company="Adam Nachman">
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
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The ProcessClusteredConstraint class
    /// </summary>
    internal class ProcessClusteredConstraints : ProcessDatabase<BaseConstraint>
    {
        #region Private Variables

        /// <summary>
        /// The table processor associated with the object
        /// </summary>
        private ProcessTables tables;

        /// <summary>
        /// The view processor associated with the object
        /// </summary>
        private ProcessViews views;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessClusteredConstraints class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessClusteredConstraints(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessClusteredConstraints class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessClusteredConstraints(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
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
                return string.Concat(this.GetFilePrefix(), "ClusteredConstraints.sql");
            }
        }

        /// <summary>
        /// Sets the tables associated with the processor 
        /// </summary>
        internal ProcessTables Tables
        {
            set
            {
                this.tables = value;
            }
        }

        /// <summary>
        /// Sets the views associated with the processor 
        /// </summary>
        internal ProcessViews Views
        {
            set
            {
                this.views = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(BaseConstraint newObject)
        {
            base.AddObject(newObject);
            this.tables.AssociateConstraint(newObject);
            this.views.AssociateConstraint(newObject);
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting clustered constraints...");

            foreach (BaseConstraint constraint in this.tables.ClusteredConstraints)
            {
                ExecuteScript(string.Concat(constraint.Schema, constraint.Name));
            }

            foreach (BaseConstraint constraint in this.views.ClusteredConstraints)
            {
                ExecuteScript(string.Concat(constraint.Schema, constraint.Name));
            }

            base.CheckDependencies();
        }

        #endregion Methods
    }
}