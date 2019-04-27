using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
	public class DevStatue : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			dustType = 2;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Statue");
			AddMapEntry(new Color(120, 120, 120), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 36)
			{
				case 0:
					item = mod.ItemType("AlphakipStatue");
					break;
				case 1:
					item = mod.ItemType("LizStatue");
					break;
				case 2:
					item = mod.ItemType("HallamStatue");
					break;
				case 3:
					item = mod.ItemType("FazerStatue");
					break;
                case 4:
                    item = mod.ItemType("DallinStatue");
                    break;
                case 5:
                    item = mod.ItemType("AvesStatue");
                    break;
                case 6:
                    item = mod.ItemType("GroxStatue");
                    break;
                case 7:
                    item = mod.ItemType("MoonStatue");
                    break;
                case 8:
                    item = mod.ItemType("SauceStatue");
                    break;
                case 9:
                    item = mod.ItemType("KyuuStatue");
                    break;
                case 10:
                    item = mod.ItemType("BegStatue");
                    break;
                case 11:
                    item = mod.ItemType("FargoStatue");
                    break;
                case 12:
                    item = mod.ItemType("TailsStatue");
                    break;
                case 13:
                    item = mod.ItemType("CharlieStatue");
                    break;
                case 14:
                    item = mod.ItemType("FerretStatue");
                    break;
                case 15:
                    item = mod.ItemType("LCSStatue");
                    break;
                case 16:
                    item = mod.ItemType("EnderStatue");
                    break;
            }
			if (item > 0)
			{
				Item.NewItem(i * 16, j * 16, 36, 36, item);
			}
		}
	}
}