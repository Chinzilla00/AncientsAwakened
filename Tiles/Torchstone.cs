using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Incinerite")] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            soundType = 21;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchstone");   //put your CustomBlock name
            AddMapEntry(new Color(50, 25, 12));
			minPick = 65;
        }
    }
}