using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Tiles
{
    public class Doomgrass : ModTile
    {


        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            SetModTree(new OroborosTree());
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(50, 50, 50));
            drop = ItemID.DirtBlock;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }
    }
}