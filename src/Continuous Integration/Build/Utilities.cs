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
namespace SqlDeployment.Build
{
    using System.Collections.Generic;

    /// <summary>
    /// An internal, static class of utility methods
    /// </summary>
    internal static class Utilities
    {
        #region Private Variables

        /// <summary>
        /// The static logger to use
        /// </summary>
        private static volatile SqlDeployment.Interfaces.ILogger logger = new BaseLog();

        #endregion Private Variables

        #region Static Properties

        /// <summary>
        /// Gets the static logger instance
        /// </summary>
        public static SqlDeployment.Interfaces.ILogger Logger
        {
            get
            {
                return logger;
            }
        }

        #endregion Static Properties

        #region Static Methods

        /// <summary>
        /// Accepts an array of ITaskItems and returns a string list
        /// </summary>
        /// <param name="names">The ITaskItems array to process</param>
        /// <returns>A list of strings</returns>
        public static List<string> TaskItemToList(Microsoft.Build.Framework.ITaskItem[] names)
        {
            List<string> result = new List<string>();
            foreach (Microsoft.Build.Framework.ITaskItem s in names)
            {
                result.Add(s.ItemSpec);
            }

            return result;
        }

        #endregion Static Methods
    }
}
