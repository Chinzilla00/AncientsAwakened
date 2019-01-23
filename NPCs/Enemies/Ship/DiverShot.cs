using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Ship
{
    public class DiverShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eyeshot");
		}
    	
        public override void SetDefaults()
        {
            projectile.CloneDefaults(452);
        }
    }
}