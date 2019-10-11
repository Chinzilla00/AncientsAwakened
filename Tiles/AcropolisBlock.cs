using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AcropolisBlock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][Terraria.ModLoader.ModContent.TileType<AcropolisAltarBlock>()] = true;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("Acropolis Brick");   
            AddMapEntry(new Color(0, 29, 125));
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