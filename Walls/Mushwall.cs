using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class Mushwall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetDefaults()
		{
            Main.wallHouse[this.Type] = true;
			drop = mod.ItemType("Mushroom Wall");
			AddMapEntry(new Color(60, 14, 14));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }
    }
}