// <copyright file="Utils.cs" company="Adam Nachman">
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
namespace UnitTests
{
    using System.IO;
    using System.Reflection;

    public static class Utils
    {
        /// <summary>
        /// The currently running assembly. Static, cause it should only ever get loaded once
        /// </summary>
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        public static string GetResourceString(string name)
        {
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(string.Concat("UnitTests.ScriptResources.", name))))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }
    }
}
