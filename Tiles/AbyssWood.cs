using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class AbyssWood : ModTile
    {

        public bool glow = true; 
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][mod.TileType("AbyssLeaves")] = true;
            Main.tileMerge[Type][mod.TileType("AbyssWoodSolid")] = true;
            soundType = SoundID.Tink;
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