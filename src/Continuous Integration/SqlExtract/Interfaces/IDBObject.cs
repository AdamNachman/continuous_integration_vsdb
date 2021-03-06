﻿// <copyright file="IDBObject.cs" company="Adam Nachman">
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
    /// The interface implemented by all DBObject classes
    /// </summary>
    public interface IDBObject
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether or not the processing of the object is complete
        /// </summary>
        bool Complete
        {
            get;
        }

        /// <summary>
        /// Gets the name of the schema
        /// </summary>
        string Schema
        {
            get;
        }

        /// <summary>
        /// Gets the name of the parent object
        /// </summary>
        string ParentName
        {
            get;
        }

        /// <summary>
        /// Gets the name of the object
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the raw ddl of the object
        /// </summary>
        string Ddl
        {
            get;
        }

        /// <summary>
        /// Gets the complete ddl of the object, including prefixes and suffixes
        /// </summary>
        string CompleteDdl
        {
            get;
        }

        /// <summary>
        /// Gets or sets the object on which this object depends
        /// </summary>
        string DependsOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the object exists in the target schema
        /// </summary>
        bool PreExists
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Marks the processing of the object complete
        /// </summary>
        void MarkComplete();

        #endregion Methods
    }
}
