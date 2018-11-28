using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Apocalyptite : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Doomstone")] = true;
            Main.tileMergeDirt[Type] = false;
            //true for block to emit light
            soundType = 21;
            drop = mod.ItemType("Apocalyptite");   //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(70, 20, 20));
			minPick = 225;
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
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/ApocalyptiteTile_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}