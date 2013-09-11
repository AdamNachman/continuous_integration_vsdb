// <copyright file="BaseLog.cs" company="Adam Nachman">
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

    /// <summary>
    /// A default logger
    /// </summary>
    internal class BaseLog : SqlDeployment.Interfaces.ILogger
    {
        #region Private Static Variables

        /// <summary>
        /// The static ILogger class
        /// </summary>
        private static SqlDeployment.Interfaces.ILogger logger;

        #endregion Private Static Variables

        /// <summary>
        /// Initializes a new instance of the BaseLog class
        /// </summary>
        public BaseLog()
            : this(new TraceLog())
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseLog class
        /// </summary>
        /// <param name="buildLogger">The logger object to use</param>
        public BaseLog(object buildLogger)
        {
            logger = (SqlDeployment.Interfaces.ILogger)buildLogger;
        }

        #region Public Methods

        /// <summary>
        /// Initializes the logger
        /// </summary>
        /// <param name="buildTask">The task object with which to iitialize the looger</param>
        public void Init(object buildTask)
        {
            if (buildTask is Task)
            {
                Task task = (Task)buildTask;
                if (task.BuildEngine == null)
                {
                    logger = new TraceLog();
                }
                else
                {
                    logger = new BuildLog();
                    logger.Init(task.Log);
                }
            }
            else
            {
                logger = new TraceLog();
            }
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogInformation(string format, params object[] args)
        {
            logger.LogInformation(string.Format(format, args));
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogWarning(string format, params object[] args)
        {
            logger.LogWarning(string.Format(format, args));
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogError(string message)
        {
            logger.LogError(message);
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogError(string format, params object[] args)
        {
            logger.LogError(string.Format(format, args));
        }

        /// <summary>
        /// Logs an error from an exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>False if unsuccessful</returns>
        public bool LogErrorFromException(Exception ex)
        {
            return logger.LogErrorFromException(ex);
        }

        #endregion Public Methods
    }
}