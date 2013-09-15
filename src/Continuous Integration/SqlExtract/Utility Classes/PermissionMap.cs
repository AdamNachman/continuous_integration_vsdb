// <copyright file="PermissionMap.cs" company="Adam Nachman">
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

    /// <summary>
    /// The PermissionMap class
    /// </summary>
    [Serializable]
    public class PermissionMap
    {
        #region Private Variables

        /// <summary>
        /// The database principal name
        /// </summary>
        private string principalName = string.Empty;

        /// <summary>
        /// The grant type
        /// </summary>
        private string grantType = string.Empty;

        /// <summary>
        /// The object types
        /// </summary>
        private string objectTypes = string.Empty;

        /// <summary>
        /// The name of the target schema
        /// </summary>
        private string schemaName;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PermissionMap class
        /// </summary>
        public PermissionMap() :
            this(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PermissionMap class
        /// </summary>
        /// <param name="principalName">The name of the database principle</param>
        /// <param name="grantType">The type of permission to grant</param>
        /// <param name="objectTypes">The object types against which the grants should apply</param>
        public PermissionMap(string principalName, string grantType, string objectTypes) :
            this(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PermissionMap class
        /// </summary>
        /// <param name="principalName">The name of the database principle</param>
        /// <param name="grantType">The type of permission to grant</param>
        /// <param name="objectTypes">The object types against which the grants should apply</param>
        /// <param name="schemaName">Name of the schema to which the permissions should apply.</param>
        public PermissionMap(string principalName, string grantType, string objectTypes, string schemaName)
        {
            this.principalName = principalName;
            this.grantType = grantType;
            this.schemaName = objectTypes;
            this.objectTypes = schemaName;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the grant type
        /// </summary>
        public string GrantType
        {
            get
            {
                return this.grantType;
            }
            set
            {
                this.grantType = value;
            }
        }

        /// <summary>
        /// Gets or sets the object types
        /// </summary>
        public string ObjectTypes
        {
            get
            {
                return this.objectTypes;
            }
            set
            {
                this.objectTypes = value;
            }
        }

        /// <summary>
        /// Gets or sets the database principal name
        /// </summary>
        public string PrincipalName
        {
            get
            {
                return this.principalName;
            }
            set
            {
                this.principalName = value;
            }
        }

        /// <summary>
        /// Gets or sets the schema name for which permissions should be extracted
        /// </summary>
        public string SchemaName
        {
            get
            {
                return this.schemaName;
            }
            set
            {
                this.schemaName = value;
            }
        }

        #endregion Public Properties
    }
}
