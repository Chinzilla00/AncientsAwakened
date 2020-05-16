using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAMod.Tiles.Plants
{
    public class MadnessShroom : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.RandomStyleRange = 5;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            drop = ModContent.ItemType<Items.Mushrooms.MadnessShroom>();
            dustType = ModContent.DustType<Dusts.InfinityOverloadP>();
            soundType = SoundID.Grass;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}