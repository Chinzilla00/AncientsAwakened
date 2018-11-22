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
            Main.tileBlendAll[this.Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = false;
            soundType = 21;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthstone");   //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 127));
			minPick = 65;
        }
    }
}