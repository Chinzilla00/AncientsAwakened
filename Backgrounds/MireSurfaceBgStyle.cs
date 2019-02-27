using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Backgrounds
{
    public class MireSurfaceBgStyle : ModSurfaceBgStyle
    {
		ScreenFog mireBGFog = new ScreenFog(true);
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneMire && !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneDesert;
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
            return mod.GetBackgroundSlot("Backgrounds/MireBG");
        }
        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/MireFG2");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("Backgrounds/MireFG1");
        }
		
		public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
		{
			mireBGFog.Update(mod.GetTexture("Backgrounds/fog"));
			mireBGFog.Draw(mod.GetTexture("Backgrounds/fog"), true, new Color(120, 120, 200));
			return true;
		}
    }
}
