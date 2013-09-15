// <copyright file="PermissionPattern.cs" company="Adam Nachman">
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

    /// <summary>
    /// The PermissionPattern class
    /// </summary>
    [Serializable]
    public class PermissionPattern
    {
        #region Private Variables

        /// <summary>
        /// The list of PermissionMap objects
        /// </summary>
        private List<PermissionMap> permissionMaps = null;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PermissionPattern class
        /// </summary>
        public PermissionPattern() :
            this(string.Empty, new List<PermissionMap>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the PermissionPattern class
        /// </summary>
        /// <param name="objectName">The name of the object</param>
        /// <param name="permissionMaps">The permissions that should be applied</param>
        public PermissionPattern(string objectName, List<PermissionMap> permissionMaps)
        {
            this.permissionMaps = permissionMaps;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the list of PermissionMap objects
        /// </summary>
        public List<PermissionMap> PermissionMaps
        {
            get
            {
                return this.permissionMaps;
            }
            set
            {
                this.permissionMaps = value;
            }
        }

        #endregion Public Properties
    }
}
