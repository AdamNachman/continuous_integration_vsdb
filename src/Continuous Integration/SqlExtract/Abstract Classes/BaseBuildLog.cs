// <copyright file="BaseBuildLog.cs" company="Adam Nachman">
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
    using Microsoft.Build.Utilities;

    internal abstract class BaseBuildLog
    {
        #region Private Static Variables

        private static BaseBuildLog logger;

        #endregion Private Static Variables

        #region Static Constructors

        /// <summary>
        /// Initializes static members of the BaseBuildLog class
        /// </summary>
        static BaseBuildLog()
        {
            logger = new TraceLog();
        }

        /// <summary>
        /// Initializes a new instance of the BaseBuildLog class
        /// </summary>
        protected BaseBuildLog()
        {
        }

        #endregion Static Constructors

        #region Internal Static Methods

        internal static void SetUp(Task task)
        {
            if (task.BuildEngine == null)
            {
                logger = new TraceLog();
            }
            else
            {
                logger = new MSBuildLog(task.Log);
            }
        }

        internal static void Information(string message)
        {
            logger.LogInformation(message);
        }

        internal static void Information(string format, params object[] args)
        {
            logger.LogInformation(String.Format(format, args));
        }

        internal static void Warning(string message)
        {
            logger.LogWarning(message);
        }

        internal static void Warning(string format, params object[] args)
        {
            logger.LogWarning(String.Format(format, args));
        }

        internal static void Error(string message)
        {
            logger.LogError(message);
        }

        internal static void Error(string format, params object[] args)
        {
            logger.LogError(String.Format(format, args));
        }

        internal static bool ErrorFromException(Exception ex)
        {
            return logger.LogErrorFromException(ex);
        }

        #endregion Internal Static Methods

        #region Protected Abstract Methods

        protected abstract void LogInformation(string message);

        protected abstract void LogWarning(string message);

        protected abstract void LogError(string message);

        protected abstract bool LogErrorFromException(Exception ex);

        #endregion Protected Abstract Methods
    }
}
