using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAMod.Tiles.Plants
{
    public class BlackLotus : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            drop = mod.ItemType("BlackLotus");
            soundType = SoundID.Grass;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}