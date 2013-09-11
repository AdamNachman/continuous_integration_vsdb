// <copyright file="AssemblyToExtract.cs" company="Adam Nachman">
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
    /// An assembly to extract to sql
    /// </summary>
    [Serializable]
    public class AssemblyToExtract
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path of the assembly to process.</value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the authorization.
        /// </summary>
        /// <value>The authorization. If empty, sql will use the default of the connected user when the assembly is created</value>
        public string Authorization { get; set; }

        /// <summary>
        /// Gets or sets the target path.
        /// </summary>
        /// <value>The target path where the assembly will be saved.</value>
        public string TargetPath { get; set; }

        /// <summary>
        /// Gets or sets the permission set.
        /// </summary>
        /// <value>The permission set. If empty, sql will use the default of SAFE</value>
        public string PermissionSet { get; set; }
    }
}
