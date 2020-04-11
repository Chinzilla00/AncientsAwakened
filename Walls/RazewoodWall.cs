using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class RazewoodWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("RazewoodDust");
            AddMapEntry(new Color(25, 12, 10));
            drop = mod.ItemType("RazewoodWall");
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}