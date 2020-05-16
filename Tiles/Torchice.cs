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
            Main.tileBlendAll[Type] = false;
            TileID.Sets.Conversion.Ice[Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileBlockLight[Type] = true;
            soundType = SoundID.Tink;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchice");   
            AddMapEntry(new Color(50, 35, 0));
            TileID.Sets.Ices[Type] = true;
        }
    }
}