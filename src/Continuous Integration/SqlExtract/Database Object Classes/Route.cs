// <copyright file="Route.cs" company="Adam Nachman">
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
    /// The Route class
    /// </summary>
    public class Route : BaseDBObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Route class
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        public Route(string name, string ddl)
            : base(name, ddl)
        {
        }

        #endregion Constructors

        #region Protected Methods

        /// <summary>
        /// Gets the Ddl of the object
        /// </summary>
        public override string Ddl
        {
            // override Ddl to use the complete Ddl (which includes existence checks), since on extract the
            // autocreated values pre-exist, and this raises errors during the checking of dependancies
            get
            {
                return this.CompleteDdl;
            }
        }

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected override void LoadDdl(string ddl)
        {
            this.Prefix = string.Format("IF NOT EXISTS (SELECT * FROM sys.routes WHERE name = N'{0}')\r\nBEGIN\r\n", this.Name);

            this.Body = ddl;
            this.Suffix = "\r\nEND\r\nGO";
        }

        #endregion Protected Methods
    }
}
