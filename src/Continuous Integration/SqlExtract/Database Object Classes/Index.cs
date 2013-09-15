// <copyright file="Index.cs" company="Adam Nachman">
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
    /// The Index class
    /// </summary>
    public class Index : ClusteredConstraint
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Index class
        /// </summary>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        public Index(string parentName, string name, string ddl)
            : this(SystemConstants.DefaultSchema, parentName, name, ddl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Index class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        public Index(string schema, string parentName, string name, string ddl)
            : base(schema, parentName, name, ddl)
        {
        }

        #endregion Constructors
    }
}
