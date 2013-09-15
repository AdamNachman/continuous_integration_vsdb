// <copyright file="SystemConstants.cs" company="Adam Nachman">
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
    /// System constants, used internally
    /// </summary>
    internal class SystemConstants
    {
        /// <summary>
        /// The default schema
        /// </summary>
        public const string DefaultSchema = "dbo";

        /// <summary>
        /// String for setting Quoted Identifiers On
        /// </summary>
        public const string SetQuotedIdentifiersOn = "SET QUOTED_IDENTIFIER ON\r\nGO\r\n";

        /// <summary>
        /// String for setting Quoted Identifiers Off
        /// </summary>
        public const string SetQuotedIdentifiersOff = "\r\nSET QUOTED_IDENTIFIER OFF\r\nGO\r\n";

        /// <summary>
        /// Adds a batch end (GO) and carriage returns
        /// </summary>
        public const string BatchEnd = "\r\nGO\r\n";

        /// <summary>
        /// Token replacement used to represent a database name
        /// </summary>
        public const string DatabaseReplacementToken = "$(DatabaseName)";
    }
}
