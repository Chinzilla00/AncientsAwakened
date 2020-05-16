using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class Torchstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Incinerite")] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            soundType = SoundID.Tink;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("Torchstone");   
            AddMapEntry(new Color(50, 25, 12));
			minPick = 65;
        }
    }
}