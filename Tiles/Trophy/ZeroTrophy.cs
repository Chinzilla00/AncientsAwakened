using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Trophy
{
    public class ZeroTrophy : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            dustType = 7;
			disableSmartCursor = true;
			AddMapEntry(new Color(120, 85, 60));
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 8)
            {
                frameCounter = 0;
                if (++frame >= 9) frame = 0;
            }
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
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/ZeroTrophy_Glow"), new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Globals.AAColor.COLOR_WHITEFADE1, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
            Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("ZeroTrophy"));
        }
	}
}