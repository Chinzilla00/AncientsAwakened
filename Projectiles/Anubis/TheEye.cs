using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis
{
    public class TheEye : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 42;
			projectile.tileCollide = false;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Eye");
		}
	
        public override void AI()
        {
            if (projectile.scale < 1f) projectile.scale += 0.01f;
            if (projectile.alpha > 0) projectile.alpha -= 5;

            if (projectile.ai[1] == 0)
            {
                projectile.velocity.Y += 0.005f;
                if (projectile.velocity.Y > .2f)
                {
                    projectile.ai[1] = 1f;
                    projectile.netUpdate = true;
                }
            }
            else
            if (projectile.ai[1] == 1)
            {
                projectile.velocity.Y -= 0.005f;
                if (projectile.velocity.Y < -.2f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
 
                float shootToX = target.position.X + target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != 488)
                {
                    if (projectile.ai[0] > 20f) // Time in (60 = 1 second) 
                    {
                        distance = 1.6f / distance;

                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX*4, shootToY*4, 668, projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                        projectile.ai[0] = 0f;
                    }
                }
            }
            projectile.ai[0] += 1f;
		}
	}
}