// <copyright file="ScriptExecutorConfiguration.cs" company="Adam Nachman">
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
namespace SqlDeployment
{
    using System;
    using SqlDeployment.Utilities;

    [Serializable]
    public class ScriptExecutorConfiguration
    {
        #region Private Variables

        private string targetDatabase;
        private string targetServer;
        private SerializableDictionary<int, string> executionSequence;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScriptExecutorConfiguration class
        /// </summary>
        public ScriptExecutorConfiguration()
        {
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the list of scripts to execute
        /// </summary>
        public SerializableDictionary<int, string> ExecutionSequence
        {
            get
            {
                return this.executionSequence;
            }

            set
            {
                this.executionSequence = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the target database
        /// </summary>
        public string TargetDatabase
        {
            get
            {
                return this.targetDatabase;
            }

            set
            {
                this.targetDatabase = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the target server
        /// </summary>
        public string TargetServer
        {
            get
            {
                return this.targetServer;
            }

            set
            {
                this.targetServer = value;
            }
        }

        #endregion Public Properties
    }
}
