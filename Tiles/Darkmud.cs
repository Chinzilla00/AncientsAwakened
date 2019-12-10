using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Darkmud : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("AbyssGrass")] = true;
            Main.tileMerge[Type][mod.TileType("AbyssWoodSolid")] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            soundType = 21;
            dustType = mod.DustType("DeepAbyssiumDust");
            AddMapEntry(new Color(0, 0, 100));
			minPick = 65;
        }


        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}