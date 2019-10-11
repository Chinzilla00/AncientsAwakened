using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using BaseMod;

namespace AAMod.Tiles.Boxes
{
    class StarBox : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Music Box");
            dustType = mod.DustType("RadiumDust");
            AddMapEntry(new Color(200, 200, 200), name);
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("StarBox"));
		}

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (Main.dayTime)
            {
                ModTranslation name = CreateMapEntryName();
                AddMapEntry(new Color(160, 150, 0), name);
                dustType = mod.DustType("RadiumDust");
            }
            else
            {
                ModTranslation name = CreateMapEntryName();
                AddMapEntry(new Color(0, 30, 100), name);
                dustType = Terraria.ModLoader.ModContent.DustType<Dusts.DarkmatterDust>();
            }
            return true;
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            int width = 16, height = 16;
            int frameX = tile != null && tile.active() ? tile.frameX + (Main.tileFrame[Type] * 38) : 0;
            int frameY = tile != null && tile.active() ? tile.frameY : 0;
            Texture2D Tex = Main.tileTexture[Type];
            if (!Main.dayTime)
            {
                Tex = mod.GetTexture("Tiles/Boxes/StarBoxN");
            }
            BaseDrawing.DrawTileTexture(spriteBatch, Tex, x, y, width, height, frameX, frameY, false, false, false, null);
            return false;
        }

        public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("StarBox");
		}
	}
}
