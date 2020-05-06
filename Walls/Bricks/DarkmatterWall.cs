using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class DarkmatterWall : ModWall
	{
		public override void SetDefaults()
        {
            dustType = mod.DustType("DarkmatterDust");
            AddMapEntry(new Color(30, 30, 60));
            soundType = 21;
            drop = mod.ItemType("DarkmatterWall");
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}