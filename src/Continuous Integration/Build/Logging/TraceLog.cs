// <copyright file="TraceLog.cs" company="Adam Nachman">
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
    using System.Diagnostics;

    /// <summary>
    /// A trace logger
    /// </summary>
    internal class TraceLog : SqlDeployment.Interfaces.ILogger
    {
        #region Protected Methods

        /// <summary>
        /// Initializes the logger
        /// </summary>
        /// <param name="logger">The task object with which to iitialize the looger</param>
        public void Init(object logger)
        {
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogInformation(string message)
        {
            Trace.TraceInformation(message);
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogInformation(string format, params object[] args)
        {
            Trace.TraceInformation(string.Format(format, args));
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogWarning(string format, params object[] args)
        {
            Trace.TraceWarning(string.Format(format, args));
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The message text</param>
        public void LogError(string message)
        {
            Trace.TraceError(message);
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        public void LogError(string format, params object[] args)
        {
            Trace.TraceError(string.Format(format, args));
        }

        /// <summary>
        /// Logs an error from an exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>False if unsuccessful</returns>
        public bool LogErrorFromException(Exception ex)
        {
            return true;
        }

        #endregion Protected Methods
    }
}
