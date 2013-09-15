// <copyright file="ScriptExecutorEventArgs.cs" company="Adam Nachman">
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
namespace SqlDeployment
{
    using System;

    public class ScriptExecutorEventArgs : EventArgs
    {
        #region Private Variables

        private EventMessageCode eventCode;
        private string eventMessage;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScriptExecutorEventArgs class
        /// </summary>
        /// <param name="eventCode">The event code</param>
        /// <param name="eventMessage">The event message</param>
        public ScriptExecutorEventArgs(EventMessageCode eventCode, string eventMessage)
        {
            this.eventCode = eventCode;
            this.eventMessage = eventMessage;
        }

        #endregion Constructors

        #region Public Enums

        /// <summary>
        /// The various message types in use
        /// </summary>
        public enum EventMessageCode
        {
            /// <summary>
            /// Script is executing
            /// </summary>
            ScriptExecution = 0,

            /// <summary>
            /// Script execution is complete
            /// </summary>
            ExecutionComplete = 1,

            /// <summary>
            /// An informational message from the connection
            /// </summary>
            SqlInfoMessage = 2,

            /// <summary>
            /// Stopping execution by request of the consuming class
            /// </summary>
            ExecutionStoppingByRequest = 3,

            /// <summary>
            /// Execution stopped by request of the consuming class
            /// </summary>
            ExecutionStoppedByRequest = 4,

            /// <summary>
            /// The count of the objects in the script
            /// </summary>
            ObjectCount = 100000,

            /// <summary>
            /// The current progress
            /// </summary>
            Progress = 100010,
        }

        #endregion Public Enums

        #region Public Methods

        /// <summary>
        /// Gets or sets the message associated with the event
        /// </summary>
        public string EventMessage
        {
            get
            {
                return this.eventMessage;
            }

            set
            {
                this.eventMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets the Event Code associated with the event
        /// </summary>
        public EventMessageCode EventCode
        {
            get
            {
                return this.eventCode;
            }

            set
            {
                this.eventCode = value;
            }
        }

        #endregion Public Methods
    }
}
