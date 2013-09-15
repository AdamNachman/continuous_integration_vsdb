// <copyright file="ScriptMatcher.cs" company="Adam Nachman">
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
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class ScriptMatcher
    {
        #region Private Variables

        private static List<Matcher> matchers = null;

        #endregion Private Variables

        #region Public Methods

        public static ParseResult ParseScript(string script)
        {
            ParseResult result = new ParseResult();
            List<Matcher> matchList = GetMatchers();
            Match match = null;
            int i = 0;
            int length = matchList.Count;
            while (!result.Matched && i < length)
            {
                match = matchList[i].Match(script);
                result.Matched = match.Success;
                result.IsFullText = match.Success && matchList[i].IsFullText;
                i++;
            }

            if (result.Matched && match != null)
            {
                result.TypeName = Utilities.SanitizeTypeName(match.Groups["type"].Value.ToUpperInvariant());
                result.Parent = match.Groups["parent"].Value;
                result.Name = Utilities.SanitizeObjectName(result.IsFullText ? match.Groups["name"].Value + match.Groups["keyindex"].Value : match.Groups["name"].Value);
                result.SchemaName = Utilities.SanitizeSchemaName(match.Groups["schema"].Value, IgnoreSchemaForType(result.TypeName));
            }

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IgnoreSchemaForType(string typeName)
        {
            switch (typeName)
            {
                case "ASSEMBLY":
                case "SCHEMA":
                case "ROUTE":
                case "ROLE":
                case "SERVICE":
                case "FILEGROUP":
                case "FULLTEXT CATALOG":
                case "MESSAGE TYPE":
                case "PARTITION SCHEME":
                case "PARTITION FUNCTION":
                    return true;
                default:
                    return false;
            }
        }

        private static List<Matcher> GetMatchers()
        {
            if (matchers == null)
            {
                matchers = new List<Matcher>();
                matchers.Add(new Matcher(new Regex(@"CREATE\s+(?<type>FULLTEXT\s+INDEX)\s+ON\s+?\[(?<schema>\[?([A-Za-z_]*)?\]?\.)?\[?(?<name>[\w.]+)\]?\s+(?<definition>\(?\[?\w.*?\))\s+(KEY\s+INDEX)\s+?\[?(?<keyindex>\w.*)?\]*\s*(ON\s+?\[?(?<catalog>\w.*)?\]*)*", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline), true));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+(?<type>PARTITION\s+FUNCTION|PARTITION\s+SCHEME)\s+?\[?(?<name>[\w.\s]+)\]?(\s*((AS\s+PARTITION)\s+?\[?(?<parent>[\w.]+)\]?))*", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+?(?<type>(UNIQUE\s+INDEX|INDEX|UNIQUE\s+CLUSTERED\s+INDEX|UNIQUE\s+NONCLUSTERED\s+INDEX|CLUSTERED\s+INDEX|NONCLUSTERED\s+INDEX|CONSTRAINT)+)\s+?\[?(?<name>[\w.\-]+)\]?(\s+ON\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<parent>\w+)\]?)?", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+?(?<type>(FUNCTION|FUNC|PROCEDURE|PROC|TABLE|VIEW)+)\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?(?<name>(?:\[[\w\s]+\])|(?:\[\w+])|(?:\w+))", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+([\n\r])*?(?<type>(SCHEMA|ASSEMBLY|ROUTE|ROLE|SERVICE|CONTRACT|FULLTEXT\s+CATALOG|MESSAGE\s+TYPE)+)\s+?\[?(?<name>[\w./-]+)\]?(\s*((AUTHORIZATION)\s+?\[?(?<parent>[\w.]+)\]?))*", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+?(?<type>(TRIGGER)+)\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<name>\w+)\]?(\s+ON\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<parent>\w+)\]?)?", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+((NON|UNIQUE\s+)?CLUSTERED\s+)?(?<type>[A-Za-z]*)\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<name>[\w.\-]+)\]?(\s+ON\s+(\[?dbo\]?\.)?\[?(?<parent>\w+)\]?)?", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"CREATE\s+?(?<type>[A-Za-z]*)\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<name>[\w.]+)\]?(\s+ON\s+(\[?dbo\]?\.)?\[?(?<parent>\w+)\]?)?", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"ALTER\s+TABLE\s+(?<schema>\[?([A-Za-z_]*)\]?\.)?\[?(?<parent>\w+)\]?\s+(WITH\s+(NO)?CHECK\s+)?ADD\s+CONSTRAINT\s+\[?(?<name>\w+)\]?\s+(?<type>DEFAULT|CHECK|FOREIGN\s+KEY|PRIMARY\s+KEY|UNIQUE)", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
                matchers.Add(new Matcher(new Regex(@"(ALTER+.+DATABASE)+.+()+\s+ADD+\s+(?<type>FILEGROUP)+\s+(?<name>(?:\[[\w\s]+\])|(?:\[\w+])|(?:\w+))", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline)));
            }
            return matchers;
        }

        #endregion Private Methods
    }
}
