// <copyright file="ManifestExistenceChecker.cs" company="Adam Nachman">
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
    using System.IO;

    /// <summary>
    /// Checks for the existince of a schema object based on the object name in a file
    /// </summary>
    public class ManifestExistenceChecker : IExistenceChecker
    {
        #region Private Variables

        private SqlDeployment.Interfaces.ILogger logger = null;
        private string configurationString = string.Empty;
        private List<string> objectNames = new List<string>();

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or sets the configuration string of the IExistenceChecker
        /// </summary>
        public string ConfigurationString
        {
            get
            {
                return this.configurationString;
            }
            set
            {
                this.configurationString = value;
            }
        }

        /// <summary>
        /// Gets or sets the ILogger for of the IExistenceChecker
        /// </summary>
        public SqlDeployment.Interfaces.ILogger Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                this.logger = value;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Determines whether or not an object already exists in the schema/manifest file
        /// </summary>
        /// <param name="schemaName">The name of the schema to which the object belongs</param>
        /// <param name="objectName">The name of the object</param>
        /// <returns>True if the object exists, else false</returns>
        public bool DoesDBObjectExists(string schemaName, string objectName)
        {
            return this.objectNames.Contains(string.Concat(Utilities.NormaliseNameString(schemaName), ".", Utilities.NormaliseNameString(objectName)));
        }

        /// <summary>
        /// Initializes the object
        /// </summary>
        public void Initialize()
        {
            // Load the file manifest into the List
            if (File.Exists(this.configurationString))
            {
                using (StreamReader sr = new StreamReader(this.configurationString))
                {
                    string objectName = string.Empty;
                    while (sr.Peek() >= 0)
                    {
                        objectName = sr.ReadLine();
                        if (!string.IsNullOrEmpty(objectName.Trim()))
                        {
                            if (!this.objectNames.Contains(objectName))
                            {
                                this.objectNames.Add(objectName);
                            }
                        }
                    }

                    sr.Close();
                }
            }
            else
            {
                throw new FileNotFoundException(string.Format("The configured file for the manifest checker {0} does not exist.", this.configurationString));
            }
        }

        #endregion Methods
    }
}
