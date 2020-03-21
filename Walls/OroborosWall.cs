using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class OroborosWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(8, 8, 8));
            soundType = 21;
            drop = mod.ItemType("OroborosWall");
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}