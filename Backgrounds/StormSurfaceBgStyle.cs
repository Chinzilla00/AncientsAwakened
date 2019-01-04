using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    class StormSurfaceBgStyle : ModSurfaceBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>(mod).ZoneStorm;
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

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("BlankTex");
        }

        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("BlankTex");
        }

        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("BlankTex");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            mireBGFog.Update(mod.GetTexture("Backgrounds/StormClouds"));
            mireBGFog.Draw(mod.GetTexture("Backgrounds/StormClouds"), true, new Color(160, 80, 200));
            return true;
        }
    }
}
