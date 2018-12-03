using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TorchsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("TorchsandHardened");   //put your CustomBlock name
            AddMapEntry(new Color(50, 30, 17));
            minPick = 65;
        }
    }
}