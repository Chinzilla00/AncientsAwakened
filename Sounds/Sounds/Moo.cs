using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Sounds.Sounds
{
    public class Moo : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
			soundInstance = sound.CreateInstance();		
            soundInstance.Volume = volume * 1f;
            soundInstance.Pan = pan;
            soundInstance.Pitch = (float)Main.rand.Next(-4, 4) * .05f;			
            return soundInstance;
        }
    }
}