using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchsand : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModCactus(new Razetus());
            SetModPalmTree(new RazePalmTree());
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchsand");   //put your CustomBlock name
            AddMapEntry(new Color(50, 35, 22));
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("RazePalmSapling");
        }
    }
}