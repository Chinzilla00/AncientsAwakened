
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.NPCs.Bosses.Shen
{
	public class Arrow : ModProjectile
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.light = 2f;
			projectile.height = 64;
			projectile.alpha = 0;
			projectile.timeLeft = 280;
			projectile.penetrate = 1;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			
		}

		public override void AI()
		{
            projectile.rotation += .3f;
			projectile.ai[0]++;
			if (projectile.ai[0] >= 180)
            {
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
			}
            else
            {
                projectile.Center = Main.player[projectile.owner].Center;
            }

			if (projectile.ai[0] == 180)
            {
		        projectile.alpha = 0;
			}
			if (projectile.ai[0] == 190)
            {
		        projectile.alpha = 255;
			}
			if (projectile.ai[0] == 200)
            {
		        projectile.alpha = 0;
			}
			if (projectile.ai[0] == 210)
            {
		        projectile.alpha = 255;
			}
			if (projectile.ai[0] == 215)
            {
		        projectile.alpha = 0;
			}
			if (projectile.ai[0] == 220)
            {	
		        projectile.alpha = 255;
			}
		}
	}
}

