using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.Backgrounds
{
    class MireDesertBgStyle : ModSurfaceBgStyle
    {
        ScreenFog mireBGFog = new ScreenFog(true);

        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && Main.LocalPlayer.ZoneDesert;
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
            return mod.GetBackgroundSlot("Backgrounds/MireDesertBG");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(mod.GetTexture("Backgrounds/FogTex"));
            mireBGFog.Draw(mod.GetTexture("Backgrounds/FogTex"), true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
        }

    }
}
