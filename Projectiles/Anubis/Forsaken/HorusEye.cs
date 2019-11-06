using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Anubis.Forsaken
{
    public class HorusEye : ModProjectile
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
			projectile.scale = 0.1f;
            projectile.alpha = 255;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Horus Eye");
		}
	
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, Color.DarkSeaGreen.R / 255, Color.DarkSeaGreen.G / 255, Color.DarkSeaGreen.B / 255);
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

                if (projectile.scale >= 1f && distance < 600f && target.catchItem == 0 && !target.friendly && target.active && target.type != 488 && Collision.CanHit(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                {
                    if (projectile.ai[0] > 15f) // Time in (60 = 1 second) 
                    {
                        for (int h = 0; h < 5; h++)
						{
							Vector2 vel = new Vector2(0, -1);
							float rand = Main.rand.NextFloat() * 6.283f;
							vel = vel.RotatedBy(rand);
							vel *= 8f;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, mod.ProjectileType("HorusHawk"), projectile.damage, 0, Main.myPlayer);
						}
                        projectile.ai[0] = 0f;
						projectile.scale = 0.5f;
                    }
                }
            }
            projectile.ai[0] += 1f;
		}
	}
}