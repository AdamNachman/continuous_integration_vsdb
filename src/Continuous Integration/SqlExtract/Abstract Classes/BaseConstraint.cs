// <copyright file="BaseDBObject.cs" company="Adam Nachman">
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
    /// The base constraint class, from which other constraints will inherit
    /// </summary>
    public abstract class BaseConstraint : BaseDBObject
    {
        #region Constants and Fields

        /// <summary>
        /// Indicates that the parent object is a view
        /// </summary>
        private bool isOwnedByView;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the BaseConstraint class
        /// </summary>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includedrop">if set to <c>true</c> then include drop statements.</param>
        protected BaseConstraint(string parentName, string name, string ddl, bool includedrop)
            : this(SystemConstants.DefaultSchema, parentName, name, ddl, includedrop)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseConstraint class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includedrop">if set to <c>true</c> then include drop statements.</param>
        protected BaseConstraint(string schema, string parentName, string name, string ddl, bool includedrop)
            : base(schema, parentName, name, ddl, false, includedrop)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not the object is owned by a view
        /// </summary>
        public bool IsOwnedByView
        {
            get
            {
                return this.isOwnedByView;
            }

            set
            {
                this.isOwnedByView = value;
                this.Prefix = SystemConstants.SetQuotedIdentifiersOn + this.Prefix;
                this.Suffix = this.Suffix + SystemConstants.SetQuotedIdentifiersOff;
            }
        }
        #endregion
    }
}