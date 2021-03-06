﻿// <copyright file="ProcessClrAssemblies.cs" company="Adam Nachman">
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
    /// The ProcessSchema class
    /// </summary>
    internal class ProcessClrAssemblies : ProcessDatabase<ClrAssembly>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessClrAssemblies"/> class.
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessClrAssemblies(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessClrAssemblies"/> class.
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessClrAssemblies(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
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
                return string.Concat(this.GetFilePrefix(), "Assemblies.sql");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(ClrAssembly newObject)
        {
            base.AddObject(newObject);
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting assemblies...");
            base.CheckDependencies();
        }

        #endregion Methods
    }
}