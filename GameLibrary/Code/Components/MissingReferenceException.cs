using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Components
{
    public class MissingReferenceException : Exception
    {
        // Properties
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        public Type SourceType { get; private set; }
        /// <summary>
        /// Gets the type of the reference.
        /// </summary>
        public Type ReferenceType { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Components.MissingReferenceException"/> class.
        /// </summary>
        /// <param name="componentType">Type of the reference.</param>
        public MissingReferenceException(Type referenceType)
            : base(string.Format("Required reference {0} doesn't exist inside the reference registry", referenceType))
        {
            ReferenceType = referenceType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Components.MissingReferenceException"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="componentType">Type of the reference.</param>
        public MissingReferenceException(Type sourceType, Type referenceType)
            : base(string.Format("{0} requires a {1} reference registered inside the reference registry", sourceType.Name, referenceType.Name))
        {
            SourceType = sourceType;
            ReferenceType = referenceType;
        }
    }
}
