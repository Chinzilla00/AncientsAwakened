using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Doomgrass : ModTile
    {


        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Terraria.ID.TileID.Sets.NeedsGrassFraming[Type] = true;
            Main.tileMergeDirt[Type] = true;
            SetModTree(new OroborosTree());
            Terraria.ID.TileID.Sets.Conversion.Grass[Type] = true;
            drop = mod.ItemType("Dirt");
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(40, 40, 40));
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }
    }
}