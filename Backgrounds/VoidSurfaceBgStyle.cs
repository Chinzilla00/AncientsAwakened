using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    class VoidSurfaceBgStyle : ModSurfaceBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneVoid;
        }

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            Player player = Main.player[Main.myPlayer];
            if (player.ZoneSkyHeight)
            {
                return mod.GetBackgroundSlot("Backgrounds/VoidBG");

            }
            return mod.GetBackgroundSlot("Backgrounds/VoidSurface");
        }
    }
}
