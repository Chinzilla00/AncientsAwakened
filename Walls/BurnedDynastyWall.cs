using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class BurnedDynastyWall : ModWall
	{
		public override void SetDefaults()
		{
            dustType = mod.DustType("AshDust");
			AddMapEntry(new Color(50, 25, 0));
		}

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}


        public override void KillWall(int i, int j, ref bool fail)
        {
            if (AAWorld.downedShen)
            {
                fail = false;
            }
            fail = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}