using System.Linq;
using System.Collections.Generic;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Logging;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faseway.GameLibrary.Components
{
    /// <summary>
    /// Provides access to components of the Xna Framework.
    /// </summary>
    public class XnaReference : IComponent
    {
        // Properties
        /// <summary>
        /// Gets a collection of all references.
        /// </summary>
        public List<object> References { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.XnaReference"/> class.
        /// </summary>
        public XnaReference()
        {
            References = new List<object>();
        }

        // Methods
        /// <summary>
        /// Links a specified reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void Link(object reference)
        {
            if (References.Find(match => match.GetType().Name == reference.GetType().Name) != null)
            {
                Logger.Log("Reference {0} has already been linked", reference.GetType().Name);
            }
            else
            {
                References.Add(reference);

                Logger.Log("Reference {0} has been linked", reference.GetType().Name);
            }
        }

        /// <summary>
        /// Unlinks a specified reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void Unlink(object reference)
        {
            References.Remove(reference);

            Logger.Log("Reference {0} has been unlinked", reference.GetType().Name);
        }

        /// <summary>
        /// Unlinks a specified reference abstraction.
        /// </summary>
        public void Unlink<T>() where T : class
        {
            Unlink(Get<T>());
        }

        /// <summary>
        /// Gets a reference by a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the reference.</typeparam>
        public T Get<T>() where T : class
        {
            var reference = References.Find(refr => refr.GetType() == typeof(T));
            if (reference != null)
            {
                return (T)reference;
            }

            Logger.Log("Reference {0} does not exist inside the reference registry", typeof(T).Name);

            return default(T);
        }

        /// <summary>
        /// Requires a specified reference.
        /// </summary>
        /// <typeparam name="T">The reference type.</typeparam>
        public T GetAndRequire<T>() where T : class
        {
            var reference = Get<T>();
            if (object.Equals(reference, null))
            {
                throw new MissingReferenceException(typeof(T));
            }

            return reference;
        }
    }
}
