using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class EverleafRoots : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            soundType = 21;
            dustType = mod.DustType("EverleafDust");
            drop = mod.ItemType("Everleaf");
            AddMapEntry(new Color(10, 80, 15));
			minPick = 225;
        }
      
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0;
            g = .30f;
            b = 0f;
        }
    }
}