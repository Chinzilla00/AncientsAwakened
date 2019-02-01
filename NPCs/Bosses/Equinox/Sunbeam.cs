using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class Sunbeam : Moonray
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sunbeam");
		}

		public override void Effects()
		{
        	Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.5f) / 255f, ((255 - projectile.alpha) * 0.5f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f);	
		}
    }
}