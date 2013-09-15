// <copyright file="FileBuilder.cs" company="Adam Nachman">
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
    using System.IO;
    using System.Text;

    internal static class FileBuilder
    {
        private static string filepath = string.Empty;
        private static bool initialized = false;
        private static StringBuilder data = new StringBuilder();

        public static void Initialize()
        {
            if (data.Length > 0)
            {
                data = new StringBuilder();
            }
            initialized = true;
        }

        internal static void SetPath(string path)
        {
            filepath = path;
        }

        internal static void AddText(string text)
        {
            if (!initialized)
            {
                throw new Exception("The Filebuilder has not yet been initialized");
            }
            if (string.IsNullOrEmpty(filepath))
            {
                throw new Exception("The output path for the Filebuilder has not yet been configured");
            }
            data.AppendLine(text);
        }

        internal static void Flush()
        {
            WriteAll();
            if (data.Length > 0)
            {
                data = new StringBuilder();
            }
            initialized = false;
        }

        private static void WriteAll()
        {
            string fileName = filepath;

            using (FileStream fs = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(data.ToString().ToCharArray());
                    bw.Close();
                }

                fs.Close();
            }
        }
    }
}
