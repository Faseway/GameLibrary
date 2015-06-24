using Faseway.GameLibrary;
using Faseway.GameLibrary.Components;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Scripting;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Faseway.GameLibrary.Scripting
{
    public class MissionScript : Script
    {
        // Properties
        /// <summary>
        /// Gets the content manager.
        /// </summary>
        protected ContentManager Content
        {
            get { return Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>(); }
        }

        // Constructor
        public MissionScript()
        {

        }

        // Methods
        public virtual void Start()
        {

        }
    }
}
