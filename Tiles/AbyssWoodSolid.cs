using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AbyssWoodSolid : ModTile
    {

        public bool glow = true; 
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][mod.TileType("AbyssLeaves")] = true;
            Main.tileMerge[Type][mod.TileType("AbyssWood")] = true;
            Main.tileMerge[Type][mod.TileType("Darkmud")] = true;
            Main.tileMerge[Type][mod.TileType("AbyssGrass")] = true;
            soundType = 21;
            dustType = ModContent.DustType<Dusts.AbyssDust>();
            AddMapEntry(new Color(52, 0, 200));
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