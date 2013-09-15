// <copyright file="ExistenceCheckerDefault.cs" company="Adam Nachman">
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
    /// The default checker - will always return false
    /// </summary>
    internal class ExistenceCheckerDefault : IExistenceChecker
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExistenceCheckerDefault class
        /// </summary>
        public ExistenceCheckerDefault()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the configuration string of the IExistenceChecker. In this case, the class will always return false, so this is not actuall implemented
        /// </summary>
        public string ConfigurationString
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the ILogger for of the IExistenceChecker
        /// </summary>
        public SqlDeployment.Interfaces.ILogger Logger
        {
            get
            {
                return null;
            }
            set
            {
                // Do nothing - since this always returns false for the existence check, the logger is irrelevant
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// This is the default existence checker class - will always return false.
        /// </summary>
        /// <param name="schemaName">The name of the schema to which the object belongs</param>
        /// <param name="objectName">The name of the object</param>
        /// <returns>Always returns false</returns>
        public bool DoesDBObjectExists(string schemaName, string objectName)
        {
            return false;
        }

        /// <summary>
        /// Initializes the object
        /// </summary>
        public void Initialize()
        {
            // in this case, this does nothing
        }

        #endregion Methods
    }
}
