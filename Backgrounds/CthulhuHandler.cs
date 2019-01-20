using AAMod.Buffs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Backgrounds
{
    public class CthulhuHandler : ModWorld
    {
		ScreenCthulhuFog CthulhuFog = new ScreenCthulhuFog(false);
		
        public override void PostDrawTiles()
        {
            CthulhuFog.Update(mod.GetTexture("Backgrounds/CthulhuClouds"));
            CthulhuFog.Draw(mod.GetTexture("Backgrounds/CthulhuClouds"), false, Color.White, true);
        }
    }
}