using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace AAMod.Sounds.Sounds
{
    public class Zerohit2 : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance.State == SoundState.Playing)
                return null;
            soundInstance.Volume = volume * 1f;
            return soundInstance;
        }
    }
}