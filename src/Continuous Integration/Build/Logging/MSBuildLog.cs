// <copyright file="MSBuildLog.cs" company="Adam Nachman">
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
    using System;
    using Microsoft.Build.Utilities;

    internal class MSBuildLog : BaseBuildLog
    {
        #region Private Variables

        private TaskLoggingHelper log;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MSBuildLog class
        /// </summary>
        internal MSBuildLog(TaskLoggingHelper log)
        {
            this.log = log;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void LogInformation(string message)
        {
            this.log.LogMessage(message);
        }

        protected override void LogWarning(string message)
        {
            this.log.LogWarning(message);
        }

        protected override void LogError(string message)
        {
            this.log.LogError(message);
        }

        protected override bool LogErrorFromException(Exception ex)
        {
            this.log.LogErrorFromException(ex);
            return false;
        }

        #endregion Protected Methods
    }
}