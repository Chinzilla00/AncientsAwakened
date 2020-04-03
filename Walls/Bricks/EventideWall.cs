using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls.Bricks
{
    public class EventideWall : ModWall
	{
		public override void SetDefaults()
        {
            Main.wallLight[Type] = true;
            dustType = mod.DustType("AbyssiumDust");
			AddMapEntry(new Color(33, 37, 96));
            soundType = 21;
            drop = mod.ItemType("EventideWall");
            Main.wallHouse[Type] = true;
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
    }
}