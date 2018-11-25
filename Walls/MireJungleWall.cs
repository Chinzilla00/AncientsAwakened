using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class MireJungleWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("AbyssiumDust");
			AddMapEntry(new Color(0, 0, 120));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}