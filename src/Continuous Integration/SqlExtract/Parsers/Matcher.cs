// <copyright file="Matcher.cs" company="Adam Nachman">
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
namespace AdamNachman.Build.SqlExtract.Parsers
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// A matcher class for regex
    /// </summary>
    internal class Matcher
    {
        private Regex regEx;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public Matcher(Regex regex)
            : this(regex, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        /// <param name="isFullText">if set to <c>true</c> [is full text].</param>
        public Matcher(Regex regex, bool isFullText)
        {
            this.IsFullText = isFullText;
            this.regEx = regex;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is full text.
        /// </summary>
        /// <value><c>true</c> if this instance is full text; otherwise, <c>false</c>.</value>
        public bool IsFullText { get; private set; }

        /// <summary>
        /// Matches the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>A Match object</returns>
        public Match Match(string script)
        {
            return this.regEx.Match(script);
        }
    }
}
