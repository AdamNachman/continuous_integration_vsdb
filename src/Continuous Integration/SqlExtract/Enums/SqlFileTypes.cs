// <copyright file="SqlFileTypes.cs" company="Adam Nachman">
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
namespace AdamNachman.Build.SqlExtract.Enums
{
    /// <summary>
    /// The types of sql files currently supported by the extraction utility
    /// </summary>
    internal enum SqlFileTypes
    {
        /// <summary>
        /// Table schema objects
        /// </summary>
        Tables,

        /// <summary>
        /// View schema objects
        /// </summary>
        Views,

        /// <summary>
        /// Function schema objects
        /// </summary>
        Functions,

        /// <summary>
        /// Procedure schema objects
        /// </summary>
        Procedures,

        /// <summary>
        /// Clustered constraint schema objects
        /// </summary>
        ClusteredConstraints,

        /// <summary>
        /// Clr Assembly  objects
        /// </summary>
        ClrAssemblies,

        /// <summary>
        /// Constraint schema objects
        /// </summary>
        Constraints,

        /// <summary>
        /// Trigger schema objects
        /// </summary>
        Triggers,

        /// <summary>
        /// Circular references among schema objects
        /// </summary>
        Circular,

        /// <summary>
        /// Schema schema objects
        /// </summary>
        Schemas,

        /// <summary>
        /// Role schema objects
        /// </summary>
        Roles,

        /// <summary>
        /// Filegroup schema objects
        /// </summary>
        Filegroups,

        /// <summary>
        /// Route schema objects
        /// </summary>
        Routes,

        /// <summary>
        /// Message schema objects
        /// </summary>
        Messages,

        /// <summary>
        /// Contract schema objects
        /// </summary>
        Contracts,

        /// <summary>
        /// BrokerQueue schema objects
        /// </summary>
        BrokerQueue,

        /// <summary>
        /// Service schema objects
        /// </summary>
        Services,

        /// <summary>
        /// FullTextCatalog schema objects
        /// </summary>
        FullTextCatalogs,

        /// <summary>
        /// FullTextIndexes schema objects
        /// </summary>
        FullTextIndexes,

        /// <summary>
        /// PartitionFunctions schema objects
        /// </summary>
        PartitionFunctions,

        /// <summary>
        /// PartitionSchemes schema objects
        /// </summary>
        PartitionSchemes,

        /// <summary>
        /// Synonym objects
        /// </summary>
        Synonyms
    }
}
