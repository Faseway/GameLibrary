using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Faseway.GameLibrary.Components;
using System;

namespace Faseway.GameLibrary.Audio
{
    public static class Audio2D
    {
        // Variables
        private static SoundEffect _effect;
        private static DateTime _last;

        // Methods
        public static void PlayEffect()
        {
            if (_effect == null)
            {
                _effect = Seed.Components.GetAndRequire<XnaReference>().GetAndRequire<ContentManager>().Load<SoundEffect>("Audio\\FX\\Ring");
                _last = DateTime.Now;
                SoundEffect.MasterVolume = 0.03f;
            }
            if ((DateTime.Now - _last).TotalSeconds >= 0.5)
            {
                _effect.Play();
                _last = DateTime.Now;
            }
        }
    }
}
