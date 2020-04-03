using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class RelicWall : ModWall
	{
		public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            dustType = DustID.Ice;
			AddMapEntry(new Color(30, 30, 60));
            soundType = 21;
            drop = mod.ItemType("RelicWall");
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}