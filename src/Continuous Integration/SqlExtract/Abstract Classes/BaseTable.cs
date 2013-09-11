// <copyright file="BaseTable.cs" company="Adam Nachman">
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
    using System.Collections.Generic;

    /// <summary>
    /// The abstract BaseTable class
    /// </summary>
    public abstract class BaseTable : BaseDBObject
    {
        #region Private Variables

        /// <summary>
        /// The clustered constraints associated with the object
        /// </summary>
        private List<ClusteredConstraint> clusteredConstraints;

        /// <summary>
        /// The constraints associated with the object
        /// </summary>
        private List<BaseConstraint> constraints;

        #endregion Private Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BaseTable class
        /// </summary>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="preExists">Indicates whether or not the object exists in the target schema</param>
        protected BaseTable(string name, string ddl, bool preExists)
            : this(SystemConstants.DefaultSchema, name, ddl, preExists)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseTable class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="name">The name of the object</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="preExists">Indicates whether or not the object exists in the target schema</param>
        protected BaseTable(string schema, string name, string ddl, bool preExists)
            : base(schema, string.Empty, name, ddl, preExists)
        {
            this.clusteredConstraints = new List<ClusteredConstraint>();
            this.constraints = new List<BaseConstraint>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the IEnumerable collection of clustered constraints
        /// </summary>
        internal ClusteredConstraint Clustered
        {
            get
            {
                foreach (ClusteredConstraint constraint in this.clusteredConstraints)
                {
                    if (constraint.Clustered)
                    {
                        return constraint;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the IEnumerable collection of non clustered constraints
        /// </summary>
        internal IEnumerable<ClusteredConstraint> NonClustered
        {
            get
            {
                foreach (ClusteredConstraint constraint in this.clusteredConstraints)
                {
                    if (!constraint.Clustered)
                    {
                        yield return constraint;
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets the IEnumerable collection of non clustered constraints
        /// </summary>
        internal IEnumerable<BaseConstraint> OtherConstraints
        {
            get
            {
                foreach (BaseConstraint constraint in this.constraints)
                {
                    yield return constraint;
                }
            }
        }

        #endregion Properties

        #region Internal Abstract Methods

        /// <summary>
        /// Links a constraint
        /// </summary>
        /// <param name="constraint">The constraint to link</param>
        internal abstract void LinkConstraint(BaseConstraint constraint);

        #endregion Internal Abstract Methods

        #region Protected Methods

        /// <summary>
        /// Loads the ddl
        /// </summary>
        /// <param name="ddl">The ddl to load</param>
        protected override void LoadDdl(string ddl)
        {
            this.Prefix = SystemConstants.SetQuotedIdentifiersOn;
            this.Body = ddl;
            this.Suffix = SystemConstants.BatchEnd;
        }

        /// <summary>
        /// Adds a constraint to the constraints collection for this object
        /// </summary>
        /// <param name="constraint">The constraint to add</param>
        protected void AddConstraint(BaseConstraint constraint)
        {
            ClusteredConstraint cc = constraint as ClusteredConstraint;
            if (cc != null)
            {
                this.clusteredConstraints.Add(cc);
            }
            else
            {
                this.constraints.Add(constraint);
            }
        }

        #endregion Protected Methods
    }
}