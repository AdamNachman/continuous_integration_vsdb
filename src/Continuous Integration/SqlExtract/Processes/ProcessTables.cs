// <copyright file="ProcessTables.cs" company="Adam Nachman">
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
    using System.Collections.Generic;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The ProcessTables class
    /// </summary>
    internal class ProcessTables : ProcessDatabase<Table>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessTables class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessTables(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessTables class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessTables(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
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
                return string.Concat(this.GetFilePrefix(), "Tables.sql");
            }
        }

        /// <summary>
        /// Gets the clustered associated with the object
        /// </summary>
        internal IEnumerable<BaseConstraint> ClusteredConstraints
        {
            get
            {
                foreach (Table to in this.DatabaseObjects)
                {
                    ClusteredConstraint cc = to.Clustered;
                    if (cc != null)
                    {
                        yield return cc;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the NonForeign constraints associated with the object
        /// </summary>
        internal IEnumerable<BaseConstraint> NonForeignConstraints
        {
            get
            {
                foreach (Table table in this.DatabaseObjects)
                {
                    foreach (ClusteredConstraint clusteredConstraint in table.NonClustered)
                    {
                        yield return clusteredConstraint;
                    }

                    foreach (BaseConstraint cosntraint in table.OtherConstraints)
                    {
                        yield return cosntraint;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the ForeignKeys associated with the object
        /// </summary>
        internal IEnumerable<ForeignKey> ForeignKeys
        {
            get
            {
                foreach (Table to in this.DatabaseObjects)
                {
                    foreach (ForeignKey fk in to.ForeignKeys)
                    {
                        yield return fk;
                    }
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(Table newObject)
        {
            base.AddObject(newObject);
        }

        /// <summary>
        /// Associates a constraint with the object
        /// </summary>
        /// <param name="constraint">The constraint to associate</param>
        internal void AssociateConstraint(BaseConstraint constraint)
        {
            Table table = this.GetDatabaseObject(constraint.ParentName);
            //// try again with possible schema concatenation
            if (table == null)
            {
                table = this.GetDatabaseObject(string.Concat(constraint.Schema, constraint.ParentName));
            }

            if (table != null)
            {
                table.LinkConstraint(constraint);
            }
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting tables...");
            base.CheckDependencies();
        }

        #endregion Methods
    }
}