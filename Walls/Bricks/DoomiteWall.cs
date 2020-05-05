using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class DoomiteWall : ModWall
	{
		public override void SetDefaults()
		{
            dustType = mod.DustType("DoomsdayDust");
			AddMapEntry(new Color(50, 25, 0));
            Main.wallHouse[Type] = true;
            soundType = 21;
            drop = mod.ItemType("DoomiteWall");
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}