// <copyright file="AssembliesToExtract.cs" company="Adam Nachman">
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
    using System.Collections.Generic;

    /// <summary>
    /// Represents a collection of assemblies to extract
    /// </summary>
    [Serializable]
    public class AssembliesToExtract
    {
        public AssembliesToExtract()
        {
        }

        public List<AssemblyToExtract> ConfiguredAssemblies { get; set; }
    }
}