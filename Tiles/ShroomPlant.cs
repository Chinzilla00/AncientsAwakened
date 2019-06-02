using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.Enums;
using Terraria.DataStructures;

namespace AAMod.Tiles
{
	public class Shroomplant : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
        }


        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int item = mod.ItemType("Shroomplant");
            Item.NewItem(i * 16, j * 16, 36, 36, item);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}