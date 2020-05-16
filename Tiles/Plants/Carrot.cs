using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAMod.Tiles.Plants
{
    public class Carrot : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            drop = ModContent.ItemType<Items.Potions.Carrot>();
            dustType = ModContent.DustType<Dusts.CarrotDust>();
            soundType = SoundID.Grass;
        }

        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
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