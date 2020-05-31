using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class DoomstoneBrickWall : ModWall
	{
		public override void SetDefaults()
        {
            dustType = mod.DustType("DoomDust");
			AddMapEntry(new Color(10, 10, 10));
            soundType = 21;
            drop = mod.ItemType("DoomstoneBrickWall");
            Main.wallHouse[Type] = true;
            Main.wallLargeFrames[Type] = 2;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            BaseDrawing.DrawWallTexture(spriteBatch, mod.GetTexture("Glowmasks/DoomstoneBrickWall_Glow"), i, j, false, Globals.AAGlobalTile.GetBlankColorDim);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}