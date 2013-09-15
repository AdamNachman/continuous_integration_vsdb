// <copyright file="ILogger.cs" company="Adam Nachman">
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
namespace SqlDeployment.Interfaces
{
    using System;

    /// <summary>
    /// A standard interface for a logger class
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Initializes the logger
        /// </summary>
        /// <param name="task">The task object with which to iitialize the looger</param>
        void Init(object task);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message text</param>
        void LogInformation(string message);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        void LogInformation(string format, params object[] args);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message text</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        void LogWarning(string format, params object[] args);

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message">The message text</param>
        void LogError(string message);

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="format">The format of the message</param>
        /// <param name="args">The arguments</param>
        void LogError(string format, params object[] args);

        /// <summary>
        /// Logs an error from an exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>False if unsuccessful</returns>
        bool LogErrorFromException(Exception ex);
    }
}
