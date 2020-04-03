using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Brick
{
    public class DaybreakBrickWall : ModWall
	{
        public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("DaybreakWall");
            AddMapEntry(new Color(40, 12, 10));
            dustType = mod.DustType("DaybreakIncineriteDust");
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/DaybreakBrickWall_Glow");
            BaseMod.BaseDrawing.DrawWallTexture(sb, glowTex, x, y, false, AAGlobalTile.GetAkumaColorDim);
        }
    }
}