using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class FulgurGlassWall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetDefaults()
		{
            Main.wallHouse[this.Type] = true;
			drop = mod.ItemType("Fulgurite Glass Wall");
			AddMapEntry(new Color(40, 0, 50));
            
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