using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Walls
{
	public class Mushwall : ModWall
	{
        public Texture2D glowTex;
		public bool glow = true;

		public override void SetDefaults()
		{
            Main.wallHouse[this.Type] = true;
			drop = mod.ItemType("RottedWall");
			AddMapEntry(new Color(60, 14, 14));
		}
    }
}