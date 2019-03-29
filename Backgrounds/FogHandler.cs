using AAMod.Buffs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.Backgrounds
{
    public class FogHandler : ModWorld
    {
		ScreenFog mireFog = new ScreenFog(false);
		
        public override void PostDrawTiles()
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(mod.NPCType<YamataA>());

            mireFog.Update(mod.GetTexture("Backgrounds/fog"));			
            mireFog.Draw(mod.GetTexture("Backgrounds/fog"), false, YamataA ? YamataFog : Color.White, true);
        }
    }
}