// <copyright file="BaseDBObject.cs" company="Adam Nachman">
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
    /// The abstract BaseDBObject class
    /// </summary>
    public abstract class BaseDBObject : IDBObject
    {
        #region Private Variables

        /// <summary>
        /// The schema to which the object belongs
        /// </summary>
        private string schema;

        /// <summary>
        /// The name of the parent object
        /// </summary>
        private string parentName;

        /// <summary>
        /// The name of the object
        /// </summary>
        private string name;

        /// <summary>
        /// The prefix for the ddl
        /// </summary>
        private string ddlPrefix;

        /// <summary>
        /// The ddl of the object
        /// </summary>
        private string ddlBody;

        /// <summary>
        /// The suffix for the ddl
        /// </summary>
        private string ddlSuffix;

        /// <summary>
        /// A value indicating whether or not the object has been processed
        /// </summary>
        private bool complete;

        /// <summary>
        /// A value indicating what this object depends on
        /// </summary>
        private string markComplete;

        /// <summary>
        /// A value indicating whether or not this object already exists in the target schema
        /// </summary>
        private bool preExists;


        /// <summary>
        /// A value indicating whether or not this object should include drop statements in it's execution
        /// </summary>
        private bool generateDropStatements;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BaseDBObject class
        /// </summary>
        /// <param name="parentName">The name of the parent object</param>
        /// <param name="ddl">The ddl of the object</param>
        protected BaseDBObject(string parentName, string ddl)
            : this(SystemConstants.DefaultSchema, string.Empty, parentName, ddl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseDBObject class
        /// </summary>
        /// <param name="parentName">The name of the parent object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        protected BaseDBObject(string parentName, string name, string ddl)
            : this(SystemConstants.DefaultSchema, parentName, name, ddl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseDBObject class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The name of the parent object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        protected BaseDBObject(string schema, string parentName, string name, string ddl)
            : this(string.IsNullOrEmpty(schema) ? SystemConstants.DefaultSchema : schema, parentName, name, ddl, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseDBObject class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The name of the parent object</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="preExists">Indicates whether or not the object exists on the target schema</param>
        protected BaseDBObject(string schema, string parentName, string name, string ddl, bool preExists, bool includeDrop = false)
        {
            this.schema = schema;
            this.parentName = parentName;
            this.name = name;
            this.complete = false;
            this.preExists = preExists;
            this.generateDropStatements = includeDrop;
            this.LoadDdl(ddl);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the schema to which the object belongs
        /// </summary>
        public virtual string Schema
        {
            get
            {
                return this.schema;
            }
        }

        /// <summary>
        /// Gets the name of the objects parent
        /// </summary>
        public virtual string ParentName
        {
            get
            {
                return this.parentName;
            }
        }

        /// <summary>
        /// Gets the name of the object
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the Ddl that defines the object
        /// </summary>
        public virtual string Ddl
        {
            get
            {
                return this.ddlBody;
            }
        }

        /// <summary>
        /// Gets the complete Ddl, including prefix and suffix
        /// </summary>
        public virtual string CompleteDdl
        {
            get
            {
                return string.Concat(this.ddlPrefix, this.ddlBody, this.ddlSuffix);
            }
        }



        /// <summary>
        /// Gets a value indicating whether or not to include drop statements.
        /// </summary>
        /// <value>if <c>true</c> then include drop statements; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IncludeDropStatements
        {
            get
            {
                return this.generateDropStatements;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the object has been processed
        /// </summary>
        public virtual bool Complete
        {
            get
            {
                return this.complete;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating what this object depends on
        /// </summary>
        public virtual string DependsOn
        {
            get
            {
                return this.markComplete;
            }

            set
            {
                this.markComplete = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the object is expected to exist in the target schema
        /// </summary>
        public virtual bool PreExists
        {
            get
            {
                return this.preExists;
            }
            set
            {
                this.preExists = value;
            }
        }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets or sets the prefix for the ddl
        /// </summary>
        protected string Prefix
        {
            get
            {
                return this.ddlPrefix;
            }

            set
            {
                this.ddlPrefix = value;
            }
        }

        /// <summary>
        /// Gets or sets the body of the class
        /// </summary>
        protected string Body
        {
            get
            {
                return this.ddlBody;
            }

            set
            {
                this.ddlBody = value;
            }
        }

        /// <summary>
        /// Gets or sets the suffix for the ddl
        /// </summary>
        protected string Suffix
        {
            get
            {
                return this.ddlSuffix;
            }

            set
            {
                this.ddlSuffix = value;
            }
        }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Marks processing complete
        /// </summary>
        public void MarkComplete()
        {
            this.complete = true;
        }

        #endregion Public Methods

        #region Public Overridden Methods

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns>Schema.object, or just the object</returns>
        public override string ToString()
        {
            if (this.parentName.Length > 0)
            {
                return string.Concat(this.parentName, ".", this.name);
            }
            else
            {
                return this.name;
            }
        }

        #endregion Public Overridden Methods

        #region Protected Abstract Methods

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected abstract void LoadDdl(string ddl);

        #endregion Protected Abstract Methods
    }
}
