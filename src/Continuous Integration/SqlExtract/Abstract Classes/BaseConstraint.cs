namespace AdamNachman.Build.SqlExtract
{
    /// <summary>
    /// The base constraint class, from which other constraints will inherit
    /// </summary>
    public abstract class BaseConstraint : BaseDBObject
    {
        #region Constants and Fields

        /// <summary>
        /// Indicates that the parent object is a view
        /// </summary>
        private bool isOwnedByView;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the BaseConstraint class
        /// </summary>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includedrop">if set to <c>true</c> then include drop statements.</param>
        protected BaseConstraint(string parentName, string name, string ddl, bool includedrop)
            : this(SystemConstants.DefaultSchema, parentName, name, ddl, includedrop)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BaseConstraint class
        /// </summary>
        /// <param name="schema">The schema to which the object belongs</param>
        /// <param name="parentName">The parent of the object (table, etc)</param>
        /// <param name="name">The name of the constraint</param>
        /// <param name="ddl">The ddl of the object</param>
        /// <param name="includedrop">if set to <c>true</c> then include drop statements.</param>
        protected BaseConstraint(string schema, string parentName, string name, string ddl, bool includedrop)
            : base(schema, parentName, name, ddl, false, includedrop)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether or not the object is owned by a view
        /// </summary>
        public bool IsOwnedByView
        {
            get
            {
                return this.isOwnedByView;
            }

            set
            {
                this.isOwnedByView = value;
                this.Prefix = SystemConstants.SetQuotedIdentifiersOn + this.Prefix;
                this.Suffix = this.Suffix + SystemConstants.SetQuotedIdentifiersOff;
            }
        }
        #endregion
    }
}