using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DoomitePlate : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = mod.DustType("DoomDust");
            drop = mod.ItemType("DoomiteScrap");
            AddMapEntry(new Color(51, 48, 61));
            minPick = 0;
        }
    }
}