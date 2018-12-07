using AAMod.Buffs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Backgrounds
{
    public class FogHandler : ModWorld
    {
		ScreenFog mireFog = new ScreenFog(false);
		
        public override void PostDrawTiles()
        {
            mireFog.Update(mod.GetTexture("Backgrounds/fog"));			
            mireFog.Draw(mod.GetTexture("Backgrounds/fog"), false, Color.White, true);
        }
    }
}