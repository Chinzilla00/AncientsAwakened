using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthsand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModCactus(new Bogtus());
            SetModPalmTree(new BogPalmTree());
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthsand");   //put your CustomBlock name
            AddMapEntry(new Color(37, 33, 50));
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("BogPalmSapling");
        }
    }
}