﻿// <copyright file="View.cs" company="Adam Nachman">
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
    /// The View class
    /// </summary>
    public class View : BaseTable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the View class
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="preExists">Indicates whether or not the object exists in the target schema</param>
        public View(string name, string ddl, bool preExists)
            : this(SystemConstants.DefaultSchema, name, ddl, preExists)
        {
        }

        /// <summary>
        /// Initializes a new instance of the View class
        /// </summary>
        /// <param name="schema">The schema of the object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="preExists">Indicates whether or not the object exists in the target schema</param>
        public View(string schema, string name, string ddl, bool preExists)
            : base(schema, name, ddl, preExists)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the complete Ddl, including prefix and suffix
        /// </summary>
        public override string CompleteDdl
        {
            get
            {
                if (this.PreExists)
                {
                    return string.Concat(Utilities.ConvertCreateToAlter(this.Body), this.Suffix);
                }
                else
                {
                    return string.Concat(this.Prefix, this.Body, this.Suffix);
                }
            }
        }

        #endregion Properties

        #region Internal Methods

        /// <summary>
        /// Links a constraint to the object
        /// </summary>
        /// <param name="constraint">The costraint to link</param>
        internal override void LinkConstraint(BaseConstraint constraint)
        {
            constraint.IsOwnedByView = true;
            this.AddConstraint(constraint);
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected override void LoadDdl(string ddl)
        {
            this.Prefix = SystemConstants.SetQuotedIdentifiersOn;
            this.Body = ddl;
            this.Suffix = "\r\nGO";
        }

        #endregion Protected Methods
    }
}
