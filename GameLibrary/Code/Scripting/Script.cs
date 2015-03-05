using Faseway.GameLibrary;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;

namespace Faseway.GameLibrary.Scripting
{
    public abstract class Script
    {
        // Properties
        public string Name { get { return GetType().Name; } }

        // Constructor
        public Script()
        {
        }
    }
}
