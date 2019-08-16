using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class InfernoUgBgStyle : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneInferno;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/InfernoUnderground1");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/InfernoUnderground");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/InfernoCavern1");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/InfernoCavern");
        }
    }
}
