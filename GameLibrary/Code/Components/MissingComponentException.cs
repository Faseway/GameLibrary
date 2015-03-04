using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Components
{
    [Serializable]
    public class MissingComponentException : Exception
    {
        // Properties
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        public Type SourceType { get; private set; }
        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        public Type ComponentType { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Components.MissingComponentException"/> class.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type componentType)
            : base(string.Format("Required component {0} doesn't exist inside the component registry.", componentType))
        {
            ComponentType = componentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Components.MissingComponentException"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type sourceType, Type componentType)
            : base(string.Format("{0} requires a {1} component registered inside the component registry.", sourceType.Name, componentType.Name))
        {
            SourceType = sourceType;
            ComponentType = componentType;
        }
    }
}
