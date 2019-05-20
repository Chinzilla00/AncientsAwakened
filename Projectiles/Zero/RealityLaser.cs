using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class RealityLaser : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reality Laser");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 10;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 30;
            projectile.extraUpdates = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            projectile.ranged = true;
        }

        public override void AI()
        {
        	int num103 = Player.FindClosest(projectile.Center, 1, 1);
			projectile.ai[1] += 1f;
			if (projectile.ai[1] < 110f && projectile.ai[1] > 30f)
			{
				float scaleFactor2 = projectile.velocity.Length();
				Vector2 vector11 = Main.player[num103].Center - projectile.Center;
				vector11.Normalize();
				vector11 *= scaleFactor2;
				projectile.velocity = ((projectile.velocity * 24f) + vector11) / 25f;
				projectile.velocity.Normalize();
				projectile.velocity *= scaleFactor2;
			}
			if (projectile.ai[0] < 0f)
			{
				if (projectile.velocity.Length() < 18f)
				{
					projectile.velocity *= 1.02f;
				}
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        	Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.9f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.1f) / 255f);
        }
    }
}