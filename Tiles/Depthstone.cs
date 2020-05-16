using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("AbyssiumOre")] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            soundType = SoundID.Tink;
            minPick = 65;
            TileID.Sets.JungleSpecial[Type] = true;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthstone");   
            AddMapEntry(new Color(27, 19, 50));
        }
    }
}