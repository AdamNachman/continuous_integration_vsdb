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
    using System.Collections.Generic;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// The generic abstract ProcessDatabase class
    /// </summary>
    /// <typeparam name="T">The type of processor</typeparam>
    internal abstract class ProcessDatabase<T> : ProcessDatabase where T : IDBObject
    {
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
            : base(targetDatabase, appendDatabaseNameToScripts, appendDatabaseName)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the IEnumerable collection of pending objects
        /// </summary>
        internal new IEnumerable<T> PendingDBObjects
        {
            get
            {
                foreach (T po in base.PendingDBObjects)
                {
                    yield return po;
                }
            }
        }

        /// <summary>
        /// Gets the IEnumerable collection of objects
        /// </summary>
        internal new IEnumerable<T> DatabaseObjects
        {
            get
            {
                foreach (T databaseObject in base.DatabaseObjects)
                {
                    yield return databaseObject;
                }
            }
        }

        #endregion Properties

        #region Internal Methods

        /// <summary>
        /// Adds an object to the internal collection
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal virtual void AddObject(T newObject)
        {
            base.AddObject(newObject);
        }

        /// <summary>
        /// Adds a new object
        /// </summary>
        /// <param name="newObject">The object to add</param>
        internal override void AddObject(IDBObject newObject)
        {
            this.AddObject((T)newObject);
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// Returns the an object based on the name
        /// </summary>
        /// <param name="name">The name of the object to return</param>
        /// <returns>null if the object cannot be retrieved</returns>
        protected new T GetDatabaseObject(string name)
        {
            return (T)base.GetDatabaseObject(name);
        }

        #endregion Protected Methods
    }
}
