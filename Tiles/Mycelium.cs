using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Mycelium : ModTile
	{
		public static int _type;

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			SetModTree(new MushroomTree());
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            dustType = mod.DustType("Mushdust");
			AddMapEntry(new Color(100, 100, 0));
		}
        
		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return mod.TileType("MushroomTree");
		}
	}
}