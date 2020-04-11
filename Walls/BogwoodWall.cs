using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class BogwoodWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("BogwoodDust");
            AddMapEntry(new Color(25, 12, 10));
            drop = mod.ItemType("BogwoodWall");
            Main.wallHouse[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}