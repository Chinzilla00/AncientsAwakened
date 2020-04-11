using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace AAMod.Items.Dev.DevTile.Tiles
{
    public class CerberusKennel : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);
			ModTranslation modTranslation = CreateMapEntryName(null);
			modTranslation.SetDefault("Cerberus Kennel");
			AddMapEntry(Color.Gold, modTranslation);
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("InvokerBag"));
		}
    }
}