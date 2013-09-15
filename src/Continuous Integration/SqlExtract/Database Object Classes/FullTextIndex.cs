// <copyright file="FullTextIndex.cs" company="Adam Nachman">
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
    using System.Text.RegularExpressions;

    /// <summary>
    /// The FullTextIndex class
    /// </summary>
    public class FullTextIndex : BaseDBObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FullTextIndex class
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        public FullTextIndex(string name, string ddl)
            : base(name, ddl)
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
            Regex objectDef = new Regex(@"CREATE\s+(?<type>FULLTEXT\s+INDEX)\s+ON\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<name>[\w.]+)\]?\s+(?<definition>\(?\[?\w.*?\))\s+(KEY\s+INDEX)\s+?\[?(?<keyindex>\w.*)?\]\s+ON\s+?\[?(?<catalog>\w.*)?\]");
            Match m = objectDef.Match(ddl);
            string keyindex = m.Groups["keyindex"].Value;
            string targetObject = string.Concat(m.Groups["schema"].Value, m.Groups["name"].Value);
            string catalog = m.Groups["catalog"].Value;

            this.Prefix = string.Format("IF NOT EXISTS (SELECT * FROM sys.fulltext_indexes sfi INNER JOIN sys.indexes si ON sfi.unique_index_id=si.index_id AND sfi.object_id=si.object_id WHERE si.name = '{0}' AND sfi.object_id=object_id('{1}'))\r\nBEGIN\r\n", keyindex, targetObject);

            this.Body = ddl;
            this.Suffix = "\r\nEND\r\nGO";
        }

        #endregion Protected Methods
    }
}
