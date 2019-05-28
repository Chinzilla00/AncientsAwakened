using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class RottedWall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetDefaults()
		{
            Main.wallHouse[this.Type] = true;
			drop = mod.ItemType("RottedWall");
			AddMapEntry(new Color(39, 34, 8));
		}

        public override void KillWall(int i, int j, ref bool fail)
        {
            fail = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}