using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class InfernoGrassWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("RazeleafDust");
			AddMapEntry(new Color(200, 150, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}