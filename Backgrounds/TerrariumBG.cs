using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class TerrariumBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().Terrarium;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/TerrariumBG");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/TerrariumBG");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/TerrariumBG");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/TerrariumBG");
        }
    }
}
