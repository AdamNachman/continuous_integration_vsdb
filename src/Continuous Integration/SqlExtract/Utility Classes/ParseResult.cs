// <copyright file="ParseResult.cs" company="Adam Nachman">
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
    /// Result of a parse operation
    /// </summary>
    public class ParseResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is full text.
        /// </summary>
        /// <value><c>true</c> if this instance is full text; otherwise, <c>false</c>.</value>
        public bool IsFullText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ParseResult"/> is matched.
        /// </summary>
        /// <value><c>true</c> if matched; otherwise, <c>false</c>.</value>
        public bool Matched { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the object.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>The name of the schema.</value>
        public string SchemaName { get; set; }
    }
}
