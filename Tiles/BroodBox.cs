using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using BaseMod;

namespace AAMod.Tiles
{
    class BroodBox : ModTile
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
            dustType = mod.DustType("RazeleafDust");
            AddMapEntry(new Color(200, 200, 200), name);
		}

        public Texture2D glowTex = null;

        public Color GetColor(Color color)
        {
            return Color.White;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/BroodBox_Glow");
            if (glowTex != null && tile != null && tile.active() && tile.type == Type)
            {
                int width = 16, height = 16;
                int frameX = (tile != null && tile.active() ? tile.frameX + (Main.tileFrame[Type] * 36) : 0);
                int frameY = (tile != null && tile.active() ? tile.frameY : 0);
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("BroodBox"));
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("BroodBox");
		}
	}
}
