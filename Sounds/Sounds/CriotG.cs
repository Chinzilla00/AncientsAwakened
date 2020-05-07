using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace AvesMod.Sounds.Sounds
{
    public class CriotG : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance.State == SoundState.Playing)
                return null;
            soundInstance.Volume = volume * 1f;
            soundInstance.Pan = pan;
            soundInstance.Pitch = Main.rand.Next(-4, 4) * .05f;
            return soundInstance;
        }
    }
}