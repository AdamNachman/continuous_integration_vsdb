// <copyright file="Utilities.cs" company="Adam Nachman">
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
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A static Utilities class
    /// </summary>
    internal static class Utilities
    {
        #region Private Variables

        /// <summary>
        /// The static instance of the ILogger
        /// </summary>
        private static volatile SqlDeployment.Interfaces.ILogger logger = null;

        /// <summary>
        /// The static instance of the IExistenceChecker
        /// </summary>
        private static volatile AdamNachman.Build.SqlExtract.IExistenceChecker existenceChecker = null;

        #endregion Private Variables

        #region Static Methods

        /// <summary>
        /// Gets or sets the static instance of the ILogger
        /// </summary>
        public static SqlDeployment.Interfaces.ILogger Logger
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
            }
        }

        /// <summary>
        /// Gets or sets the static instance of the IExistenceChecker
        /// </summary>
        public static AdamNachman.Build.SqlExtract.IExistenceChecker ExistenceChecker
        {
            get
            {
                return existenceChecker;
            }
            set
            {
                existenceChecker = value;
            }
        }

        /// <summary>
        /// Accepts a string and ensures that it is returned in a predictable format
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <returns>The object name encapsulated in []</returns>
        public static string NormaliseNameString(string name)
        {
            name = name.Replace("]", string.Empty);
            name = name.Replace("[", string.Empty);
            return string.Concat("[", name, "]");
        }

        /// <summary>
        /// Accepts a dml script and converts it from a "CREATE" to an "ALTER"
        /// </summary>
        /// <param name="dml">The dml of the object</param>
        /// <returns>The dml to process</returns>
        public static string ConvertCreateToAlter(string dml)
        {
            Regex reg = new Regex(@"CREATE\s+(?<type>(PROC|PROCEDURE|FUNCTION|VIEW))", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = reg.Match(dml);
            if (m.Success)
            {
                dml = Regex.Replace(dml, @"CREATE\s+(?<type>(PROC|PROCEDURE|FUNCTION|VIEW))", string.Concat("ALTER ", m.Groups["type"].Value.ToString().ToUpper()));
            }

            return dml;
        }

        /// <summary>
        /// Sanitizes the name of the schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns>The sanitized schema</returns>
        public static string SanitizeSchemaName(string schema)
        {
            return SanitizeSchemaName(schema, false);
        }

        /// <summary>
        /// Cleans up the schema to ensure consistency
        /// </summary>
        /// <param name="schema">The schema to "sanitize"</param>
        /// <param name="ignoreEmpty">if set to <c>true</c>, do not replace empty schema with the default schema.</param>
        /// <returns>The sanitized schema</returns>
        public static string SanitizeSchemaName(string schema, bool ignoreEmpty)
        {
            if (string.IsNullOrEmpty(schema) && !ignoreEmpty)
            {
                return SystemConstants.DefaultSchema;
            }
            else
            {
                schema = schema.Replace("]", string.Empty);
                schema = schema.Replace("[", string.Empty);
                schema = schema.Replace(".", string.Empty);
            }

            return schema;
        }

        /// <summary>
        /// Removes whitespace from the string
        /// </summary>
        /// <param name="typeName">The string to process</param>
        /// <returns>A cleaned string</returns>
        public static string SanitizeTypeName(string typeName)
        {
            typeName = typeName.Replace("\t", " ");
            typeName = typeName.Replace(Environment.NewLine, " ");
            while (typeName.Contains("  "))
            {
                typeName = typeName.Replace("  ", " ");
            }
            return typeName.Trim();
        }

        /// <summary>
        /// Sanitizes the name of the object.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>A sanitized string</returns>
        public static string SanitizeObjectName(string objectName)
        {
            objectName = objectName.Replace("\t", string.Empty);
            objectName = objectName.Replace("]", string.Empty);
            objectName = objectName.Replace("[", string.Empty);
            objectName = objectName.Replace(Environment.NewLine, string.Empty);
            objectName = objectName.Replace("\r", string.Empty);
            return objectName;
        }

        /// <summary>
        /// Extracts a CLR to a file.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void ExtractClrToFile(AssemblyToExtract assembly)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("CREATE ASSEMBLY [{0}]", assembly.Name));
            if (!string.IsNullOrWhiteSpace(assembly.Authorization))
            {
                builder.AppendLine(string.Format("    AUTHORIZATION [{0}]", assembly.Authorization));
            }
            builder.AppendLine(string.Format("    FROM {0}", ExtractBinary(assembly.FilePath)));
            if (!string.IsNullOrWhiteSpace(assembly.PermissionSet))
            {
                builder.Append(string.Format("    WITH PERMISSION_SET = {0}", assembly.PermissionSet));
            }
            builder.Append(";");

            using (FileStream fs = new FileStream(assembly.TargetPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(builder.ToString().ToCharArray());
                    bw.Close();
                }

                fs.Close();
            }
        }

        /// <summary>
        /// Returns the hex representation of a binary file
        /// </summary>
        /// <param name="path">The fully qualified path.</param>
        /// <returns>A string representing the hex of the file bytes</returns>
        public static string ExtractBinary(string path)
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("0x");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int pos = fs.ReadByte();
                while (pos > -1)
                {
                    retVal.Append(pos.ToString("X2", CultureInfo.InvariantCulture));
                    pos = fs.ReadByte();
                }
                fs.Close();
            }

            return retVal.ToString();
        }

        #endregion Static Methods
    }
}