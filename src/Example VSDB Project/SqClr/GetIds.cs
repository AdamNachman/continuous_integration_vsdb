// <copyright file="GetIds.cs" company="Adam Nachman">
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
namespace AdamNachman.SqlClr
{
    using System;
    using System.Collections;
    using System.Data.SqlTypes;
    using Microsoft.SqlServer.Server;

    /// <summary>
    /// Extends the user defined functions class to include a function to split an array and return an (unsorted) table
    /// </summary>
    public class Selection
    {
        /// <summary>
        /// Gets the ids.
        /// </summary>
        /// <param name="list">The list of strings.</param>
        /// <param name="delim">The delimiter.</param>
        /// <returns>An IEnumerable list of ids</returns>
        [SqlFunction(IsDeterministic = true, FillRowMethodName = "FillRows", TableDefinition = "Id int ")]
        public static IEnumerable GetIds([SqlFacet(MaxSize = -1)]SqlString list, [SqlFacet(MaxSize = 1)] SqlChars delim)
        {
            if (string.IsNullOrEmpty(list.ToString().Trim()) || delim.IsNull || delim.Length < 1)
            {
                yield break;
            }

            string[] values = list.Value.Split(delim.Value);

            int id;
            foreach (string value in values)
            {
                if (int.TryParse(value, out id))
                {
                    yield return id;
                }
            }
        }

        /// <summary>
        /// Fills the rows.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="id">The parsed id.</param>
        public static void FillRows(object obj, out SqlInt32 id)
        {
            id = (int)obj;
        }
    }
}

