using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class RadiumWall : ModWall
	{
		public override void SetDefaults()
        {
            dustType = mod.DustType("RadiumDust");
            AddMapEntry(new Color(60, 60, 30));
            soundType = 21;
            drop = mod.ItemType("RadiumWall");
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}