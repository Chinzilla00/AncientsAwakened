using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class VoidUGBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneVoid;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/VoidUG");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/VoidUG");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/VoidUG");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/VoidUG");
        }
    }
}
