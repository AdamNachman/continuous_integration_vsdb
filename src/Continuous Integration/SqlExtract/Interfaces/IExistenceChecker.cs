// <copyright file="IExistenceChecker.cs" company="Adam Nachman">
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
    /// <summary>
    /// The interface implemented by all ExistenceChecker classes
    /// </summary>
    public interface IExistenceChecker
    {
        #region Properties

        /// <summary>
        /// Gets or sets the configuration string of the IExistenceChecker
        /// </summary>
        string ConfigurationString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ILogger for of the IExistenceChecker
        /// </summary>
        SqlDeployment.Interfaces.ILogger Logger
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Determines whether or not an object already exists in the schema/manifest file
        /// </summary>
        /// <param name="schemaName">The name of the schema to which the object belongs</param>
        /// <param name="objectName">The name of the object</param>
        /// <returns>True if the object exists, else false</returns>
        bool DoesDBObjectExists(string schemaName, string objectName);

        /// <summary>
        /// Initializes the object
        /// </summary>
        void Initialize();

        #endregion Methods
    }
}
