// <copyright file="SQLExtractParserTests.cs" company="Adam Nachman">
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
    using AdamNachman.Build.SqlExtract;
    using AdamNachman.Build.SqlExtract.Parsers;
    using NUnit.Framework;

    [TestFixture]
    public class SQLExtractParserTests
    {
        #region Table Objects

        [Test]
        public void ShouldGetTableNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TableNoSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        [Test]
        public void ShouldGetTableStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TableStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        [Test]
        public void ShouldGetTableUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableUnBracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TableUnBracketedSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        [Test]
        public void ShouldGetTableUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableUnBracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TableUnBracketedName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        [Test]
        public void ShouldGetTableDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TableDifferentSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        [Test]
        public void ShouldGetTableCommentPrefix()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TableCommentPrefix.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TableCommentPrefix", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("TABLE", result.TypeName);
        }

        #endregion Table Objects

        #region  View Objects

        [Test]
        public void ShouldGetViewNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ViewNoSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        [Test]
        public void ShouldGetViewStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ViewStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        [Test]
        public void ShouldGetViewUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewUnBracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ViewUnBracketedSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        [Test]
        public void ShouldGetViewUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewUnBracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ViewUnBracketedName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        [Test]
        public void ShouldGetViewDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ViewDifferentSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        [Test]
        public void ShouldGetViewCommentPrefix()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ViewCommentPrefix.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ViewCommentPrefix", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("VIEW", result.TypeName);
        }

        #endregion View Objects

        #region Function Objects

        [Test]
        public void ShouldGetFunctionNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("FunctionNoSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("FunctionStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionWithSpaces()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionWithSpaces.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("Function With Spaces", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionUnBracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("FunctionUnBracketedSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionUnBracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("FunctionUnBracketedName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("FunctionDifferentSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionCommentPrefix()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionCommentPrefix.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("FunctionCommentPrefix", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetFunctionTableValueOnDiffSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FunctionTableValueOnDiffSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("FunctionTableValueOnDiffSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FUNCTION", result.TypeName);
        }

        #endregion Function Objects

        #region Unique Constrants

        [Test]
        public void ShouldGetUniqueConstraintDboSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("UniqueConstraintDboSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual("MyTable", result.Parent);
            Assert.AreEqual("UNIQUE", result.TypeName);
        }

        [Test]
        public void ShouldGetUniqueConstraintDiffSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("UniqueConstraintDiffSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual("MyTable", result.Parent);
            Assert.AreEqual("UNIQUE", result.TypeName);
        }

        [Test]
        public void ShouldGetUniqueConstraintNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("UniqueConstraintNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual("MyTable", result.Parent);
            Assert.AreEqual("UNIQUE", result.TypeName);
        }

        #endregion Unique Constrants

        #region Schema Objects

        [Test]
        public void ShouldGetSchemaStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("SchemaStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("SchemaStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("SCHEMA", result.TypeName);
        }

        [Test]
        public void ShouldGetSchemaWithOwner()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("SchemaWithOwner.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("SchemaWithOwner", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("SCHEMA", result.TypeName);
        }

        #endregion Schema Objects

        #region Route Objects

        [Test]
        public void ShouldGetRouteStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("RouteStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("RouteStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("ROUTE", result.TypeName);
        }

        [Test]
        public void ShouldGetRouteWithOwner()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("RouteWithOwner.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("RouteWithOwner", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("ROUTE", result.TypeName);
        }

        #endregion Route Objects

        #region Database Role Objects

        [Test]
        public void ShouldGetDatabaseRoleStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DatabaseRoleStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("DatabaseRoleStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("ROLE", result.TypeName);
        }

        [Test]
        public void ShouldGetDatabaseRoleWithOwner()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DatabaseRoleWithOwner.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("DatabaseRoleWithOwner", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("ROLE", result.TypeName);
        }

        [Test]
        public void ShouldGetDatabaseRoleWithOwnerNoWhitespace()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DatabaseRoleWithOwnerNoWhitespace.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("DatabaseRoleWithOwnerNoWhitespace", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("ROLE", result.TypeName);
        }

        #endregion Database Role Objects

        #region Stored Procedure Objects

        [Test]
        public void ShouldGetStoredProcedureNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ProcedureNoSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }

        [Test]
        public void ShouldGetStoredProcedureStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ProcedureStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }

        [Test]
        public void ShouldGetStoredProcedureUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureUnBracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ProcedureUnBracketedSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }

        [Test]
        public void ShouldGetStoredProcedureUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureUnBracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ProcedureUnBracketedName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }
        
        [Test]
        public void ShouldGetStoredProcedureDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ProcedureDifferentSchema", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }
        
        [Test]
        public void ShouldGetStoredProcedureWithSpaces()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureWithSpaces.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("Procedure With Spaces", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }

        [Test]
        public void ShouldGetStoredProcedureCommentPrefix()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureCommentPrefix.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ProcedureCommentPrefix", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROCEDURE", result.TypeName);
        }

        [Test]
        public void ShouldGetStoredProcedureShortTYpeName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ProcedureShortTypeName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ProcedureShortTypeName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PROC", result.TypeName);
        }

        #endregion Stored Procedure Objects

        #region Default Constraint Objects

        [Test]
        public void ShouldGetDefaultConstraintNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DefaultConstrantNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("DefaultConstrantNoSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("DEFAULT", result.TypeName);
        }

        [Test]
        public void ShouldGetDefaultConstraintStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DefaultConstrantStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("DefaultConstrantStandard", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("DEFAULT", result.TypeName);
        }

        [Test]
        public void ShouldGetDefaultConstraintUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DefaultConstrantUbracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("DefaultConstrantUbracketedSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("DEFAULT", result.TypeName);
        }

        [Test]
        public void ShouldGetDefaultConstraintUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DefaultConstrantUbracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("DefaultConstrantUbracketedName", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("DEFAULT", result.TypeName);
        }

        [Test]
        public void ShouldGetDefaultConstraintDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("DefaultConstrantDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("DefaultConstrantDifferentSchema", result.Name);
            Assert.AreEqual("TableTwo", result.Parent);
            Assert.AreEqual("DEFAULT", result.TypeName);
        }

        #endregion Default Constraint Objects

        #region Foreign Key Constraint Objects

        [Test]
        public void ShouldGetForeignKeyConstraintNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ForeignKeyNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ForeignKeyNoSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("FOREIGN KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetForeignKeyConstraintStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ForeignKeyStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ForeignKeyStandard", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("FOREIGN KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetForeignKeyConstraintUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ForeignKeyUnbracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("ForeignKeyUnbracketedSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("FOREIGN KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetForeignKeyConstraintUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ForeignKeyUnbracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ForeignKeyUnbracketedName", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("FOREIGN KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetForeignKeyConstraintDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ForeignKeyDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("ForeignKeyDifferentSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("FOREIGN KEY", result.TypeName);
        }

        #endregion Foreign Key Constraint Objects

        #region Primary Key Constraint Objects

        [Test]
        public void ShouldGetPrimaryKeyConstraintNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PrimaryKeyNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("PrimaryKeyNoSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("PRIMARY KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetPrimaryKeyConstraintStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PrimaryKeyStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("PrimaryKeyStandard", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("PRIMARY KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetPrimaryKeyConstraintUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PrimaryKeyUnbracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("PrimaryKeyUnbracketedSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("PRIMARY KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetPrimaryKeyConstraintUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PrimaryKeyUnbracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("PrimaryKeyUnbracketedName", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("PRIMARY KEY", result.TypeName);
        }

        [Test]
        public void ShouldGetPrimaryKeyConstraintDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PrimaryKeyDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("PrimaryKeyDifferentSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("PRIMARY KEY", result.TypeName);
        }

        #endregion Primary Key Constraint Objects

        #region Check Constraint Objects

        [Test]
        public void ShouldGetCheckConstraintNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("CheckConstraintNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("CheckConstraintNoSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("CHECK", result.TypeName);
        }

        [Test]
        public void ShouldGetCheckConstraintStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("CheckConstraintStandard.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("CheckConstraintStandard", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("CHECK", result.TypeName);
        }

        [Test]
        public void ShouldGetCheckConstraintUnBracketedSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("CheckConstraintUnbracketedSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);
            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("CheckConstraintUnbracketedSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("CHECK", result.TypeName);
        }

        [Test]
        public void ShouldGetCheckConstraintUnBracketedName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("CheckConstraintUnbracketedName.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("CheckConstraintUnbracketedName", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("CHECK", result.TypeName);
        }

        [Test]
        public void ShouldGetCheckConstraintDifferentSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("CheckConstraintDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("CheckConstraintDifferentSchema", result.Name);
            Assert.AreEqual("TableOne", result.Parent);
            Assert.AreEqual("CHECK", result.TypeName);
        }

        #endregion Check Constraint Objects

        #region Service Objects

        [Test]
        public void ShouldGetServiceStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ServiceStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("ServiceStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("SERVICE", result.TypeName);
        }

        [Test]
        public void ShouldGetServiceWithOwner()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("ServiceWithOwner.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("ServiceWithOwner", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("SERVICE", result.TypeName);
        }

        #endregion Schema Objects

        #region Filegroup Objects

        [Test]
        public void ShouldGetFilegroup()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FileGroup.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("MyFilegroup", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FILEGROUP", result.TypeName);
        }

        [Test]
        public void ShouldGetFilegroupWithUnderscores()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FileGroupWithUnderscores.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("My_Filegroup", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FILEGROUP", result.TypeName);
        }

        [Test]
        public void ShouldGetFilegroupWithSpaces()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FileGroupWithSpaces.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("My Filegroup", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FILEGROUP", result.TypeName);
        }

        [Test]
        public void ShouldGetFilegroupUnbracketed()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FileGroupUnbracketed.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("MyFileGroup", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FILEGROUP", result.TypeName);
        }

        #endregion Filegroup Objects

        #region Fulltext Objects

        [Test]
        public void ShouldGetFulltextCatalog()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FulltextCatalogStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("FulltextCatalogStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FULLTEXT CATALOG", result.TypeName);
        }

        [Test]
        public void ShouldGetFulltextIndexStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("FulltextIndexStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsTrue(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TableSixFulltextIndexStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("FULLTEXT INDEX", result.TypeName);
        }

        #endregion Fulltext Objects

        #region Message Objects

        [Test]
        public void ShouldGetMessageTypeStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("MessageTypeStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("MessageTypeStandard", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("MESSAGE TYPE", result.TypeName);
        }

        [Test]
        public void ShouldGetMessageTypeXMLName()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("MessageTypeXMLName.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual(@"///Adventure1-Wo1r_ks.com/Expenses/SubmitExpense", result.Name);
            Assert.AreEqual("MySchema", result.Parent);
            Assert.AreEqual("MESSAGE TYPE", result.TypeName);
        }

        #endregion Message Objects

        #region Queue Objects

        [Test]
        public void ShouldGetQueueStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("QueueStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("QueueStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("QUEUE", result.TypeName);
        }

        #endregion Queue Objects

        #region Partition Objects

        [Test]
        public void ShouldGetPartitionFunctionUnbracketed()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PartitionFunctionUnbracketed.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("PartitionUnbracketed", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PARTITION FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetPartitionFunctionStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PartitionFunctionStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("PartitionFunctionStandard", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PARTITION FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetPartitionFunctionWithSpaces()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PartitionFunctionWithSpaces.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("Partition Function With Spaces", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("PARTITION FUNCTION", result.TypeName);
        }

        [Test]
        public void ShouldGetPartitionSchemeStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("PartitionSchemeStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("PartitionSchemeStandard", result.Name);
            Assert.AreEqual("PartitionFunctionStandard", result.Parent);
            Assert.AreEqual("PARTITION SCHEME", result.TypeName);
        }

        #endregion Partition Objects

        #region Trigger Objects

        [Test]
        public void ShouldGetTriggerNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TriggerNoSchema.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TriggerNoSchema", result.Name);
            Assert.AreEqual("TriggerTable", result.Parent);
            Assert.AreEqual("TRIGGER", result.TypeName);
        }

        [Test]
        public void ShouldGetTriggerSchemaOnTrigger()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TriggerSchemaOnTrigger.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TriggerSchemaOnTrigger", result.Name);
            Assert.AreEqual("TriggerTable", result.Parent);
            Assert.AreEqual("TRIGGER", result.TypeName);
        }

        [Test]
        public void ShouldGetTriggerSchemaOnTable()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TriggerSchemaOnTable.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TriggerSchemaOnTable", result.Name);
            Assert.AreEqual("TriggerTable", result.Parent);
            Assert.AreEqual("TRIGGER", result.TypeName);
        }

        [Test]
        public void ShouldGetTriggerSchemaStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("TriggerSchemaStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TriggerSchemaStandard", result.Name);
            Assert.AreEqual("TriggerTable", result.Parent);
            Assert.AreEqual("TRIGGER", result.TypeName);
        }

        #endregion Trigger Objects

        #region Assembly Objects

        [Test]
        public void ShouldGetAssemblyStandard()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("AssemblyStandard.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("AdamNachman.SqClr", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("ASSEMBLY", result.TypeName);
        }

        [Test]
        public void ShouldGetAssemblyWithOwner()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("AssemblyWithOwner.sql"));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual(string.Empty, result.SchemaName);
            Assert.AreEqual("AdamNachman.SqClr", result.Name);
            Assert.AreEqual("dbo", result.Parent);
            Assert.AreEqual("ASSEMBLY", result.TypeName);
        }

        #endregion Assembly Objects

        #region Synonym Objects

        [Test]
        public void ShouldGetSynonymDboSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("SynonymOnDbo.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("SYNONYM", result.TypeName);
        }

        [Test]
        public void ShouldGetSynonymNoSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("SynonymOnNoSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("dbo", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("SYNONYM", result.TypeName);
        }

        [Test]
        public void ShouldGetSynonymDiffSchema()
        {
            ParseResult result = ScriptMatcher.ParseScript(Utils.GetResourceString("SynonymDifferentSchema.sql"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Matched);
            Assert.IsFalse(result.IsFullText);

            Assert.AreEqual("MySchema", result.SchemaName);
            Assert.AreEqual("TheName", result.Name);
            Assert.AreEqual(string.Empty, result.Parent);
            Assert.AreEqual("SYNONYM", result.TypeName);
        }


        #endregion Synonym Objects
    }
}