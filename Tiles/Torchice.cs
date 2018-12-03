using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchice : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            soundType = 21;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchice");   //put your CustomBlock name
            AddMapEntry(new Color(50, 35, 0));
        }
    }
}