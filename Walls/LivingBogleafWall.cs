using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class LivingBogleafWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("BogleafDust");
			AddMapEntry(new Color(100, 0, 150));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}