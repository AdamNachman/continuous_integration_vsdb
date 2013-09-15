// <copyright file="ProcessViews.cs" company="Adam Nachman">
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
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The ProcessViews class
    /// </summary>
    internal class ProcessViews : ProcessDatabase<View>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ProcessViews class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        internal ProcessViews(Database targetDatabase)
            : this(targetDatabase, false, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ProcessViews class
        /// </summary>
        /// <param name="targetDatabase">The database against which the processor will execute</param>
        /// <param name="appendDatabaseNameToScripts">Indicates whether or not the database name will be appended to the script output</param>
        /// <param name="appendDatabaseName">The name to append</param>
        internal ProcessViews(Database targetDatabase, bool appendDatabaseNameToScripts, string appendDatabaseName)
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
                return string.Concat(this.GetFilePrefix(), "Views.sql");
            }
        }

        /// <summary>
        /// Gets the name of the drop file
        /// </summary>
        internal override string DropFileName
        {
            get
            {
                return string.Concat(this.GetFilePrefix(), "DropViews.sql");
            }
        }

        /// <summary>
        /// Gets an IEnumerable list of the clustered constraints associated with the view
        /// </summary>
        internal IEnumerable<BaseConstraint> ClusteredConstraints
        {
            get
            {
                foreach (View vo in this.DatabaseObjects)
                {
                    ClusteredConstraint cc = vo.Clustered;
                    if (cc != null)
                    {
                        yield return cc;
                    }
                }
            }
        }

        /// <summary>
        /// Gets an IEnumerable list of the non foreign key constraints associated with the view
        /// </summary>
        internal IEnumerable<BaseConstraint> NonForeignConstraints
        {
            get
            {
                foreach (View view in this.DatabaseObjects)
                {
                    foreach (ClusteredConstraint clusteredConstraint in view.NonClustered)
                    {
                        yield return clusteredConstraint;
                    }

                    foreach (BaseConstraint cosntraint in view.OtherConstraints)
                    {
                        yield return cosntraint;
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
        internal override void AddObject(View newObject)
        {
            base.AddObject(newObject);

            if (!newObject.PreExists)
            {
                string deleteMe = string.Format("IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[{1}].[{0}]'))\r\n  DROP VIEW [{1}].[{0}]\r\nGO", newObject.Name, newObject.Schema);
                this.AddDropScript(deleteMe);
            }
        }

        /// <summary>
        /// Associates a constraint with the object
        /// </summary>
        /// <param name="constraint">The constraint to associate</param>
        internal void AssociateConstraint(BaseConstraint constraint)
        {
            View view = this.GetDatabaseObject(constraint.ParentName);
            //// try again with possible schema name concatenation
            if (view == null)
            {
                view = this.GetDatabaseObject(string.Concat(constraint.Schema, constraint.ParentName));
            }

            if (view != null)
            {
                view.LinkConstraint(constraint);
            }
        }

        /// <summary>
        /// Checks whether or not there are any dependancies
        /// </summary>
        internal override void CheckDependencies()
        {
            Utilities.Logger.LogInformation("Extracting views...");
            base.CheckDependencies();
        }

        #endregion Methods
    }
}