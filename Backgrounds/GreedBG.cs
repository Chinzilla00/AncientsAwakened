using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class GreedBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneHoard;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/GreedBG");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/GreedBG");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/GreedBG");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/GreedBG");
        }
    }
}
