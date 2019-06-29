using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.Yamata.Awakened;
using Terraria.Graphics.Effects;

namespace AAMod.Backgrounds
{
    public class FogHandler : ModWorld
    {
		ScreenFog mireFog = new ScreenFog(false);
        
        public static OverlayManager Scene = new OverlayManager();

        public override void PostDrawTiles()
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(mod.NPCType<YamataA>());

            mireFog.Update(mod.GetTexture("Backgrounds/FogTex"));
            mireFog.Draw(mod.GetTexture("Backgrounds/FogTex"), false, YamataA ? YamataFog : Color.White, true);
        }
    }
}