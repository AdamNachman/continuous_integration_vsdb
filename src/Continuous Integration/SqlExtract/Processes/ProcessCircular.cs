﻿// <copyright file="ProcessCircular.cs" company="Adam Nachman">
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
    /// The ProcessCircular class
    /// </summary>
    internal class ProcessCircular : ProcessDatabase
    {
        #region Private Variables

        /// <summary>
        /// The function processor associated with the object
        /// </summary>
        private ProcessFunctions functions;

        /// <summary>
        /// The view processor associated with the object
        /// </summary>
        private ProcessViews views;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessCircular class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="functions">The functions associated with the database</param>
        /// <param name="views">The views associated with the database</param>
        internal ProcessCircular(Database targetDatabase, ProcessFunctions functions, ProcessViews views)
            : this(targetDatabase, functions, views, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessCircular class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="functions">The functions associated with the database</param>
        /// <param name="views">The views associated with the database</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessCircular(Database targetDatabase, ProcessFunctions functions, ProcessViews views, bool appendDatabaseNameToScripts, string appendDatabaseName)
            : base(targetDatabase, appendDatabaseNameToScripts, appendDatabaseName)
        {
            this.functions = functions;
            this.views = views;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the filename
        /// </summary>
        internal override string FileName
        {
            get
            {
                return string.Concat(this.GetFilePrefix(), "CircularReferences.sql");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(IDBObject newObject)
        {
            base.AddObject(newObject);
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting circular references...");

            foreach (IDBObject so in this.functions.PendingDBObjects)
            {
                this.AddObject(so);
            }

            this.functions.RemovePendingDBObject();
            foreach (IDBObject so in this.views.PendingDBObjects)
            {
                this.AddObject(so);
            }

            this.views.RemovePendingDBObject();

            base.CheckDependencies();
        }

        #endregion Methods
    }
}