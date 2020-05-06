using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class CovetiteBrickWall : ModWall
	{
		public override void SetDefaults()
        {
            dustType = DustID.Gold;
            AddMapEntry(new Color(60, 60, 0));
            soundType = 21;
            drop = mod.ItemType("CovetiteBrickWall");
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}