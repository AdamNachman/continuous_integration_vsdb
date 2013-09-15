// <copyright file="Table.cs" company="Adam Nachman">
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
    /// The Table class
    /// </summary>
    public class Table : BaseTable
    {
        #region Private Variables

        /// <summary>
        /// The foreign keys associated with the table
        /// </summary>
        private List<ForeignKey> foreignKeys;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Table class
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        public Table(string name, string ddl)
            : this(SystemConstants.DefaultSchema, name, ddl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Table class
        /// </summary>
        /// <param name="schema">The schema of the object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        public Table(string schema, string name, string ddl)
            : base(schema, name, ddl, false)
        {
            this.foreignKeys = new List<ForeignKey>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the foreign keys associated with the table
        /// </summary>
        internal IEnumerable<ForeignKey> ForeignKeys
        {
            get
            {
                foreach (ForeignKey fk in this.foreignKeys)
                {
                    yield return fk;
                }
            }
        }

        #endregion Properties

        #region Internal Methods

        /// <summary>
        /// Links a constraint to the table
        /// </summary>
        /// <param name="constraint">The constraint to associate with the table</param>
        internal override void LinkConstraint(BaseConstraint constraint)
        {
            ForeignKey fk = constraint as ForeignKey;
            if (fk != null)
            {
                this.foreignKeys.Add(fk);
            }
            else
            {
                this.AddConstraint(constraint);
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected override void LoadDdl(string ddl)
        {
            this.Prefix = SystemConstants.SetQuotedIdentifiersOn + string.Format("IF OBJECT_ID(N'[{1}].[{0}]', 'U') IS NULL\r\nBEGIN\r\n", this.Name, this.Schema);
            this.Body = ddl;
            this.Suffix = "\r\nEND\r\nGO\r\n";
        }

        #endregion Protected Methods
    }
}