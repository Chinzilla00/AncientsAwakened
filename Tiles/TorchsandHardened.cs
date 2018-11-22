using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TorchsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("TorchsandHardened");   //put your CustomBlock name
            AddMapEntry(new Color(50, 30, 17));
            minPick = 65;
        }
    }
}