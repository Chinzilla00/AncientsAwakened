using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.Backgrounds
{
    public class MireSurfaceBgStyle : ModSurfaceBgStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneDesert;
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
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);
            
            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(mod.GetTexture("Backgrounds/FogTex"));
			mireBGFog.Draw(mod.GetTexture("Backgrounds/FogTex"), true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
		}
    }

    public class MireUgBgStyle : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/MireUnderground1");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/MireUnderground");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/MireCavern1");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/MireCavern");
        }
    }

    class MireDesertBgStyle : ModSurfaceBgStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

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
