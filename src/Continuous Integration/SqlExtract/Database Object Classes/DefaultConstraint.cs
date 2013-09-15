// <copyright file="DefaultConstraint.cs" company="Adam Nachman">
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
    /// The DefaultConstraint class
    /// </summary>
    public class DefaultConstraint : BaseConstraint
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DefaultConstraint class
        /// </summary>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includeDrop">if set to <c>true</c> include drop statement in ddl.</param>
        public DefaultConstraint(string parentName, string name, string ddl, bool includeDrop)
            : this(SystemConstants.DefaultSchema, parentName, name, ddl, includeDrop)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DefaultConstraint class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includeDrop">if set to <c>true</c> include drop statement in ddl.</param>
        public DefaultConstraint(string schema, string parentName, string name, string ddl, bool includeDrop)
            : base(schema, parentName, name, ddl, includeDrop)
        {
        }

        #endregion Constructors

        #region Protected Methods

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected override void LoadDdl(string ddl)
        {
            if (this.IncludeDropStatements)
            {
                this.Prefix = SystemConstants.SetQuotedIdentifiersOn + string.Format(
                                            "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{2}].[{1}]',N'D') AND parent_object_id = object_id('[{2}].[{0}]'))\r\n" +
                                            "BEGIN\r\n" +
                                            "  ALTER TABLE [{2}].[{0}] DROP CONSTRAINT [{1}]\r\n" +
                                            "\r\nEND\r\nGO\r\n\r\n" +

                                            "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{2}].[{1}]',N'D') AND parent_object_id = object_id('[{2}].[{0}]'))\r\nBEGIN\r\n",
                                            this.ParentName,
                                            this.Name,
                                            this.Schema);
            }
            else
            {
                this.Prefix = SystemConstants.SetQuotedIdentifiersOn + string.Format(
                                            "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{2}].[{1}]',N'D') AND parent_object_id = object_id('[{2}].[{0}]'))\r\nBEGIN\r\n",
                                            this.ParentName,
                                            this.Name,
                                            this.Schema);
            }
            this.Body = ddl;
            this.Suffix = "\r\nEND\r\nGO";
        }

        #endregion Protected Methods
    }
}
