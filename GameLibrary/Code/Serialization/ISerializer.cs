using System;
using System.IO;
using System.Text;

namespace Faseway.GameLibrary.Serialization
{
    /// <summary>
    /// Abstracted the serialization process.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the given <paramref name="instance"/> into the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="stream">The stream.</param>
        void Serialize(object instance, Stream stream);
        /// <summary>
        /// Deserializes the instance of the given <paramref name="type"/> from the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        object Deserialize(Type type, Stream stream);
    }
}
